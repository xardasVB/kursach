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
    public class SubcategoryProvider : ISubcategoryProvider
    {
        private readonly ISqlRepository _sqlRepository;

        public SubcategoryProvider(ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public StatusViewModel CreateSubcategory(SubcategoryInterfereViewModel Subcategory)
        {
            IEnumerable<string> SubcategoryNames = GetSubcategories()
                .Where(s => s.CategoryId == Subcategory.CategoryId)
                .Select(s => s.Name);

            if (SubcategoryNames.Contains(Subcategory.Name))
                return StatusViewModel.Dublication;

            _sqlRepository.Insert<Subcategory>(new Subcategory
            {
                Name = Subcategory.Name,
                CategoryId = Subcategory.CategoryId
            });
            _sqlRepository.SaveChanges();

            return StatusViewModel.Success;
        }

        public StatusViewModel DeleteSubcategory(int id)
        {
            Subcategory Subcategory = _sqlRepository.GetById<Subcategory>(id);
            _sqlRepository.Delete<Subcategory>(Subcategory);
            _sqlRepository.SaveChanges();

            return StatusViewModel.Success;
        }

        public StatusViewModel EditSubcategory(SubcategoryInterfereViewModel Subcategory, int id)
        {
            List<string> SubcategoryNames = GetSubcategories()
                .Where(s => s.CategoryId == Subcategory.CategoryId)
                .Select(s => s.Name)
                .ToList();
            SubcategoryNames.Remove(_sqlRepository.GetById<Subcategory>(id).Name);

            if (SubcategoryNames.Contains(Subcategory.Name))
                return StatusViewModel.Dublication;

            _sqlRepository.Update<Subcategory>(new Subcategory
            {
                Id = id,
                Name = Subcategory.Name,
                CategoryId = Subcategory.CategoryId
            });
            _sqlRepository.SaveChanges();
            return StatusViewModel.Success;
        }

        public SubcategoryItemViewModel GetSubcategoryById(int id)
        {
            Subcategory Subcategory = _sqlRepository.GetAll<Subcategory>().FirstOrDefault(s => s.Id == id);
            return new SubcategoryItemViewModel
            {
                Id = Subcategory.Id,
                Name = Subcategory.Name,
                Category = Subcategory.Category.Name,
                CategoryId = Subcategory.CategoryId,
                ProductCount = Subcategory.Products.Count
            };
        }

        public IEnumerable<SubcategoryItemViewModel> GetSubcategories()
        {
            return _sqlRepository
                .GetAll<Subcategory>()
                .Select(s => new SubcategoryItemViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category.Name,
                    CategoryId = s.CategoryId,
                    ProductCount = s.Products.Count
                });
        }

        public SubcategoryViewModel GetSubcategoriesByPage(int page, int items, string searchName)
        {
            IEnumerable<Subcategory> query = _sqlRepository.GetAll<Subcategory>();
            SubcategoryViewModel model = new SubcategoryViewModel();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(c => c.Name.ToLower().Contains(searchName.ToLower()));
            }

            model.Subcategories = query
                .Skip((page - 1) * items)
                .Take(items)
                .Select(s => new SubcategoryItemViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category.Name,
                    CategoryId = s.CategoryId,
                    ProductCount = s.Products.Count
                }).ToList();
            model.TotalPages = (int)Math.Ceiling((double)query.Count() / items);
            model.CurrentPage = page;
            model.SearchName = searchName;
            model.Items = items;

            return model;
        }

        public IEnumerable<SubcategoryItemViewModel> GetSubcategoriesByCategoryId(int id)
        {
            return _sqlRepository
                .GetAll<Subcategory>()
                .Where(s => s.CategoryId == id)
                .Select(s => new SubcategoryItemViewModel
                {
                    Name = s.Name,
                    Id = s.Id,
                    CategoryId = s.Id,
                    ProductCount = s.Products.Count
                });
        }
    }
}
