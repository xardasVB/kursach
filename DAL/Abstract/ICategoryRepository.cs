using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICategoryRepository
    {
        Category AddCategory(Category category);
        Category UpdateCategory(Category category);
        IQueryable<Category> GetCategories();
        Category GetCategoryById(int id);
    }
}
