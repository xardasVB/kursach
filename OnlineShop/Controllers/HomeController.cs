using BLL.Abstract;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Helpers;
using Newtonsoft.Json;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        ICategoryProvider _categoryProvider;
        ISubcategoryProvider _subcategoryProvider;
        IProductProvider _productProvider;
        static ProductViewModel products = new ProductViewModel();

        public HomeController(ICategoryProvider categoryProvider,
            ISubcategoryProvider subcategoryProvider,
            IProductProvider productProvider)
        {
            _categoryProvider = categoryProvider;
            _subcategoryProvider = subcategoryProvider;
            _productProvider = productProvider;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Shop(ProductSearchViewModel searchModel = null)
        {
            products = _productProvider.GetProductsByPage(0, 0, searchModel);
            return View(products);
        }

        [HttpGet]
        public ContentResult AjaxGetListProducts(int begin)
        {
            string json = "";
            List<ProductItemViewModel> list = products.Products
                .Skip(begin)
                .Take(12).ToList();
            json = JsonConvert.SerializeObject(new
            {
                count = list.Count(),
                products = list
            });
            return Content(json, "application/json");
        }

        //[Authorize]
        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        [ChildActionOnly]
        public ActionResult GetProductImage(int id)
        {
            var product = _productProvider.GetProductById(id);
            ImageViewModel image = new ImageViewModel();

            if (product.Image != null)
                image.ImageName = product.Image;
            image.Folder = "Product";

            return PartialView("_Image", image);
        }

        public ActionResult GetCategories()
        {
            var list = new Dictionary<CategoryItemViewModel, IEnumerable<SubcategoryItemViewModel>>();
            foreach (var category in _categoryProvider.GetCategories())
                list.Add(category, _subcategoryProvider.GetSubcategoriesByCategoryId(category.Id));

            return PartialView("_Categories", list);
        }

        public ActionResult Product(int id = 0)
        {
            ProductItemViewModel model;
            if (id > 0)
                model = _productProvider.GetProductById(id);
            else
                model = new ProductItemViewModel();

            return View(model);
        }
    }
}