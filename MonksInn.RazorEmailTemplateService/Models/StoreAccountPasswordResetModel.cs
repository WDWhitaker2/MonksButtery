using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.RazorEmailTemplateService.Models
{
    public class StoreAccountPasswordResetModel 
    {
        public StoreUser User { get; internal set; }
        public StoreUserPasswordResetToken NewToken { get; internal set; }
        public EmailButton ResetButton { get; internal set; }
    }
}
