using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    public class XtestController : BaseController
    {

        internal XTestLogic XTestLogic { get; set; }

        public XtestController(IUnitOfWork uow) : base(uow)
        {
            XTestLogic = new XTestLogic(uow);
        }

        public IActionResult Index()
        {
            XTestLogic.SendTestEmail("wesisnotavailable@gmail.com");
            return View();
        }
    }
}
