using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.Order;
using MonksInn.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class OrdersController : BaseController
    {
        public OrdersController(IUnitOfWork uow) : base(uow)
        {
        }

        [HasAccess(SystemPermission.CanAccessAllOrdersPage)]
        public IActionResult Index()
        {
            var model = new OrderViewModel();
            model.Orders = GetOrders()
                .OrderByDescending(a => a.OrderNumber)
                .ToList();
            model.ActiveNav = "Orders";
            return View(model);
        }

        public IActionResult View(Guid id)
        {
            var order = OrderLogic.GetOrder(id, 
                "PubLocation", 
                "DeliveryAddress", 
                "StoreUser",
                "Items",
                "Items.TappedStockItem",
                "Items.TappedStockItem.Beer"
                );
            return View(order);
        }


        [HasAccess(SystemPermission.CanAccessDispatchOrdersPage)]
        public IActionResult Dispatch()
        {
            var model = new OrderViewModel();
            model.Orders = GetOrders()
                .Where(a => a.OrderStatus == Domain.Enums.OrderStatus.PendingDispatch)
                .Where(a => !a.IsForDelivery || a.DeliveryDateAllocation.DateAllocation.Date <= DateTime.Now.Date)
                .ToList();
            return View(model);

        }

        [HasAccess(SystemPermission.CanDispatchOrder)]
        public IActionResult MarkOrderDispatched(Guid id)
        {
            OrderLogic.MarkOrderDispatched(id, User.GetUserId().Value);
            SaveDbChanges();
            return RedirectToAction("Dispatch");
        }




        [HasAccess(SystemPermission.CanAccessDeliveryOrdersPage)]
        public IActionResult Deliveries()
        {
            var model = new OrderViewModel();
            model.Orders = GetOrders()
                .Where(a => a.OrderStatus == Domain.Enums.OrderStatus.PendingDelivery)
                .ToList();
            return View(model);

        }

        [HasAccess(SystemPermission.CanAccessDeliveryOrdersPage)]
        public IActionResult MarkOrderDelivered(Guid id, string action)
        {
            OrderLogic.MarkOrderReceived(id, User.GetUserId().Value);
            SaveDbChanges();
            return RedirectToAction("Deliveries");
        }



        [HasAccess(SystemPermission.CanAccessTakeawayOrdersPage)]
        public IActionResult Takeaways()
        {
            var model = new OrderViewModel();
            model.Orders = GetOrders()
                .Where(a => a.OrderStatus == Domain.Enums.OrderStatus.PendingPickup)
                .ToList();
            return View(model);

        }

        [HasAccess(SystemPermission.CanAccessTakeawayOrdersPage)]
        public IActionResult MarkOrderReceived(Guid id, string action)
        {
            OrderLogic.MarkOrderReceived(id, User.GetUserId().Value);
            SaveDbChanges();
            return RedirectToAction("Takeaways");
        }

        


        private IQueryable<Domain.Order> GetOrders()
        {
            return OrderLogic
                .GetOrders("Items",
                "StoreUser",
                "DeliveryAddress",
                "Items.TappedStockItem.Beer",
                "PubLocation",
                "DeliveryDateAllocation");
        }
    }
}
