using AutoMapper.Configuration;
using DevIO.App.Extensions;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using DioIO.Business.Interface;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(
            this IServiceCollection services)
        {
            services.AddScoped<MyDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, 
                CurrencyAttributeAdapterProvider>();

            return services;
        }
    }
}
