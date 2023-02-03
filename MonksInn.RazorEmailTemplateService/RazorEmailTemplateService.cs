using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using MimeKit;
using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.RazorEmailTemplateService.Engine;
using MonksInn.RazorEmailTemplateService.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MonksInn.RazorEmailTemplateService
{
    public class RazorEmailTemplateService : CustomRazorEngine, IEmailTemplateService
    {
        SystemSettings Settings { get; }
        public RazorEmailTemplateService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider, IDatabaseService databaseService ) : base(razorViewEngine, tempDataProvider, serviceProvider)
        {
            Settings = databaseService.SystemSettings.AsQueryable(true).FirstOrDefault();
        }


        public async Task<string> GetStoreUserRegistrationBodyAsync(StoreUser newuser, StoreUserRegistrationToken token)
        {
            var model = new StoreUserRegistrationModel();
            model.User = newuser;
            model.NewToken = token;
            model.ResetButton = new EmailButton("Confirm Email Address", $"/Account/VerifyEmail?token={HttpUtility.UrlEncode(token.TokenHash)}");

            return await RenderViewToStringAsync("StoreUserRegistrationEmail", model, Settings.EmailTemplateBaseWebUrl);

        }

        public async Task<string> GetSystemAccountPasswordResetLinkBodyAsync(SystemUser user, SystemUserPasswordResetToken token)
        {
            var model = new SystemAccountPasswordResetLinkModel();
            model.User = user;
            model.NewToken = token;
            model.ResetButton = new EmailButton("Reset Email" , $"/Account/ResetPassword?token={HttpUtility.UrlEncode(token.Hash)}");

            return await RenderViewToStringAsync("SystemAccountPasswordResetLinkEmail", model, Settings.EmailTemplateBaseBackendUrl);
        }

        public async Task<string> GetTestEmailBodyAsync()
        {
            var model = new TestEmailModel();
            model.PlaceholderText = "Get Rekt!";

            return await RenderViewToStringAsync("TestEmail", model, Settings.EmailTemplateBaseWebUrl);
        }

        public async Task<string> GetStoreAccountPasswordResetBodyAsync(StoreUser user, StoreUserPasswordResetToken token)
        {
            var model = new StoreAccountPasswordResetModel();
            model.User = user;
            model.NewToken = token;
            model.ResetButton = new EmailButton("Reset Email", $"/Account/ResetPassword?token={HttpUtility.UrlEncode(token.Hash)}");


            string emailBody = await RenderViewToStringAsync("StoreAccountPasswordResetEmail", model, Settings.EmailTemplateBaseWebUrl);

            return emailBody;
        }

        public async Task<string> GetContactUsBodyAsync(string email, string name, string usermessage)
        {
            var model = new ContactUsModel();
            model.UserEmail = email;
            model.UserName = name;
            model.UserMessage = usermessage;
            model.ResponseButton = new EmailButton("RespondToEmail", $"mailto:{email}?body=Dear {name}");


            string emailBody = await RenderViewToStringAsync("ContactUsEmail", model, Settings.EmailTemplateBaseWebUrl);

            return emailBody;
        }

        public async Task<string> GetWholesaleApplicationEmailBodyAsync(WholesaleApplication application, StoreUser storeUser)
        {
            var model = new WholesaleApplicationModel();
            model.Application = application;
            model.StoreUser = storeUser;
            model.ReviewAccountButton = new EmailButton("View Application", $"");

            string emailBody = await RenderViewToStringAsync("WholesaleApplicationEmail", model, Settings.EmailTemplateBaseBackendUrl);

            return emailBody;
        }

        public async Task<string> GetWholesaleApplicationResponseEmailBodyAsync(WholesaleApplication application, StoreUser storeUser)
        {
            var model = new WholesaleApplicationResponseModel();
            model.Application = application;
            model.StoreUser = storeUser;

            string emailBody = await RenderViewToStringAsync("WholesaleApplicationResponseEmail", model, Settings.EmailTemplateBaseWebUrl);

            return emailBody;
        }

        public async Task<string> GetBookingConfirmationBodyAsync(Booking booking)
        {
            var model = new BookingConfirmationModel();
            model.Booking = booking;

            string emailBody = await RenderViewToStringAsync("BookingConfirmationEmail", model, Settings.EmailTemplateBaseWebUrl);

            return emailBody;
        }
    }
}