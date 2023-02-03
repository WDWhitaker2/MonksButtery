using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MonksInn.Domain.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Waiting Payment")]
        PendingPayment,
        [Display(Name = "Waiting Dispatch")]
        PendingDispatch,
        [Display(Name = "Waiting Pickup")]
        PendingPickup,
        [Display(Name = "Waiting Delivery")]
        PendingDelivery,
        Complete,
        Cancelled,
        [Display(Name = "Waiting Return Collection")]
        PendingReturned,
        Returned,
    }
}
