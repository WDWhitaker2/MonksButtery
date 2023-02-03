using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MonksInn.Domain.Interfaces;
using MonksInn.Web.Models;
using MonksInn.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork uow) : base(uow)
        {
        }
        public IActionResult Index(string pub = "Monks Inn")
        {

            if(!PubLocationLogic.PubNameExists(pub))
            {
                pub = "Monks Inn";
            }

            var model = new IndexViewModel();
            model.CurrentPub = pub;
            model.OtherPubLocations = PubLocationLogic.GetAllPubLocations().Select(a => a.Name).Where(a=>a != pub).ToList();
            model.TappedStock = TapLogic.GetAllStockItems("Beer", "PubLocation")
                .Where(a => a.PubLocation.Name.Contains(pub))
                .ToList();

            return View(model);
        }

        public IActionResult Legal()
        {
            var model = new LegalViewModel();
            var settings = SystemSettingsLogic.GetSystemSettings();
            model.TermsAndConditions = settings.TermsAndConditions;
            model.PrivacyPolicy = settings.PrivacyPolicy;
            model.CookiePolicy = settings.CookiePolicy;
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ContactUsPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult ContactUsPartial(ContactUsPartialViewModel model)
        {

            if (ModelState.IsValid)
            {
                Uow.EmailService.SendContactUsEmail(model.Email, model.Name, model.Message);
                model.SentSuccessfully = true;
            }

            return PartialView(model);
        }

        
    }


}
