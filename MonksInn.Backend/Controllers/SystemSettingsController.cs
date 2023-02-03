using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.SystemSettings;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class SystemSettingsController : BaseController
    {
        public SystemSettingsController(IUnitOfWork uow) : base(uow)
        {
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAccessSystemSettingsPage)]
        public IActionResult Index()
        {
            var settings = SystemSettingsLogic.GetSystemSettings();

            if (settings == null)
            {
                settings = SystemSettingsLogic.GenerateSettings();
                SaveDbChanges();
            }

            var model = new SystemSettingsViewModel();
            model.PrivacyPolicy = settings.PrivacyPolicy;
            model.TermsAndConditions = settings.TermsAndConditions;

            model.SmtpServer = settings.SmtpServer;
            model.SmtpPort = settings.SmtpPort;
            model.SmtpUseSsl = settings.SmtpUseSsl;
            model.SmtpUsername = settings.SmtpUsername;
            model.SmtpPassword = settings.SmtpPassword;
            model.DefaultFromEmail = settings.DefaultFromEmail;
            model.EmailTemplateBaseWebUrl = settings.EmailTemplateBaseWebUrl;
            model.EmailTemplateBaseBackendUrl = settings.EmailTemplateBaseBackendUrl;
            model.ContactUsRecipientEmail = settings.ContactUsRecipientEmail;
            model.CookiePolicy = settings.CookiePolicy;



            return View(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAccessSystemSettingsPage)]
        public IActionResult Index(SystemSettingsViewModel settings)
        {
            if (ModelState.IsValid)
            {

                var model = SystemSettingsLogic.GetSystemSettings();
                model.PrivacyPolicy = settings.PrivacyPolicy;
                model.TermsAndConditions = settings.TermsAndConditions;

                model.SmtpServer = settings.SmtpServer;
                model.SmtpPort = settings.SmtpPort;
                model.SmtpUseSsl = settings.SmtpUseSsl;
                model.SmtpUsername = settings.SmtpUsername;
                model.SmtpPassword = settings.SmtpPassword;
                model.DefaultFromEmail = settings.DefaultFromEmail;
                model.EmailTemplateBaseWebUrl = settings.EmailTemplateBaseWebUrl;
                model.EmailTemplateBaseBackendUrl = settings.EmailTemplateBaseBackendUrl;
                model.ContactUsRecipientEmail = settings.ContactUsRecipientEmail;
                model.CookiePolicy = settings.CookiePolicy;


                SaveDbChanges();
                AddAlert("System settings updated successfully");
            }
            return View(settings);
        }
    }
}
