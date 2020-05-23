using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ISubcategoryRepository
    {
        Subcategory AddSubcategory(Subcategory Subcategory);
        Subcategory UpdateSubcategory(Subcategory Subcategory);
        IQueryable<Subcategory> GetSubcategories();
        Subcategory GetSubcategoryById(int id);
    }
}
