using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IProductRepository
    {
        Product AddProduct(Product Product);
        Product UpdateProduct(Product Product);
        IQueryable<Product> GetProducts();
        Product GetProductById(int id);
    }
}
