using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Domain.Interfaces;
using MonksInn.Web.Authorization;
using MonksInn.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MonksInn.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork uow) : base(uow)
        {
        }
        [HttpGet]
        public IActionResult Login()
        {
            ForceOpenLoginModal();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LoginPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPartial(LoginViewModel model)
        {

            if (!model.ForceSendNewLink)
            {
                var user = StoreUserLogic.GetAuthenticatedUser(model.Username, model.ClearPassword);
                //if ()
                if (user == null)
                {
                    ModelState.AddModelError("", "The username and password were incorrect. Please try again.");
                }
                else if (!user.EmailAddressVerified)
                {
                    ModelState.AddModelError("", "Email verification is required. Click below if you would like us to send you a new email activation link?");
                    model.ShowSendNewLinkButton = true;
                }

                if (ModelState.IsValid)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.EmailAddress));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                    //identity.AddClaim(new Claim(ClaimTypes.Role, (user.IsWholeSaleUser ? StoreUserRole.WholeSaleUser.ToString() : StoreUserRole.RetailUser.ToString())));



                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe,
                            ExpiresUtc = model.RememberMe ? DateTime.UtcNow.AddDays(20) : DateTime.UtcNow.AddMinutes(20)
                        });


                    if (!GetCartSessionCookie().HasValue && user.CartSessionID.HasValue)
                    {
                        //if there is no cart in cookie but user has cart assign that cart to cookie
                        SetCartSessionCookie(user.CartSessionID.Value);
                    }
                    else if (GetCartSessionCookie().HasValue && !user.CartSessionID.HasValue)
                    {
                        // if there is a cart cookie but the user is not assigned a cart then assign cart to user;
                        user.CartSessionID = GetCartSessionCookie();
                    }
                    else if (GetCartSessionCookie().HasValue && user.CartSessionID.HasValue && GetCartSessionCookie().Value != user.CartSessionID.Value)
                    {
                        //if user has a cart and there is a different cart assigned to user then join the carts.
                        CartSessionLogic.JoinCarts(GetCartSessionCookie().Value, user.CartSessionID.Value);
                    }

                    SaveDbChanges();
                    model.LoginSuccessful = true;
                }
            }
            else
            {
                StoreUserLogic.ResendRegistrationToken(model.Username);
                model.ForceSendNewLinkSuccess = true;
            }


            return PartialView(model);

        }

        [HttpGet]
        public IActionResult Register()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.ClearPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
            }

            if (!StoreUserLogic.CheckEmailIsAvailable(model.Email))
            {
                ModelState.AddModelError("Email", "Email Address Already Exists.");
            }

            if (ModelState.IsValid)
            {
                var user = new Domain.StoreUser()
                {
                    EmailAddress = model.Email,
                    Name = model.Name,
                    CartSessionID = GetCartSessionCookie()
                };

                StoreUserLogic.SetUserPassword(user, model.ClearPassword);
                StoreUserLogic.Register(user);


                SaveDbChanges();

                model.RegistrationSuccessful = true;

            }

            return PartialView(model);

        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                StoreUserLogic.SendPasswordResetLink(model.EmailAddress);
                SaveDbChanges();
                model.LinkSent = true;

            }
            return PartialView(model);

        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            var model = new ResetPasswordViewModel();
            model.Id = token;

            if (!StoreUserLogic.ResetLinkIsValid(token))
            {
                model.InvalidLink = true;
            }


            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                StoreUserLogic.UpdatePasswordFromResetLink(model.Id, model.ClearPassword);
                SaveDbChanges();
                model.PasswordSaved = true;

                AddAlert("Your password has been updated successfully.", "success");
                ForceOpenLoginModal();
                return RedirectToAction("Index", "Home");
            }
            return View(model);

        }

        public IActionResult VerifyEmail(string token)
        {

            if (StoreUserLogic.TryVerifyEmailLink(token))
            {
                SaveDbChanges();
                ForceOpenLoginModal();
                AddAlert("Your email was verified successfully.", "success");
            }
            else
            {
                AddAlert("The token used has expired or is invalid.", "danger");
            }
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("", "Home");
        }

    }
}
