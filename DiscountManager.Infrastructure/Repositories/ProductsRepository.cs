using DiscountManager.Domain.Models;
using DiscountManager.Infrastructure.Enteties;
using DiscountManager.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManager.Infrastructure.Repositories
{
    public class ProductsRepository:IProductsRepository
    {
        private readonly ProductDbContext _productDbContext;
        public ProductsRepository(ProductDbContext productDbContext)
        {
            try
            {
                _productDbContext = productDbContext;
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                throw;
            }
        }

        public async Task<List<Product>> Get()
        {
            var productEntity = await _productDbContext.ProductsAll
                .AsNoTracking()
                .ToListAsync();

            var products = productEntity
                .Select(p => new Product { ProductID = p.ProductID, Name = p.Name, Price = p.Price, Provider = p.Provider })
                .ToList();

            return products;
        }
        public async Task<Guid> Create(ProductDTO product)
        {
            var productEntity = new Product
            { 
                Id = new Guid(),
                ProductID = product.ProductID,
                BrandID = product.BrandID,
                SKU = product.SKU,
                Provider = product.Provider,    
                Name = product.Name,
                Url = product.Url,
                Price = product.Price,  
                Color = product.Color,
                Sizes = product.Sizes
            };

            await _productDbContext.ProductsAll.AddAsync(productEntity);
            await _productDbContext.SaveChangesAsync();

            return productEntity.Id;
        }

        public async Task<Guid> Update(Guid id, decimal price)
        {
            await _productDbContext.ProductsAll
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(p => p
                .SetProperty(x => x.Price, x => price));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _productDbContext.ProductsAll
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
