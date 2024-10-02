using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DiscountManager.Infrastructure.Enteties
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public int? ProductID { get; set; }
        public int? BrandID { get; set; }
        public string? SKU { get; set; }
        public string? Provider { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public decimal? Price { get; set; }
        public string? Color { get; set; }
        public List<string>? Sizes { get; set; }
    }
}
