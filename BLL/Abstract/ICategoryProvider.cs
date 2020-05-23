using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ICategoryProvider
    {
        CategoryViewModel GetCategoriesByPage(int page, int items, string searchName);
        IEnumerable<CategoryItemViewModel> GetCategories();
        CategoryItemViewModel GetCategoryById(int id);
        StatusViewModel DeleteCategory(int id);
        StatusViewModel CreateCategory(CategoryInterfereViewModel Category);
        StatusViewModel EditCategory(CategoryInterfereViewModel Category, int id);
    }
}
