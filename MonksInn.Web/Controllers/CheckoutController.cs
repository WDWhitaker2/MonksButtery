using Microsoft.AspNetCore.Mvc;
using MonksInn.Domain.Interfaces;
using MonksInn.Web.Authorization;
using MonksInn.Web.Models.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Controllers
{
    [HasAccess]
    public class CheckoutController : BaseController
    {
        public CheckoutController(IUnitOfWork uow) : base(uow)
        {
        }
        [HttpGet]
        public IActionResult Index()
        {
         
            var model = new CheckoutIndexViewModel();
            model.IsWholesaleOrder = StoreUserLogic.UserIsWholesaleUser(User.GetUserId());
            model.IsDelivery = model.IsWholesaleOrder;
            SetAddresses(model);
            SetCart(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(CheckoutIndexViewModel model)
        {
            var cart = CartSessionLogic.GetCart(GetCartSessionCookie().Value, "Items.TappedStockItem");
            
            model.IsDelivery = model.IsDelivery == true ? model.IsDelivery : StoreUserLogic.UserIsWholesaleUser(User.GetUserId());

            if (model.IsDelivery)
            {
                if (!StoreUserLogic.UserIsWholesaleUser(User.GetUserId()))
                {
                    if (cart.Items.Any(a => a.TappedStockItem.ForTakeaway && !a.TappedStockItem.ForDelivery))
                    {
                        ModelState.AddModelError("IsDelivery", "There are items in your cart that are for takeaway only.");
                    }
                }

                // validate that all stock is for delivery

                // validate delivery date has value
                if (!model.SelectedDeliveryDateAllocation.HasValue)
                {
                    ModelState.AddModelError("SelectedDeliveryDateAllocation", "The Delivery date allocation field is required.");
                }

                // validate delivery address has value
                if (!model.SelectedAddress.HasValue)
                {
                    ModelState.AddModelError("SelectedAddress", "The Delivery Address field is required.");
                }

                // TODO validate delivery address has value we can deliver to.
                if (model.SelectedAddress.HasValue)
                {

                }
            }
            else
            {
                // validate that all stock is for takeaway
                if (cart.Items.Any(a => !a.TappedStockItem.ForTakeaway && a.TappedStockItem.ForDelivery))
                {
                    ModelState.AddModelError("IsDelivery", "There are items in your cart that are for delivery only.");
                }

                // validate that all stock from same location for takeaway
                if (cart.Items.Select(a => a.TappedStockItem.PubLocationId).Distinct().Count() > 1)
                {
                    ModelState.AddModelError("IsDelivery", "Takeaway orders can only be made if the pub location is the same for all items.");
                }
            }

            if (!StoreUserLogic.UserIsWholesaleUser(User.GetUserId()))
            {
                
                var tappedstock = TapLogic.GetAllStockItems();
                for (int i = 0; i < cart.Items.Count; i++)
                {

                    // validate that all stock is for delivery on the table.
                    if (model.IsDelivery && cart.Items[i].TappedStockItem.ForTakeaway && !cart.Items[i].TappedStockItem.ForDelivery)
                    {
                        ModelState.AddModelError($"Cart.Items[{i}]", "This item is only available for takeaway.");
                    }
                    // validate that all stock is for takeaway on the table.
                    if (!model.IsDelivery && !cart.Items[i].TappedStockItem.ForTakeaway && cart.Items[i].TappedStockItem.ForDelivery)
                    {
                        ModelState.AddModelError($"Cart.Items[{i}]", "This item is only available for delivery.");
                    }
                    // validate that items are still tapped.
                    if (!tappedstock.Any(a => a.Id == cart.Items[i].TappedStockItemId))
                    {
                        ModelState.AddModelError($"Cart.Items[{i}]", "This item is no longer tapped.");
                    }

                    //validate there are no wholesale items in cart
                    if(cart.Items[i].CellarStockItemId != null)
                    {
                        ModelState.AddModelError($"Cart.Items[{i}]", "This item is not available to retail accounts. please remove before continuing.");
                    }
                }
            }
            else
            {
                var availableStock = CellarLogic.GetAvailableStockItems(null);
               
                for (int i = 0; i < cart.Items.Count; i++)
                {
                    if (!availableStock.Any(a => a.Id == cart.Items[i].CellarStockItemId))
                    {
                        ModelState.AddModelError($"Cart.Items[{i}]", "This item is no longer available.");
                    }

                    //validate there are no wholesale items in cart
                    if (cart.Items[i].TappedStockItemId != null)
                    {
                        ModelState.AddModelError($"Cart.Items[{i}]", "This item is not available to wholesale accounts. please remove before continuing.");
                    }
                }
                
            }

            // validate if promocode is legit
            if (!string.IsNullOrWhiteSpace(model.PromoCode) && !PromoCodeLogic.ValidatePromoCode(model.PromoCode))
            {
                ModelState.AddModelError("PromoCode", "This is not a valid promo code.");
            }


            if (ModelState.IsValid)
            {

                var order = OrderLogic.CreateOrder(cart.Id, User.GetUserId().Value, model.IsDelivery, model.SelectedAddress, model.SelectedDeliveryDateAllocation, model.PromoCode);

                SetCartSessionCookie(null);



                SaveDbChanges();
                AddAlert("Payment screen INCOMING!");

                return RedirectToAction("Payment", new { id = order.Id });
            }

            SetAddresses(model);
            SetCart(model);

            return View(model);
        }


        public IActionResult AddressesPartial()
        {
            var model = new CheckoutIndexViewModel();
            SetAddresses(model);
            return PartialView(model);
        }

        private void SetAddresses(CheckoutIndexViewModel model)
        {
            model.Addresses = StoreUserLogic.GetUserAddresses(User.GetUserId().Value).ToList();
            model.DeliverySlots = DeliverySlotLogic.GenerateAndFetchNextAvailableDeliverySlots(14);
            SaveDbChanges();
        }
        private void SetCart(CheckoutIndexViewModel model)
        {
            model.CartId = GetCartSessionCookie();
            if (model.CartId.HasValue)
            {
                model.Cart = CartSessionLogic.GetCart(model.CartId.Value,
                    "Items",
                    "Items.CellarStockItem",
                    "Items.CellarStockItem.Beer",
                    "Items.TappedStockItem",
                    "Items.TappedStockItem.Beer",
                    "Items.TappedStockItem.PubLocation");
            }
        }
        public IActionResult CartPartial()
        {
            var model = new CheckoutIndexViewModel();
            //get cart items
            SetCart(model);


            return PartialView(model);
        }

        public IActionResult Payment(Guid id)
        {
            var model = new PaymentViewModel();
            model.Order = OrderLogic.GetOrdersForUser(User.GetUserId().Value).FirstOrDefault(a => a.Id == id);
            if (model.Order == null)
            {
                AddAlert("Order does not exists.", "danger");
                return RedirectToAction("Index", "Profile");
            }
            return View(model);
        }

        // TODO the parameter will need to change to prevent people from forcing the payment using a url with a order id.
        public IActionResult PaymentReceived(Guid id)
        {
            OrderLogic.MarkOrderAsPaid(id);
            SaveDbChanges();

            AddAlert("Payment has been recieved.");
            return RedirectToAction("Order", "Profile", new { id = id });

        }


        public IActionResult DeleteCart()
        {
            CartSessionLogic.DeleteCart(GetCartSessionCookie());
            SetCartSessionCookie(null);
            return RedirectToAction("Index", "Store");
        }
    }
}
