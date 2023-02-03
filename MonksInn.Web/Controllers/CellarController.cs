using Microsoft.AspNetCore.Mvc;
using MonksInn.Domain.Interfaces;
using MonksInn.Web.Models.Cellar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Web.Controllers
{
    public class CellarController : BaseController
    {
        public CellarController(IUnitOfWork uow) : base(uow)
        {
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.CellarStock = CellarLogic.GetAllStockItems("Beer").ToList();
            return View(model);
        }
    }
}
