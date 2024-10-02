using DiscountManager.Domain.Models;
using DiscountManager.Infrastructure.Enteties;
using DiscountManager.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManager.Application.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<List<Product>>GetAllProducts()
        {
            return await _productsRepository.Get();
        }
        public async Task<Guid> CreateProduct(ProductDTO product)
        {
            return await _productsRepository.Create(product);  
        }
        public async Task<Guid> UpdateProduct(Guid id,decimal price)
        {
            return await _productsRepository.Update(id,price);
        }
        public async Task<Guid> DeleteProduct(Guid id)
        {
            return await _productsRepository.Delete(id);
        }
    }
}
