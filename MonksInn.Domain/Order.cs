using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Domain
{
    public class Order : DatabaseObject
    {
        public long OrderNumber { get; set; }

        public bool IsForDelivery { get; set; }


       
        public OrderStatus OrderStatus { get; set; }
        public bool PaymentReceived { get; set; }





        public Guid? DeliveryAddressId { get; set; }
        public virtual StoreUserAddress DeliveryAddress { get; set; }



        public Guid? PromoCodeId { get; set; } 
        public virtual PromoCode PromoCode { get; set; }

        public virtual List<OrderItem> Items { get; set; }
        public Guid StoreUserId { get; set; }
        public virtual StoreUser StoreUser { get; set; }
        public decimal Total { get; set; }
        public decimal PromoDiscount { get; set; }
        public Guid? DeliveryDateAllocationId { get; set; }
        public virtual DeliveryDateAllocation DeliveryDateAllocation { get; set; }

        public Guid? PubLocationId { get; set; }
        public virtual PubLocation PubLocation { get; set; }
        public Guid? DispatchingUser { get; set; }
        public DateTime? DispatchDate { get; set; }
        public Guid? CompletionByUser { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
