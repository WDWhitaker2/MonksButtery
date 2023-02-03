using MonksInn.Domain;
using MonksInn.Logic.Models.DeliverySlotLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Models.Checkout
{
    public class CheckoutIndexViewModel
    {
        public Guid? CartId { get; internal set; }
        
        [Display(Name ="Delivery address")]
        public Guid? SelectedAddress { get; set; }
        [Required]
        public bool IsDelivery { get; set; }
        public string PromoCode { get; set; }
        [Display(Name = "Delivery date allocation")]
        public Guid? SelectedDeliveryDateAllocation { get; set; }

        public CartSession Cart { get; internal set; }
        public List<StoreUserAddress> Addresses { get; internal set; }
        public List<DeliveryDateAllocation> DeliverySlots { get; internal set; }
        public bool IsWholesaleOrder { get; internal set; }
    }
}
