using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rookie.Ecom.MetaShop.Business.Interfaces;
using Rookie.Ecom.MetaShop.Business.Services;
using Rookie.Ecom.MetaShop.DataAccessor;
using Rookie.Ecom.MetaShop.Identity;
using System;
using System.Reflection;

namespace Rookie.Ecom.MetaShop.Business
{
    public static class ServiceRegister
    {
        public static void AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataAccessorLayer(configuration);
            services.AddIdentityLayer(configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductPictureService, ProductPictureService>();
            services.AddTransient<IMetaIdentityUserService, MetaIdentityUserService>();

        }
    }
}
