using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.Domain.Interfaces
{
    public interface IEmailService
    {
        void SendSystemAccountPasswordResetLinkAsync(SystemUser user, SystemUserPasswordResetToken token);
        void SendTestEmailAsync(string toEmail);
        void SendStoreUserRegistrationEmail(StoreUser newuser, StoreUserRegistrationToken token);
        void SendStoreAccountPasswordResetLink(StoreUser user, StoreUserPasswordResetToken newToken);
        void SendContactUsEmail(string email, string name, string message);
        void SendWholesaleApplicationEmail(WholesaleApplication wholesaleApplication, StoreUser storeUser);
        void SendWholesaleApplicationResponseEmail(WholesaleApplication application, StoreUser storeUser);
        void SendBookingConfirmationEmail(Booking booking);
    }
}
