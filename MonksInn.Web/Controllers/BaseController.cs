using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic;
using MonksInn.Web.Authorization;
using MonksInn.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MonksInn.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        internal IUnitOfWork Uow { get; set; }
        internal BeerLibraryLogic BeerLibraryLogic { get; set; }
        internal StockOrderLogic StockOrderLogic { get; set; }
        internal CellarLogic CellarLogic { get; set; }
        internal TapLogic TapLogic { get; set; }
        internal PubLocationLogic PubLocationLogic { get; set; }
        internal SystemUserLogic SystemUserLogic { get; set; }
        internal StoreUserLogic StoreUserLogic { get; set; }
        internal PricingStructureLogic PricingStructureLogic { get; set; }
        internal FileLogic FileLogic { get; set; }
        internal CartSessionLogic CartSessionLogic { get; set; }
        internal OrderLogic OrderLogic { get; set; }
        internal PromoCodeLogic PromoCodeLogic { get; set; }
        internal DeliverySlotLogic DeliverySlotLogic { get; set; }
        internal WholesaleApplicationLogic WholesaleApplicationLogic { get; set; }
        internal SystemSettingsLogic SystemSettingsLogic { get; set; }

        public BaseController(IUnitOfWork uow)
        {
            Uow = uow;
            BeerLibraryLogic = new BeerLibraryLogic(Uow);
            StockOrderLogic = new StockOrderLogic(Uow);
            CellarLogic = new CellarLogic(Uow);
            TapLogic = new TapLogic(Uow);
            PubLocationLogic = new PubLocationLogic(Uow);
            SystemUserLogic = new SystemUserLogic(Uow);
            StoreUserLogic = new StoreUserLogic(Uow);
            PricingStructureLogic = new PricingStructureLogic(Uow);
            FileLogic = new FileLogic(Uow);
            CartSessionLogic = new CartSessionLogic(Uow);
            OrderLogic = new OrderLogic(Uow);
            PromoCodeLogic = new PromoCodeLogic(Uow);
            DeliverySlotLogic = new DeliverySlotLogic(Uow);
            WholesaleApplicationLogic = new WholesaleApplicationLogic(Uow);
            SystemSettingsLogic = new SystemSettingsLogic(Uow);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.PrivacyCookieValue = GetPrivacyCookie();
        }

        internal void SaveDbChanges()
        {
            Uow.DbContext.SaveChanges();
        }

        internal void SetCartSessionCookie(Guid? Id)
        {
            if (Id.HasValue)
            {
                HttpContext.Response.Cookies.Append("CartSession", Id.ToString());
            }
            else
            {
                HttpContext.Response.Cookies.Delete("CartSession");
            }
        }

        internal StoreUser GetCurrentUser()
        {
            var currentUserId = User.GetUserId();
            if (currentUserId.HasValue)
            {
                return StoreUserLogic.GetUser(currentUserId.Value);
            }
            return null;
        }

        internal Guid? GetCartSessionCookie()
        {
            var cookieValue = HttpContext.Request.Cookies["CartSession"];

            if (!string.IsNullOrWhiteSpace(cookieValue))
            {
                if (Guid.TryParse(cookieValue, out Guid result))
                {
                    return result;
                }
            }
            return null;
        }

        internal void SetPrivacyCookie(bool value)
        {
            HttpContext.Response.Cookies.Append("CookiePolicy", value.ToString());
        }
        internal bool? GetPrivacyCookie()
        {
            var cookieValue = HttpContext.Request.Cookies["CookiePolicy"];

            if (!string.IsNullOrWhiteSpace(cookieValue))
            {
                if (bool.TryParse(cookieValue, out bool result))
                {
                    return result;
                }
            }
            return null;
        }

        internal void AddAlert(string message, string type = "success")
        {
            var alertitems = TempData["Alerts"] != null ? JsonConvert.DeserializeObject<List<Alert>>(TempData["Alerts"].ToString()) : new List<Alert>();

            alertitems.Add(new Alert
            {
                Message = message,
                Type = type
            });

            TempData["Alerts"] = JsonConvert.SerializeObject(alertitems);
        }
        internal void ForceOpenLoginModal()
        {
            TempData["ForceOpenLoginModal"] = true;

        }
    }
}
