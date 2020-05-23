using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IOrderRepository
    {
        Order AddOrder(Order Order);
        Order UpdateOrder(Order Order);
        IQueryable<Order> GetOrders();
        Order GetOrderById(int id);
    }
}
