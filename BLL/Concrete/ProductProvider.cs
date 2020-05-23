using BLL.Abstract;
using BLL.Models;
using DAL.Abstract;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class ProductProvider : IProductProvider
    {
        private static Random rng = new Random();
        private readonly ISqlRepository _sqlRepository;

        public ProductProvider(ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public StatusViewModel CreateProduct(ProductInterfereViewModel Product)
        {
            _sqlRepository.Insert<Product>(new Product
            {
                Name = Product.Name,
                Code = Product.Code,
                Image = Product.Image,
                Price = Product.Price,
                PriceCMV = Product.PriceCMV,
                Description = Product.Description,
                StockCount = Product.StockCount,
                SubcategoryId = Product.SubcategoryId,
                ManufacturerId = Product.ManufacturerId
            });
            _sqlRepository.SaveChanges();

            return StatusViewModel.Success;
        }

        public StatusViewModel DeleteProduct(int id)
        {
            Product Product = _sqlRepository.GetById<Product>(id);
            _sqlRepository.Delete<Product>(Product);
            _sqlRepository.SaveChanges();

            return StatusViewModel.Success;
        }

        public StatusViewModel EditProduct(ProductInterfereViewModel Product, int id)
        {
            _sqlRepository.Update<Product>(new Product
            {
                Name = Product.Name,
                Code = Product.Code,
                Image = Product.Image,
                Price = Product.Price,
                PriceCMV = Product.PriceCMV,
                Description = Product.Description,
                StockCount = Product.StockCount,
                SubcategoryId = Product.SubcategoryId,
                ManufacturerId = Product.ManufacturerId
            });
            _sqlRepository.SaveChanges();
            return StatusViewModel.Success;
        }

        public ProductItemViewModel GetProductById(int id)
        {
            Product Product = _sqlRepository.GetProducts().FirstOrDefault(c => c.Id == id);
            return new ProductItemViewModel
            {
                Id = Product.Id,
                Name = Product.Name,
                Code = Product.Code,
                Image = Product.Image,
                Price = Product.Price,
                PriceCMV = Product.PriceCMV,
                Description = Product.Description,
                StockCount = Product.StockCount,
                SubcategoryId = Product.SubcategoryId,
                Subcategory = Product.Subcategory.Category.Name + "(" + Product.Subcategory.Name + ")",
                ManufacturerId = Product.ManufacturerId,
                Manufacturer = Product.Manufacturer.Name + "(" + Product.Manufacturer.Country.Name + ")"
            };
        }

        public IEnumerable<ProductItemViewModel> GetProducts()
        {
            return _sqlRepository.
                 GetProducts()
                .Select(p => new ProductItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Image = p.Image,
                    Price = p.Price,
                    PriceCMV = p.PriceCMV,
                    Description = p.Description,
                    StockCount = p.StockCount,
                    SubcategoryId = p.SubcategoryId,
                    Subcategory = p.Subcategory.Category.Name + "(" + p.Subcategory.Name + ")",
                    ManufacturerId = p.ManufacturerId,
                    Manufacturer = p.Manufacturer.Name + "(" + p.Manufacturer.Country.Name + ")"
                });
        }

        public ProductViewModel GetProductsByPage(int page, int items, ProductSearchViewModel searchModel)
        {
            IEnumerable<Product> query = _sqlRepository.GetProducts();
            ProductViewModel model = new ProductViewModel();

            if (page == 0 && items == 0)
            {
                page = 1;
                items = query.Count();
            }

            FilterRequest(ref query, searchModel);

            model.Products = Shuffle(query
                .Skip((page - 1) * items)
                .Take(items)
                .Select(p => new ProductItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Image = p.Image,
                    Price = p.Price,
                    PriceCMV = p.PriceCMV,
                    Description = p.Description,
                    StockCount = p.StockCount,
                    SubcategoryId = p.SubcategoryId,
                    Subcategory = p.Subcategory.Category.Name + "(" + p.Subcategory.Name + ")",
                    ManufacturerId = p.ManufacturerId,
                    Manufacturer = p.Manufacturer.Name + "(" + p.Manufacturer.Country.Name + ")"
                }).ToList()).ToList();
            model.TotalPages = (int)Math.Ceiling((double)query.Count() / items);
            model.CurrentPage = page;
            model.Search = searchModel;
            model.Items = items;

            return model;
        }

        void FilterRequest(ref IEnumerable<Product> query, ProductSearchViewModel searchModel)
        {
            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.Name))
                {
                    query = query.Where(c => c.Name.ToLower().Contains(searchModel.Name.ToLower())
                    || c.Manufacturer.Name.ToLower().Contains(searchModel.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(searchModel.Subcategory))
                    query = query.Where(c => c.Subcategory.Name == searchModel.Subcategory);
                if (!string.IsNullOrEmpty(searchModel.Category))
                    query = query.Where(c => c.Subcategory.Category.Name == searchModel.Category);
                if (searchModel.Order == OrderBy.PriceAscending)
                    query = query.OrderBy(c => c.Price);
                else if (searchModel.Order == OrderBy.PriceDescending)
                    query = query.OrderByDescending(c => c.Price);
            }
        }

        private static IList<T> Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
    }
}
