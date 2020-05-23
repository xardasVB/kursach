using DAL.Abstract;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class SqlRepository : ISqlRepository
    {
        protected readonly IEFContext _context;
        public SqlRepository(IEFContext context)
        {
            _context = context;
        }

        private IDbSet<TEntity> GetEntities<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            GetEntities<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            GetEntities<TEntity>().Remove(entity);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return GetEntities<TEntity>();
        }

        public TEntity GetById<TEntity>(int id) where TEntity : BaseModel<int>
        {
            return GetEntities<TEntity>().SingleOrDefault(item => item.Id == id);
        }

        public TEntity GetById<TEntity>(string id) where TEntity : BaseModel<string>
        {
            return GetEntities<TEntity>().SingleOrDefault(item => item.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseModel<int>
        {
            _context.Entry(_context.Set<TEntity>().SingleOrDefault(t => t.Id == entity.Id)).CurrentValues.SetValues(entity);
        }

        public IQueryable<Product> GetProducts()
        {
            return _context.Set<Product>()
                .Include(p => p.Subcategory)
                .Include(p => p.Manufacturer)
                .Include(p => p.Subcategory.Category)
                .Include(p => p.Manufacturer.Country);
        }
    }
}
