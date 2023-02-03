using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Models;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
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
        internal PromoCodeLogic PromoCodeLogic { get; set; }
        internal DeliverySlotLogic DeliverySlotLogic { get; set; }
        internal OrderLogic OrderLogic { get; set; }
        internal SystemSettingsLogic SystemSettingsLogic { get; set; }
        internal WholesaleApplicationLogic WholesaleApplicationLogic { get; set; }
        internal BookingLogic BookingLogic { get; set; }
        
        public BaseController(IUnitOfWork uow) : base()
        {

            Uow = uow;
            BeerLibraryLogic = new BeerLibraryLogic(uow);
            StockOrderLogic = new StockOrderLogic(uow);
            CellarLogic = new CellarLogic(uow);
            TapLogic = new TapLogic(uow);
            PubLocationLogic = new PubLocationLogic(uow);
            SystemUserLogic = new SystemUserLogic(uow);
            StoreUserLogic = new StoreUserLogic(uow);
            PricingStructureLogic = new PricingStructureLogic(uow);
            FileLogic = new FileLogic(uow);
            PromoCodeLogic = new PromoCodeLogic(uow);
            DeliverySlotLogic = new DeliverySlotLogic(uow);
            OrderLogic = new OrderLogic(uow);
            SystemSettingsLogic = new SystemSettingsLogic(uow);
            WholesaleApplicationLogic = new WholesaleApplicationLogic(uow);
            BookingLogic = new BookingLogic(uow);
        }

      
        internal void SaveDbChanges()
        {
            Uow.DbContext.SaveChanges();
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


    }
}
