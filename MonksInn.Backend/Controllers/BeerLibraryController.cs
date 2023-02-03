using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonksInn.Backend.Authorization;
using MonksInn.Backend.Models.BeerLibrary;
using MonksInn.Domain.Enums;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Controllers
{
    [HasAccess]
    public class BeerLibraryController : BaseController
    {
        public BeerLibraryController(IUnitOfWork uow) : base(uow)
        {
        }

        [HasAccess(SystemPermission.CanAccessBeerIndexPage)]
        public IActionResult Index()
        {
            var model = new IndexViewModel();

            model.BeerList = BeerLibraryLogic.GetAllBeers().OrderBy(a => a.BeerType).ThenBy(a => a.BreweryName).ThenBy(a => a.BeerName).ToList();

            return View(model);
        }

        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdateBeer)]
        public IActionResult Add(BeerType BeerType = BeerType.Ale)
        {
            var model = new AddViewModel()
            {
                BeerType = BeerType
            };

            model.CurrentBreweries = BeerLibraryLogic.GetAllBreweryNames();
            model.CurrentTypes = BeerLibraryLogic.GetAllBeerSubTypes();

            return View(model);
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdateBeer)]
        public IActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newbeer = BeerLibraryLogic.Add(new Domain.Beer()
                {
                    BeerName = model.BeerName,
                    BreweryName = model.BreweryName,
                    BeerType = model.BeerType,
                    Notes = model.Notes,
                    Strength = model.Strength,
                    SubType = model.SubType,
                    IsSpecialityBeer = model.IsSpecialityBeer,
                    ImageId = model.ImageId,
                });

                try
                {
                    FileLogic.CompressBeerImage(newbeer.Id);
                }
                catch (Exception)
                {

                }

                SaveDbChanges();
                AddAlert("Beer added successfully.");

                return RedirectToAction("index");
            }



            model.CurrentBreweries = BeerLibraryLogic.GetAllBreweryNames();
            model.CurrentTypes = BeerLibraryLogic.GetAllBeerSubTypes();

            if (!string.IsNullOrWhiteSpace(model.BreweryName) && !model.CurrentBreweries.Contains(model.BreweryName))
            {
                model.CurrentBreweries.Add(model.BreweryName);
            }
            if (!string.IsNullOrWhiteSpace(model.SubType) && !model.CurrentTypes.Contains(model.SubType))
            {
                model.CurrentBreweries.Add(model.SubType);
            }


            return View(model);
        }


        [HttpGet]
        [HasAccess(SystemPermission.CanAddUpdateBeer)]
        public IActionResult Update(Guid id)
        {
            ViewBag.IsUpdate = true;

            var beer = BeerLibraryLogic.GetBeer(id);
            if (beer != null)
            {
                var model = new AddViewModel()
                {
                    BeerName = beer.BeerName,
                    BreweryName = beer.BreweryName,
                    Id = beer.Id,
                    BeerType = beer.BeerType,
                    Notes = beer.Notes,
                    Strength = beer.Strength,
                    SubType = beer.SubType,
                    IsSpecialityBeer = beer.IsSpecialityBeer,
                    ImageId = beer.ImageId,
                };

                model.CurrentBreweries = BeerLibraryLogic.GetAllBreweryNames();
                model.CurrentTypes = BeerLibraryLogic.GetAllBeerSubTypes();

                return View("Add", model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [HasAccess(SystemPermission.CanAddUpdateBeer)]
        public IActionResult Update(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var beer = BeerLibraryLogic.GetBeer(model.Id);
                if (beer != null)
                {

                    beer.BeerName = model.BeerName;
                    beer.BreweryName = model.BreweryName;
                    beer.BeerType = model.BeerType;
                    beer.Notes = model.Notes;
                    beer.Strength = model.Strength;
                    beer.SubType = model.SubType;
                    beer.IsSpecialityBeer = model.IsSpecialityBeer;
                    beer.ImageId = model.ImageId;

                    try
                    {
                        FileLogic.CompressBeerImage(beer.Id);
                    }
                    catch (Exception)
                    {

                    }

                    SaveDbChanges();
                    AddAlert("Beer updated successfully.");

                    return RedirectToAction("index");
                }
            }



            model.CurrentBreweries = BeerLibraryLogic.GetAllBreweryNames();
            model.CurrentTypes = BeerLibraryLogic.GetAllBeerSubTypes();

            if (!string.IsNullOrWhiteSpace(model.BreweryName) && !model.CurrentBreweries.Contains(model.BreweryName))
            {
                model.CurrentBreweries.Add(model.BreweryName);
            }
            if (!string.IsNullOrWhiteSpace(model.SubType) && !model.CurrentTypes.Contains(model.SubType))
            {
                model.CurrentBreweries.Add(model.SubType);
            }


            ViewBag.IsUpdate = true;
            return View("Add", model);
        }

        [HasAccess(SystemPermission.CanArchiveBeer)]
        public IActionResult Archive(Guid id)
        {

            BeerLibraryLogic.Delete(id);
            SaveDbChanges();
            AddAlert("Beer archived successfully.");


            return RedirectToAction("Index");
        }

        public IActionResult GetImageLibrary()
        {
            var model = new List<GetImageLibraryViewModel>();

            var beerlist = BeerLibraryLogic.GetAllBeers().OrderBy(a => a.BeerType).ThenBy(a => a.BreweryName).ThenBy(a => a.BeerName).ToList();


            var imageList = FileLogic.GetAllFiles().Where(a => beerlist.Select(a => a.ImageId).Contains(a.Id)).ToList();

            foreach (var item in beerlist)
            {
                model.Add(new GetImageLibraryViewModel()
                {
                    BeerId = item.Id,
                    BeerName = item.BeerName,
                    BreweryName = item.BreweryName,
                    ImageId = item.ImageId,
                    Size = imageList.FirstOrDefault(a => a.Id == item.ImageId)?.FileData.Length ?? 0
                });
            }


            return View(model);

        }


        public IActionResult CompressBeerImage(Guid id)
        {
            FileLogic.CompressBeerImage(id);
            SaveDbChanges();
            return RedirectToAction("GetImageLibrary");
        }
    }
}
