using BLL.Abstract;
using BLL.Models;
using OnlineShop.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize(Users = "besarabvitaliy@gmail.com,asd@asd.asd")]
    public class ProductController : Controller
    {
        private readonly IProductProvider _ProductProvider;
        private readonly ISubcategoryProvider _SubcategoryProvider;
        private readonly IManufacturerProvider _ManufacturerProvider;

        public ProductController(IProductProvider ProductProvider,
            ISubcategoryProvider SubcategoryProvider,
            IManufacturerProvider ManufacturerProvider)
        {
            _ProductProvider = ProductProvider;
            _SubcategoryProvider = SubcategoryProvider;
            _ManufacturerProvider = ManufacturerProvider;
            ViewBag.MenuProduct = true;
        }

        // GET: Product
        public ActionResult Index(int page = 1, int items = 10, ProductSearchViewModel searchModel = null)
        {
            return View(_ProductProvider.GetProductsByPage(page, items, searchModel));
        }

        public ActionResult Create()
        {
            ProductInterfereViewModel model = new ProductInterfereViewModel();
            InitializeSubcategories(ref model);
            InitializeManufacturers(ref model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductInterfereViewModel model)
        {
            if (ModelState.IsValid)
            {
                string dir = Server.MapPath("~") + "Images\\Product\\";
                int widthBig = 2560;
                // Імя фалу із зображенням
                string filePhoto = string.Empty;
                if (model.Image != null)
                {
                    filePhoto = Guid.NewGuid().ToString() + ImageWorker.GetImageType(model.Image);
                    byte[] bytesPhoto = ImageWorker.GetImageBytesFromBase64(model.Image);
                    string imageType = ImageWorker.GetImageType(model.Image);
                    Bitmap imageBig = ImageWorker.SaveImageFromBytesTry(bytesPhoto, widthBig);
                    if (imageBig != null)
                    {
                        string fileName = widthBig + "_" + filePhoto;
                        string pathSaveFile = Path.Combine(dir, fileName);
                        imageBig.Save(pathSaveFile, ImageWorker.GetImageFormat(imageType));
                        model.Image = fileName;
                    }
                }
                _ProductProvider.CreateProduct(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _ProductProvider.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ProductInterfereViewModel model = new ProductInterfereViewModel();
            InitializeSubcategories(ref model);
            InitializeManufacturers(ref model);
            var Product = _ProductProvider.GetProductById(id);
            model.Name = Product.Name;
            model.Image = Product.Image;
            model.Price = Product.Price;
            model.Code = Product.Code;
            model.StockCount = Product.StockCount;
            model.SubcategoryId = Product.SubcategoryId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductInterfereViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                string dir = Server.MapPath("~") + "Images\\Product\\";
                int widthBig = 2560;
                // Імя фалу із зображенням
                string filePhoto = string.Empty;
                filePhoto = Guid.NewGuid().ToString() + ImageWorker.GetImageType(model.Image);
                if (model.Image != null)
                {
                    byte[] bytesPhoto = ImageWorker.GetImageBytesFromBase64(model.Image);
                    string imageType = ImageWorker.GetImageType(model.Image);
                    Bitmap imageBig = ImageWorker.SaveImageFromBytesTry(bytesPhoto, widthBig);
                    if (imageBig != null)
                    {
                        string fileName = widthBig + "_" + filePhoto;
                        string pathSaveFile = Path.Combine(dir, fileName);
                        imageBig.Save(pathSaveFile, ImageWorker.GetImageFormat(imageType));
                        model.Image = fileName;
                    }
                }
                _ProductProvider.EditProduct(model, id);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult GetProductImage(int id)
        {
            var product = _ProductProvider.GetProductById(id);
            ImageViewModel image = new ImageViewModel();

            if (product.Image != null)
                image.ImageName = product.Image;

            return PartialView("_ProductImage", image);
        }

        private void InitializeSubcategories(ref ProductInterfereViewModel model)
        {
            model.Subcategories = _SubcategoryProvider
            .GetSubcategories()
            .Select(c => new SelectItemViewModel<int>
            {
                Id = c.Id,
                Name = c.Category + "(" + c.Name + ")"
            }).ToList();
        }

        private void InitializeManufacturers(ref ProductInterfereViewModel model)
        {
            model.Manufacturers = _ManufacturerProvider
            .GetManufacturers()
            .Select(c => new SelectItemViewModel<int>
            {
                Id = c.Id,
                Name = c.Name + "(" + c.Country + ")"
            }).ToList();
        }
    }
}