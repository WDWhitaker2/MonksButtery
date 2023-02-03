using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class StoreUserLogic : BaseLogic
    {
        public StoreUserLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<StoreUser> GetAllUsers(params string[] includes)
        {
            return Uow.DbContext.StoreUsers.AsQueryable(true, includes);
        }

        public bool CheckEmailIsAvailable(string emailAddress, Guid? userId = null)
        {

            var users = Uow.DbContext.StoreUsers.AsQueryable(true);
            if (userId.HasValue)
            {
                users = users.Where(a => a.Id != userId);
            }

            return !users.Any(a => a.EmailAddress.ToLower() == emailAddress);
        }

        public bool UserIsWholesaleUser(Guid? userId)
        {
            return Uow.DbContext.StoreUsers.AsQueryable(false).FirstOrDefault(a => a.Id == userId)?.IsWholeSaleUser ?? false;
        }

        public StoreUser GetAuthenticatedUser(string username, string password)
        {
            var user = Uow.DbContext.StoreUsers.AsQueryable(true).FirstOrDefault(a => a.EmailAddress == username);
            if (user != null)
            {
                if (PasswordExtensions.ChallengeString(user.HashedPassword, password, user.Salt))
                {
                    return user;
                }
            }
            return null;
        }



        public StoreUser Add(StoreUser storeUser)
        {
            storeUser.DateCreated = DateTime.Now;
            return Uow.DbContext.StoreUsers.Add(storeUser);
        }

        public void SetUserPassword(StoreUser user, string newPassword)
        {

            user.HashedPassword = PasswordExtensions.GenerateHashString(newPassword, out var saltKey);
            user.Salt = saltKey;
        }

        public StoreUser GetUser(Guid id)
        {
            return Uow.DbContext.StoreUsers.AsQueryable(true).FirstOrDefault(a => a.Id == id);
        }

        public void Delete(Guid id)
        {
            var user = Uow.DbContext.StoreUsers.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if (user != null)
            {
                user.IsArchived = true;
            }
        }

        public void Register(StoreUser user)
        {
            var newuser = Add(user);

            var token = CreateStoreUserRegistrationToken(user);

            Uow.EmailService.SendStoreUserRegistrationEmail(newuser, token);

        }

        public void ApplyForWholesaleAccount(WholesaleApplication wholesaleApplication)
        {

            wholesaleApplication.DateCreated = DateTime.Now;

            var newApplication = Uow.DbContext.WholesaleApplications.Add(wholesaleApplication);

            var storeUser = Uow.DbContext.StoreUsers.AsQueryable(true).FirstOrDefault(a => a.Id == wholesaleApplication.StoreUserId);
             Uow.EmailService.SendWholesaleApplicationEmail(newApplication, storeUser);
        }

        public StoreUserRegistrationToken CreateStoreUserRegistrationToken(StoreUser user)
        {

            var token = new StoreUserRegistrationToken()
            {
                DateCreated = DateTime.Now,
                StoreUserId = user.Id,

            };
            var newtoken = Uow.DbContext.StoreUserRegistrationTokens.Add(token);
            newtoken.TokenHash = PasswordExtensions.GenerateHashString(newtoken.Id.ToString(), out var saltKey);
            newtoken.Salt = saltKey;

            return newtoken;
        }

        

        public void AddUserAddress(Guid value, StoreUserAddress storeUserAddress)
        {
            storeUserAddress.StoreUserId = value;
            storeUserAddress.DateCreated = DateTime.Now;
            Uow.DbContext.StoreUserAddresses.Add(storeUserAddress);
        }

        public IQueryable<StoreUserAddress> GetUserAddresses(Guid value)
        {
            return Uow.DbContext.StoreUserAddresses.AsQueryable(true).Where(a => a.StoreUserId == value);
        }

        public StoreUserAddress GetUserAddress(Guid id)
        {
            return Uow.DbContext.StoreUserAddresses.AsQueryable(true).FirstOrDefault(a => a.Id == id);
        }

        public void DeleteUserAddress(Guid id)
        {
            var useraddress = Uow.DbContext.StoreUserAddresses.AsQueryable(false).FirstOrDefault(a => a.Id == id);
            if (useraddress != null)
            {

                Uow.DbContext.StoreUserAddresses.Remove(useraddress);
            }
        }

        public void ResendRegistrationToken(string username)
        {
            var validuser = Uow.DbContext.StoreUsers.AsQueryable(false).Where(a => a.EmailAddress.ToLower() == username.ToLower() && !a.IsArchived).FirstOrDefault();

            if (validuser != null)
            {
                var validtoken = Uow.DbContext.StoreUserRegistrationTokens.AsQueryable(false).Where(a => !a.IsArchived && a.StoreUserId == validuser.Id).FirstOrDefault();
                if (validtoken != null)
                {
                    Uow.EmailService.SendStoreUserRegistrationEmail(validuser, validtoken);
                }
            }
        }

        public void SendPasswordResetLink(string emailAddress)
        {
            var user = Uow.DbContext.StoreUsers.AsQueryable(true).FirstOrDefault(a => a.EmailAddress.ToLower() == emailAddress.ToLower());
            if (user != null)
            {
                //archive all previous tokens
                var oldtokens = Uow.DbContext.StoreUserPasswordResetTokens.AsQueryable(false).Where(a => a.StoreUserId == user.Id).ToList();
                foreach (var oldtoken in oldtokens)
                {
                    oldtoken.IsArchived = true;
                }

                var token = new StoreUserPasswordResetToken()
                {
                    DateCreated = DateTime.Now,
                    StoreUserId = user.Id
                };

                var newToken = Uow.DbContext.StoreUserPasswordResetTokens.Add(token);
                newToken.Hash = PasswordExtensions.GenerateHashString(newToken.Id.ToString(), out int saltKey);
                newToken.Salt = saltKey;

                Uow.EmailService.SendStoreAccountPasswordResetLink(user, newToken);
            }
        }

        public bool ResetLinkIsValid(string hash)
        {
            var datetocompare = DateTime.Now.AddMinutes(-15);
            return Uow.DbContext.StoreUserPasswordResetTokens.AsQueryable(true).Any(a => a.Hash == hash && a.DateCreated >= datetocompare);
        }

        public void UpdatePasswordFromResetLink(string hash, string clearPassword)
        {
            var token = Uow.DbContext.StoreUserPasswordResetTokens.AsQueryable(false).FirstOrDefault(a => a.Hash == hash);
            var user = Uow.DbContext.StoreUsers.AsQueryable(false).FirstOrDefault(a => a.Id == token.StoreUserId);

            token.IsArchived = true;
            user.HashedPassword = PasswordExtensions.GenerateHashString(clearPassword, out int saltKey);
            user.Salt = saltKey;
        }

        public void AddLikedBeer(Guid userId, Guid beerId)
        {
            if (!Uow.DbContext.StoreUserLikedBeers.AsQueryable(false).Any(a => a.StoreUserId == userId && a.BeerId == beerId))
            {
                var newlikedBeer = new StoreUserLikedBeer()
                {
                    DateCreated = DateTime.Now,
                    BeerId = beerId,
                    StoreUserId = userId,
                };
                Uow.DbContext.StoreUserLikedBeers.Add(newlikedBeer);
            }
        }

        public bool TryVerifyEmailLink(string token)
        {
            var usertoken = Uow.DbContext.StoreUserRegistrationTokens.AsQueryable(true, "StoreUser").FirstOrDefault(a => a.TokenHash == token);
            if (usertoken != null)
            {
                usertoken.IsArchived = true;
                usertoken.StoreUser.EmailAddressVerified = true;
                return true;
            }
            return false;
        }

        public bool ForceVerifyEmailLinkForUser(Guid UserId)
        {
            var usertokens = Uow.DbContext.StoreUserRegistrationTokens.AsQueryable(true).Where(a => a.StoreUserId == UserId).ToList();
            foreach (var usertoken in usertokens)
            {
                usertoken.IsArchived = true;
            }
            var user = Uow.DbContext.StoreUsers.AsQueryable(true).FirstOrDefault(a=>a.Id == UserId);
            user.EmailAddressVerified = true;
            return true;
        }

        public IQueryable<StoreUserLikedBeer> GetUserLikedBeers(Guid userId)
        {
            return Uow.DbContext.StoreUserLikedBeers.AsQueryable(true).Where(a => a.StoreUserId == userId);
        }

        public void RemoveLikedBeer(Guid userId, Guid beerId)
        {
            var likedbeers = Uow.DbContext.StoreUserLikedBeers.AsQueryable(false).Where(a => a.BeerId == beerId && a.StoreUserId == userId).ToList();
            if (likedbeers.Any())
            {
                Uow.DbContext.StoreUserLikedBeers.Remove(likedbeers);
            }
        }
    }
}
