using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.Domain.Interfaces
{
    public interface IEmailTemplateService
    {
        Task<string> GetTestEmailBodyAsync();
        Task<string> GetSystemAccountPasswordResetLinkBodyAsync(SystemUser user, SystemUserPasswordResetToken newToken);
        Task<string> GetStoreUserRegistrationBodyAsync(StoreUser newuser, StoreUserRegistrationToken token);
        Task<string> GetStoreAccountPasswordResetBodyAsync(StoreUser user, StoreUserPasswordResetToken token);
        Task<string> GetBookingConfirmationBodyAsync(Booking booking);
        Task<string> GetContactUsBodyAsync(string email, string name, string usermessage);
        Task<string> GetWholesaleApplicationEmailBodyAsync(WholesaleApplication application, StoreUser storeUser);
        Task<string> GetWholesaleApplicationResponseEmailBodyAsync(WholesaleApplication application, StoreUser storeUser);
    }
}
