using BLL.Abstract;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize(Users = "besarabvitaliy@gmail.com,asd@asd.asd")]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerProvider _manufacturerProvider;
        private readonly ICountryProvider _countryProvider;

        public ManufacturerController(IManufacturerProvider manufacturerProvider, ICountryProvider countryProvider)
        {
            _manufacturerProvider = manufacturerProvider;
            _countryProvider = countryProvider;
            ViewBag.MenuManufacturer = true;
        }

        // GET: Manufacturer
        public ActionResult Index(int page = 1, int items = 10, string searchName = null)
        {
            return View(_manufacturerProvider.GetManufacturersByPage(page, items, searchName));
        }

        public ActionResult Create()
        {
            ManufacturerInterfereViewModel model = new ManufacturerInterfereViewModel();
            InitializeCountries(ref model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ManufacturerInterfereViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_manufacturerProvider.CreateManufacturer(model) == StatusViewModel.Dublication)
                    ModelState.AddModelError("", "This Manufacturer already exists");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _manufacturerProvider.DeleteManufacturer(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ManufacturerInterfereViewModel model = new ManufacturerInterfereViewModel();
            InitializeCountries(ref model);
            var manufacturer = _manufacturerProvider.GetManufacturerById(id);
            model.Name = manufacturer.Name;
            model.CountryId = manufacturer.CountryId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ManufacturerInterfereViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                if (_manufacturerProvider.EditManufacturer(model, id) == StatusViewModel.Dublication)
                    ModelState.AddModelError("", "This Manufacturer already exists");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        private void InitializeCountries(ref ManufacturerInterfereViewModel model)
        {
            model.Countries = _countryProvider
            .GetCountries()
            .Select(c => new SelectItemViewModel<int>
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
    }
}