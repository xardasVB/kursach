using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IBasketRepository
    {
        Basket AddBasket(Basket basket);
        Basket UpdateBasket(Basket basket);
        IQueryable<Basket> GetBaskets();
        Basket GetBasketById(int id);
    }
}
