using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ISubcategoryProvider
    {
        SubcategoryViewModel GetSubcategoriesByPage(int page, int items, string searchName);
        IEnumerable<SubcategoryItemViewModel> GetSubcategories();
        IEnumerable<SubcategoryItemViewModel> GetSubcategoriesByCategoryId(int id);
        SubcategoryItemViewModel GetSubcategoryById(int id);
        StatusViewModel DeleteSubcategory(int id);
        StatusViewModel CreateSubcategory(SubcategoryInterfereViewModel Subcategory);
        StatusViewModel EditSubcategory(SubcategoryInterfereViewModel Subcategory, int id);
    }
}
