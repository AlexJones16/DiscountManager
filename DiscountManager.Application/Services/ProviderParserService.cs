using DiscountManager.Domain.Models;
using DiscountManager.Infrastructure.Interfaces;
using DiscountManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManager.Application.Services
{
    public class ProviderParserService : IProviderParser
    {
        private readonly ProductsRepository _productsRepository;
        private readonly ProviderFabric _providerFabric;
        private List<string> _providers;
        public ProviderParserService(ProviderFabric providerFabric,ProductsRepository productsRepository)
        {
            _providerFabric = providerFabric;
            _productsRepository = productsRepository;
            _providers = new List<string> { "sinsay","megasport" };
        }
        public async Task<List<ProductDTO>> GetAllProduct()
        {
           List<ProductDTO> products = new List<ProductDTO>();    
           foreach(var provider in _providers)
           {               
                var parser = _providerFabric.Create(provider);
                products.AddRange(await parser.GetAllProduct());
           }
            foreach (var product in products)
            {
                if (product is null)
                {
                    throw new Exception("");
                }
                await _productsRepository.Create(product);
            }
           return products;
        }
    }
}
