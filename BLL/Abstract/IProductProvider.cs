using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IProductProvider
    {
        ProductViewModel GetProductsByPage(int page, int items, ProductSearchViewModel searchName);
        IEnumerable<ProductItemViewModel> GetProducts();
        ProductItemViewModel GetProductById(int id);
        StatusViewModel DeleteProduct(int id);
        StatusViewModel CreateProduct(ProductInterfereViewModel Product);
        StatusViewModel EditProduct(ProductInterfereViewModel Product, int id);
    }
}
