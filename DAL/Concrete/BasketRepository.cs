using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Concrete
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IEFContext _context;
        public BasketRepository(IEFContext context)
        {
            _context = context;
        }
        
        public Basket AddBasket(Basket basket)
        {
            _context.Set<Basket>().Add(basket);
            _context.SaveChanges();
            return basket;
        }

        public Basket GetBasketById(int id)
        {
            return _context.Set<Basket>().SingleOrDefault(b => b.Id == id);
        }

        public IQueryable<Basket> GetBaskets()
        {
            return _context.Set<Basket>();
        }

        public Basket UpdateBasket(Basket basket)
        {
            var _basket = _context.Set<Basket>().SingleOrDefault(c => c.Id == basket.Id);
            _basket = basket;
            _context.SaveChanges();
            return basket;
        }
    }
}
