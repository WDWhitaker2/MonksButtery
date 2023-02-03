using MimeKit;
using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.SmtpEmailService.Engine;
using System;
using System.Threading.Tasks;

namespace MonksInn.SmtpEmailService
{
    public class SmtpEmailService : SmtpMailEngine, IEmailService
    {
        public SmtpEmailService(IDatabaseService databaseService, IEmailTemplateService emailTemplateService) : base(databaseService, emailTemplateService)
        {
        }

        public async void SendBookingConfirmationEmail(Booking booking)
        {
            string bodyText = await EmailTemplateService.GetBookingConfirmationBodyAsync(booking);

            var message = GetNewMimeMessage(bodyText);
            message.To.Add(new MailboxAddress(booking.FullName, booking.EmailAddress));
            message.Subject = "Booking Confirmation";

            SendEmail(message);
        }

        public async void SendContactUsEmail(string email, string name, string usermessage)
        {
            string bodyText = await EmailTemplateService.GetContactUsBodyAsync(email, name, usermessage);

            var message = GetNewMimeMessage(bodyText);
            message.To.Add(new MailboxAddress("", Settings.ContactUsRecipientEmail));
            message.Subject = "Contact Us Request";

            SendEmail(message);
        }

        public async void SendStoreAccountPasswordResetLink(StoreUser user, StoreUserPasswordResetToken token)
        {
            string bodyText = await EmailTemplateService.GetStoreAccountPasswordResetBodyAsync(user, token);

            var message = GetNewMimeMessage(bodyText);
            message.To.Add(new MailboxAddress(user.Name, user.EmailAddress));
            message.Subject = "Account Password Reset Request";

            SendEmail(message);
        }

        public async void SendStoreUserRegistrationEmail(StoreUser newuser, StoreUserRegistrationToken token)
        {
            string bodyText = await EmailTemplateService.GetStoreUserRegistrationBodyAsync(newuser, token);
    
            var message = GetNewMimeMessage(bodyText);
            message.To.Add(new MailboxAddress(newuser.Name, newuser.EmailAddress));
            message.Subject = "Welcome to Monk's Inn";

            SendEmail(message);
        }

        public async void SendSystemAccountPasswordResetLinkAsync(SystemUser user, SystemUserPasswordResetToken newToken)
        {
            string bodyText = await EmailTemplateService.GetSystemAccountPasswordResetLinkBodyAsync(user, newToken);
   
            var message = GetNewMimeMessage(bodyText);
            message.To.Add(new MailboxAddress(user.Name, user.EmailAddress));
            message.Subject = "Account Password Reset Request";

            SendEmail(message);
        }

        public async void SendTestEmailAsync(string toEmail)
        {
            string bodyText = await EmailTemplateService.GetTestEmailBodyAsync();
   
            var message = GetNewMimeMessage(bodyText);
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Test Email";

            SendEmail(message);
        }


        public async void SendWholesaleApplicationEmail(WholesaleApplication wholesaleApplication, StoreUser storeUser)
        {
            string bodyText = await EmailTemplateService.GetWholesaleApplicationEmailBodyAsync(wholesaleApplication, storeUser);

            var message = GetNewMimeMessage(bodyText);
            message.To.Add(new MailboxAddress("", Settings.ContactUsRecipientEmail));
            message.Subject = "Wholesale Application Request";

            SendEmail(message);
        }

        public async void SendWholesaleApplicationResponseEmail(WholesaleApplication application, StoreUser storeUser)
        {
            string bodyText = await EmailTemplateService.GetWholesaleApplicationResponseEmailBodyAsync(application, storeUser);

            var message = GetNewMimeMessage(bodyText);
            message.To.Add(new MailboxAddress(storeUser.Name, storeUser.EmailAddress));
            message.Subject = "Wholesale Application Response";

            SendEmail(message);
        }
    }
}
