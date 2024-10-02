using DiscountManager.Application.HostedServices;
using DiscountManager.Application.Parsers;
using DiscountManager.Application.Services;
using DiscountManager.Infrastructure.Interfaces;
using DiscountManager.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManager.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<SinsayParser>();
            services.AddSingleton<MegasportParser>();
            services.AddSingleton<ProviderFabric>();
            services.AddScoped<IProductService,ProductService>();
            services.AddHostedService<ProductBackgroundService>();
            return services;
        }
    }
}
