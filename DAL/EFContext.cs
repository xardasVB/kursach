using DAL.Abstract;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EFContext : IdentityDbContext<AppUser>, IEFContext
    {
        public EFContext() : base("OnlineShopDB")
        {
            Database.SetInitializer<EFContext>(null);
        }

        public EFContext(string connString) : base(connString)
        {
            Database.SetInitializer<EFContext>(new DBInitializer());
        }

        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ShoppingHistory> ShoppingHistories { get; set; }
        public virtual DbSet<Subcategory> Subcategories { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry<TEntity>(entity);
        }
    }
}
