using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class SystemUserLogic : BaseLogic
    {
        public SystemUserLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<SystemUser> GetAllUsers(params string[] includes)
        {
            return Uow.DbContext.SystemUsers.AsQueryable(true, includes);
        }

        public bool CheckEmailIsAvailable(string emailAddress, Guid? userId = null)
        {
            emailAddress = emailAddress.ToLower();

            var users = Uow.DbContext.SystemUsers.AsQueryable(true);
            if (userId.HasValue)
            {
                users = users.Where(a => a.Id != userId);
            }

            return !users.Any(a => a.EmailAddress.ToLower() == emailAddress);
        }



        public SystemUser Add(SystemUser systemUser)
        {
            systemUser.DateCreated = DateTime.Now;
            return Uow.DbContext.SystemUsers.Add(systemUser);
        }

        public void SetUserPassword(SystemUser user, string newPassword)
        {

            user.HashedPassword = PasswordExtensions.GenerateHashString(newPassword, out var saltKey);
            user.Salt = saltKey;
        }

        public SystemUser GetUser(Guid id)
        {
            return Uow.DbContext.SystemUsers.AsQueryable(true).FirstOrDefault(a => a.Id == id);
        }

        public void Delete(Guid id)
        {
            var user = Uow.DbContext.SystemUsers.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if (user != null)
            {
                user.IsArchived = true;
            }
        }

        public SystemUser GetAuthenticatedUser(string username, string password)
        {
            username = username.ToLower();
            var user = Uow.DbContext.SystemUsers.AsQueryable(true).FirstOrDefault(a => a.EmailAddress.ToLower() == username);

            if (PasswordExtensions.ChallengeString(user.HashedPassword, password, user.Salt))
            {
                return user;
            }
            return null;
        }

        public Domain.Enums.Role GetUserRole(Guid id)
        {
            return Uow.DbContext.SystemUsers.AsQueryable(true).FirstOrDefault(a => a.Id == id).Role;
        }

        public void SendPasswordResetLink(string emailAddress)
        {
            var user = Uow.DbContext.SystemUsers.AsQueryable(true).FirstOrDefault(a => a.EmailAddress.ToLower() == emailAddress.ToLower());
            if (user != null)
            {
                //archive all previous tokens
                var oldtokens = Uow.DbContext.SystemUserPasswordResetTokens.AsQueryable(false).Where(a => a.SystemUserId == user.Id).ToList();
                foreach (var oldtoken in oldtokens)
                {
                    oldtoken.IsArchived = true;
                }

                //create a token
                var token = new SystemUserPasswordResetToken()
                {
                    DateCreated = DateTime.Now,
                    SystemUserId = user.Id
                };

                //add token to context and set hash based on the id.
                var newToken = Uow.DbContext.SystemUserPasswordResetTokens.Add(token);
                newToken.Hash = PasswordExtensions.GenerateHashString(newToken.Id.ToString(), out int saltKey);
                newToken.Salt = saltKey;

                Uow.EmailService.SendSystemAccountPasswordResetLinkAsync(user, newToken);
            }
        }

        public bool ResetLinkIsValid(string hash)
        {
            var datetocompare = DateTime.Now.AddMinutes(-15);
            return Uow.DbContext.SystemUserPasswordResetTokens.AsQueryable(true).Any(a => a.Hash == hash && a.DateCreated >= datetocompare);
        }

        public void UpdatePasswordFromResetLink(string hash, string clearPassword)
        {
            var token = Uow.DbContext.SystemUserPasswordResetTokens.AsQueryable(false).FirstOrDefault(a => a.Hash == hash);
            var user = Uow.DbContext.SystemUsers.AsQueryable(false).FirstOrDefault(a => a.Id == token.SystemUserId);

            token.IsArchived = true;
            user.HashedPassword = PasswordExtensions.GenerateHashString(clearPassword, out int saltKey);
            user.Salt = saltKey;
        }
    }
}
