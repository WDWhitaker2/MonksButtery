using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.RazorEmailTemplateService.Models
{
    public class SystemAccountPasswordResetLinkModel
    {
        public SystemUserPasswordResetToken NewToken { get;  set; }
        public SystemUser User { get;  set; }
        public EmailButton ResetButton { get; set; }
    }
}
