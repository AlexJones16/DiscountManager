using DiscountManager.Domain.Models;
using DiscountManager.Infrastructure.Enteties;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManager.Infrastructure.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<Product>> Get();
        Task<Guid> Create(ProductDTO product);
        Task<Guid> Update(Guid id, decimal price);
        Task<Guid> Delete(Guid id);
    }
}
