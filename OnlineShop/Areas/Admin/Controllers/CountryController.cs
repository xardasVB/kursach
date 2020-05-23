using BLL.Abstract;
using BLL.Concrete;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize(Users = "besarabvitaliy@gmail.com asd@asd.asd")]
    public class CountryController : Controller
    {
        private readonly ICountryProvider _countryProvider;

        public CountryController(ICountryProvider countryProvider)
        {
            _countryProvider = countryProvider;
            ViewBag.MenuCountry = true;
        }

        // GET: Country
        public ActionResult Index(int page = 1, int items = 10, string searchName = null)
        {
            return View(_countryProvider.GetCountriesByPage(page, items, searchName));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CountryInterfereViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_countryProvider.CreateCountry(model) == StatusViewModel.Dublication)
                    ModelState.AddModelError("", "This country already exists");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _countryProvider.DeleteCountry(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var country = _countryProvider.GetCountryById(id);
            return View(new CountryInterfereViewModel { Name = country.Name });
        }

        [HttpPost]
        public ActionResult Edit(CountryInterfereViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                if (_countryProvider.EditCountry(model, id) == StatusViewModel.Dublication)
                    ModelState.AddModelError("", "This country already exists");
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}