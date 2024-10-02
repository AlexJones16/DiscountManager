using DiscountManager.Infrastructure.Interfaces;
using DiscountManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManager.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("SQL:ConnectionString");
            services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IProductsRepository, ProductsRepository>();
            return services;
        }
    }
}
