using DiscountManager.Domain.Models;
using DiscountManager.Infrastructure.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManager.Infrastructure.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Guid> CreateProduct(ProductDTO product);
        Task<Guid> UpdateProduct(Guid id, decimal price);
        Task<Guid> DeleteProduct(Guid id);
    }
}
