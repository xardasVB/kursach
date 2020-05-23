using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IShoppingHistoryRepository
    {
        ShoppingHistory AddShoppingHistory(ShoppingHistory ShoppingHistory);
        ShoppingHistory UpdateShoppingHistory(ShoppingHistory ShoppingHistory);
        IQueryable<ShoppingHistory> GetShoppingHistorys();
        ShoppingHistory GetShoppingHistoryById(int id);
    }
}
