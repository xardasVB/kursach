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
    public class CategoryProvider : ICategoryProvider
    {
        private readonly ISqlRepository _sqlRepository;

        public CategoryProvider(ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public StatusViewModel CreateCategory(CategoryInterfereViewModel Category)
        {
            IEnumerable<string> CategoryNames = GetCategories()
                .Select(c => c.Name);

            if (CategoryNames.Contains(Category.Name))
                return StatusViewModel.Dublication;

            _sqlRepository.Insert<Category>(new Category
            {
                Name = Category.Name
            });
            _sqlRepository.SaveChanges();
            return StatusViewModel.Success;
        }

        public StatusViewModel DeleteCategory(int id)
        {
            Category Category = _sqlRepository.GetById<Category>(id);
            _sqlRepository.Delete<Category>(Category);
            _sqlRepository.SaveChanges();

            return StatusViewModel.Success;
        }

        public StatusViewModel EditCategory(CategoryInterfereViewModel Category, int id)
        {
            List<string> CategoryNames = GetCategories()
                .Select(c => c.Name)
                .ToList();
            CategoryNames.Remove(_sqlRepository.GetById<Category>(id).Name);

            if (CategoryNames.Contains(Category.Name))
                return StatusViewModel.Dublication;

            _sqlRepository.Update<Category>(new Category
            {
                Id = id,
                Name = Category.Name
            });
            _sqlRepository.SaveChanges();
            return StatusViewModel.Success;
        }

        public CategoryViewModel GetCategoriesByPage(int page, int items, string searchName)
        {
            IEnumerable<Category> query = _sqlRepository.GetAll<Category>();
            CategoryViewModel model = new CategoryViewModel();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(c => c.Name.ToLower().Contains(searchName.ToLower()));
            }

            model.Categories = query
                .Skip((page - 1) * items)
                .Take(items)
                .Select(c => new CategoryItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductCount = c.Subcategories.Count == 0 ? 0 : c.Subcategories.Sum(s => s.Products.Count)
                }).ToList();
            model.TotalPages = (int)Math.Ceiling((double)query.Count() / items);
            model.CurrentPage = page;
            model.SearchName = searchName;
            model.Items = items;

            return model;
        }

        public IEnumerable<CategoryItemViewModel> GetCategories()
        {
            return _sqlRepository
                .GetAll<Category>()
                .Select(c => new CategoryItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductCount = c.Subcategories.Count == 0 ? 0 : c.Subcategories.Sum(s => s.Products.Count)
                });
        }

        public CategoryItemViewModel GetCategoryById(int id)
        {
            Category Category = _sqlRepository.GetAll<Category>().FirstOrDefault(c => c.Id == id);
            return new CategoryItemViewModel
            {
                Id = Category.Id,
                Name = Category.Name,
                ProductCount = Category.Subcategories.Count == 0 ? 0 : Category.Subcategories.Sum(s => s.Products.Count)
            };
        }
    }
}
