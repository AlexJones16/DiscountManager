using DiscountManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManager.Infrastructure.Interfaces
{
    public interface IProviderParser
    {
        public Task<List<ProductDTO>> GetAllProduct();
    }
}
