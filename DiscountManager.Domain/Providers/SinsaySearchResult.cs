using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscountManager.Domain.Providers
{
    public class SinsaySearchResult
    {
        [JsonPropertyName("products")]
        public List<SinsayArchResult> archResults { get; set; }
        [JsonPropertyName("productsTotalAmount")]
        public int Total { get; set; }
        [JsonPropertyName("productsAmount")]
        public int ProductsShowed { get; set; }
    }
    public class SinsayArchResult
    {
        [JsonPropertyName("id")]
        public string ProductID { get; set; }
        [JsonPropertyName("name")]
        public string ProductName { get; set; }
        [JsonPropertyName("has_discount")]
        public bool DiscountStatus { get; set; }
        [JsonPropertyName("sku")]
        public string SKU { get; set; }
        [JsonPropertyName("price")]
        public string PriceNoDiscount { get; set; }
        [JsonPropertyName("final_price")]
        public string PriceWithDiscount { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("sizes")]
        public List<SinsayArchAvailableSizesResult> Sizes { get; set; }
        [JsonPropertyName("colorOptions")]
        public List<SinsayArchAvailableColor> Colors { get; set; }

    }
    public class SinsayArchAvailableSizesResult
    {
        [JsonPropertyName("sizeName")]
        public string Size { get; set; }
        [JsonPropertyName("stock")]
        public bool InStock { get; set; }
    }
    public class SinsayArchAvailableColor
    {
        [JsonPropertyName("color")]
        public SinsayArchAvailableColorResult ColorResult { get; set; }
    }
    public class SinsayArchAvailableColorResult
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("photo")]
        public string Photo { get; set; }
    }
}
