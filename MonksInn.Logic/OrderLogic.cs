using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using MonksInn.Logic.Models.OrderLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class OrderLogic : BaseLogic
    {
        public OrderLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public Order CreateOrder(Guid cartId, Guid storeUserId, bool isForDelivery, Guid? deliveryAddressId, Guid? deliveryDateAllocationId, string promocode)
        {
            //
            var promo = Uow.DbContext.PromoCodes.AsQueryable(false).FirstOrDefault(a => a.Code == promocode);
            var cart = Uow.DbContext.CartSessions.AsQueryable(false, "Items", "Items.TappedStockItem").FirstOrDefault(a => a.Id == cartId);
            var user = Uow.DbContext.StoreUsers.AsQueryable(false).FirstOrDefault(a => a.Id == storeUserId);

            //clear user cart;
            user.CartSessionID = null;
            //create an order dto
            var order = new Order();
            order.StoreUserId = storeUserId;
            order.DateCreated = DateTime.Now;
            order.DeliveryAddressId = deliveryAddressId;
            order.DeliveryDateAllocationId = deliveryDateAllocationId;
            order.IsForDelivery = isForDelivery;
            order.PromoCodeId = promo?.Id;
            order.PaymentReceived = false;
            order.PubLocationId = cart.Items.FirstOrDefault().TappedStockItem.PubLocationId;

            order.Items = new List<OrderItem>();

            // assign the cart  items to the order
            foreach (var item in cart.Items)
            {
                order.Items.Add(new OrderItem
                {
                    DateCreated = DateTime.Now,
                    TappedStockItemId = item.TappedStockItemId,
                    TappedStockItemPricePerUnit = item.TappedStockItemPricePerUnit.Value,
                    TappedStockUnits = item.TappedStockUnits
                });
            }

            var promologic = new PromoCodeLogic(Uow);
            decimal discount = 0m;
            if (promo != null)
            {
                // spend the promocode
                promologic.UsePromoCode(promo);
                discount = promologic.GetDiscountedAmount(order, promo);
            }
            order.PromoDiscount = discount;
            // store the total so that if there are future changes then previous records arent affected.
            order.Total = order.Items.Sum(a => a.TappedStockItemPricePerUnit * a.TappedStockUnits) - discount;
            var newOrder = Uow.DbContext.Orders.Add(order);

            // delete cart session
            Uow.DbContext.CartSessions.Remove(cart);
            //todo create an order history item

            return newOrder;
        }

        public object GetOrder(Guid id, params string[] includes)
        {
            return Uow.DbContext.Orders.AsQueryable(true, includes).FirstOrDefault(a => a.Id == id);
        }

        public void MarkOrderReceived(Guid id, Guid userId)
        {
            var order = Uow.DbContext.Orders.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            order.OrderStatus = Domain.Enums.OrderStatus.Complete;
            order.CompletionDate = DateTime.Now;
            order.CompletionByUser = userId;

            //todo create an order history item
        }

        public void MarkOrderDispatched(Guid id, Guid userId)
        {
            var order = Uow.DbContext.Orders.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            order.OrderStatus = order.IsForDelivery ? Domain.Enums.OrderStatus.PendingDelivery : Domain.Enums.OrderStatus.PendingPickup;
            order.DispatchingUser = userId;
            order.DispatchDate = DateTime.Now;

            //todo create an order history item

        }

        public IQueryable<Order> GetOrders(params string[] includes)
        {
            return Uow.DbContext.Orders.AsQueryable(true, includes);
        }

        public IQueryable<Order> GetOrdersForUser(Guid storeUserId, params string[] includes)
        {
            return Uow.DbContext.Orders.AsQueryable(true, includes).Where(a => a.StoreUserId == storeUserId);
        }

        public void CancelOrder(Guid id)
        {
            var order = Uow.DbContext.Orders.AsQueryable(false).FirstOrDefault(a => a.Id == id);
            if (order.OrderStatus == Domain.Enums.OrderStatus.PendingPayment)
            {
                order.OrderStatus = Domain.Enums.OrderStatus.Cancelled;
            }

            //todo create an order history item

        }

        public void MarkOrderAsPaid(Guid id)
        {
            var order = Uow.DbContext.Orders.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if(order != null)
            {
                order.PaymentReceived = true;
                //once an order is paid it goes to dispatch
                order.OrderStatus = Domain.Enums.OrderStatus.PendingDispatch;
            }

            //todo create an order history item

        }
    }
}
