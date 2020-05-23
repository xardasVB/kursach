using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IEFContext _context;

        public CategoryRepository(IEFContext context)
        {
            _context = context;
        }

        public Category AddCategory(Category category)
        {
            _context.Set<Category>().Add(category);
            _context.SaveChanges();
            return category;
        }

        public IQueryable<Category> GetCategories()
        {
            return _context.Set<Category>();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Set<Category>().SingleOrDefault(c => c.Id == id);
        }

        public Category UpdateCategory(Category category)
        {
            var _category = _context.Set<Category>().SingleOrDefault(c => c.Id == category.Id);
            _category = category;
            _context.SaveChanges();
            return category;
        }
    }
}
