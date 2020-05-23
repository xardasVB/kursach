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
    public class CategoryController : Controller
    {
        private readonly ICategoryProvider _CategoryProvider;

        public CategoryController(ICategoryProvider CategoryProvider)
        {
            _CategoryProvider = CategoryProvider;
            ViewBag.MenuCategory = true;
        }

        // GET: Category
        public ActionResult Index(int page = 1, int items = 10, string searchName = null)
        {
            return View(_CategoryProvider.GetCategoriesByPage(page, items, searchName));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryInterfereViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_CategoryProvider.CreateCategory(model) == StatusViewModel.Dublication)
                    ModelState.AddModelError("", "This Category already exists");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _CategoryProvider.DeleteCategory(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var Category = _CategoryProvider.GetCategoryById(id);
            return View(new CategoryInterfereViewModel { Name = Category.Name });
        }

        [HttpPost]
        public ActionResult Edit(CategoryInterfereViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                if (_CategoryProvider.EditCategory(model, id) == StatusViewModel.Dublication)
                    ModelState.AddModelError("", "This Category already exists");
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}