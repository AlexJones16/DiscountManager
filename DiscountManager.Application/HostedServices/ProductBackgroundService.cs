using DiscountManager.Domain.Models;
using DiscountManager.Infrastructure.Interfaces;
using DiscountManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManager.Application.HostedServices
{
    public class ProductBackgroundService : BackgroundService, IProviderParser
    {
        private readonly ProviderFabric _providerFabric;
        private readonly IServiceProvider _serviceProvider;
        private List<string> _providers;
        public ProductBackgroundService(ProviderFabric providerFabric,IServiceProvider serviceProvider)
        {  
            _providerFabric = providerFabric;
            _providers = new List<string> { "sinsay.com", "megasport.com" };
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await GetAllProduct();
                }
                catch
                {
                    throw new Exception("Cant execute GetAllProduct() ");
                }
                finally
                {
                    await Task.Delay(25000);
                }
            }
        }
        public async Task<List<ProductDTO>> GetAllProduct()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            using (var scope = _serviceProvider.CreateScope())
            {
                var productsRepository = scope.ServiceProvider.GetRequiredService<IProductsRepository>();
                foreach (var provider in _providers)
                {
                    var parser = _providerFabric.Create(provider);
                    products.AddRange(await parser.GetAllProduct());
                }
                foreach (var product in products)
                {
                    if (product is null)
                    {
                        throw new Exception("Product can`t be null");
                    }
                    await productsRepository.Create(product);
                }
                return products;
            }
        }
    }
}
