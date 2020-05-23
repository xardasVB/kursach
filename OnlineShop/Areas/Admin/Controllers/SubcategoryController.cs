using BLL.Abstract;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize(Users = "besarabvitaliy@gmail.com asd@asd.asd")]
    public class SubcategoryController : Controller
    {
        private readonly ISubcategoryProvider _SubcategoryProvider;
        private readonly ICategoryProvider _CategoryProvider;

        public SubcategoryController(ISubcategoryProvider SubcategoryProvider, ICategoryProvider CategoryProvider)
        {
            _SubcategoryProvider = SubcategoryProvider;
            _CategoryProvider = CategoryProvider;
            ViewBag.MenuSubcategory = true;
        }

        // GET: Subcategory
        public ActionResult Index(int page = 1, int items = 10, string searchName = null)
        {
            return View(_SubcategoryProvider.GetSubcategoriesByPage(page, items, searchName));
        }

        public ActionResult Create()
        {
            SubcategoryInterfereViewModel model = new SubcategoryInterfereViewModel();
            InitializeCategories(ref model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SubcategoryInterfereViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_SubcategoryProvider.CreateSubcategory(model) == StatusViewModel.Dublication)
                    ModelState.AddModelError("", "This Subcategory already exists");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _SubcategoryProvider.DeleteSubcategory(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            SubcategoryInterfereViewModel model = new SubcategoryInterfereViewModel();
            InitializeCategories(ref model);
            var Subcategory = _SubcategoryProvider.GetSubcategoryById(id);
            model.Name = Subcategory.Name;
            model.CategoryId = Subcategory.CategoryId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SubcategoryInterfereViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                if (_SubcategoryProvider.EditSubcategory(model, id) == StatusViewModel.Dublication)
                    ModelState.AddModelError("", "This Subcategory already exists");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        private void InitializeCategories(ref SubcategoryInterfereViewModel model)
        {
            model.Categories = _CategoryProvider
            .GetCategories()
            .Select(c => new SelectItemViewModel<int>
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
    }
}