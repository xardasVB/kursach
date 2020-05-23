﻿using Autofac;
using BLL.Abstract;
using BLL.Concrete;
using BLL.Services.Identity;
using DAL;
using DAL.Abstract;
using DAL.Concrete;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services
{
    public class DataModule : Module
    {
        private string _connStr;
        private IAppBuilder _app;

        public DataModule(string connString, IAppBuilder app)
        {
            _connStr = connString;
            _app = app;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new EFContext(this._connStr)).As<IEFContext>().InstancePerRequest();
            builder.Register(ctx =>
            {
                var context = (EFContext)ctx.Resolve<IEFContext>();
                return context;
            }).AsSelf().InstancePerDependency();
            //builder.Register(c => new AppDBContext(this._connStr)).AsSelf().InstancePerRequest();
            builder.RegisterType<AppUserStore>().As<IUserStore<AppUser>>().InstancePerRequest();
            builder.RegisterType<AppUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<AppRoleManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<AppSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => _app.GetDataProtectionProvider()).InstancePerRequest();
            builder.RegisterType<AccountProvider>().AsSelf().InstancePerRequest();

            builder.RegisterType<SqlRepository>()
                .As<ISqlRepository>().InstancePerRequest();
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterType<AccountProvider>()
                .As<IAccountProvider>().InstancePerRequest();
            builder.RegisterType<CountryProvider>()
                .As<ICountryProvider>().InstancePerRequest();
            builder.RegisterType<ManufacturerProvider>()
                .As<IManufacturerProvider>().InstancePerRequest();
            builder.RegisterType<CategoryProvider>()
                .As<ICategoryProvider>().InstancePerRequest();
            builder.RegisterType<SubcategoryProvider>()
                .As<ISubcategoryProvider>().InstancePerRequest();
            builder.RegisterType<ProductProvider>()
                .As<IProductProvider>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
