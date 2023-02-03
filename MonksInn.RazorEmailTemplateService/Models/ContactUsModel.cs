using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.RazorEmailTemplateService.Models
{
    public class ContactUsModel
    {
        public string UserMessage { get; internal set; }
        public string UserName { get; internal set; }
        public string UserEmail { get; internal set; }
        public EmailButton ResponseButton { get; internal set; }
    }
}
