using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonksInn.RazorEmailTemplateService.Models
{
    public class BookingConfirmationModel
    {
        public Booking Booking { get; internal set; }
    }
}
