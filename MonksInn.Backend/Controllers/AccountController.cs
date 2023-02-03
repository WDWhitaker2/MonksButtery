using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.Account;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork uow) : base(uow)
        {
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel()
            {
                RedirectTo = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = SystemUserLogic.GetAuthenticatedUser(model.Email, model.ClearPassword);

                if (user != null)
                {

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.EmailAddress));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));



                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe,
                            ExpiresUtc = model.RememberMe ? DateTime.UtcNow.AddDays(20) : DateTime.UtcNow.AddMinutes(20)
                        });

                    if (!string.IsNullOrWhiteSpace(model.RedirectTo))
                    {
                        return Redirect(model.RedirectTo);

                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Access Denied!");
            }

            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return Redirect("Logout");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                SystemUserLogic.SendPasswordResetLink(model.EmailAddress);
                SaveDbChanges();
                model.LinkSent = true;

            }
            return View(model);

        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            var model = new ResetPasswordViewModel();
            model.Id = token;

            if (!SystemUserLogic.ResetLinkIsValid(token))
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
                SystemUserLogic.UpdatePasswordFromResetLink(model.Id, model.ClearPassword);
                SaveDbChanges();
                model.PasswordSaved = true;
            }
            return View(model);

        }

        [HttpGet]
        [HasAccess]
        public IActionResult Profile()
        {
            var user = SystemUserLogic.GetUser(User.GetUserId().Value);

            var model = new ProfileViewModel();
            model.EmailAddress = user.EmailAddress;
            model.Name = user.Name;
            return View(model);


        }

        [HttpPost]
        [HasAccess]
        public IActionResult Profile(ProfileViewModel model)
        {
            if (!SystemUserLogic.CheckEmailIsAvailable(model.EmailAddress, User.GetUserId().Value))
            {
                ModelState.AddModelError("EmailAddress", "Email Address Already Exists.");
            }

            if (ModelState.IsValid)
            {
                var user = SystemUserLogic.GetUser(User.GetUserId().Value);
                if (user != null)
                {

                    user.EmailAddress = model.EmailAddress;

                    if (!string.IsNullOrWhiteSpace(model.NewPassword))
                    {
                        SystemUserLogic.SetUserPassword(user, model.NewPassword);
                    }

                    SaveDbChanges();
                    model.UpdateSuccessful = true;
                }
            }

            AddAlert("Profile updated successfully.");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }





    }
}
