using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscountManager.Domain.Providers
{
    public class MegasportSearchResult
    {
        [JsonPropertyName("data")]
        public MegasportSearchResultProducts Result { get; set; }
    }
    public class MegasportSearchResultProducts
    {
        [JsonPropertyName("otherColorsProducts")]
        public List<MegasportSearchResultProduct> Products { get; set; }
    }
    public class MegasportSearchResultProduct
    {
        [JsonPropertyName("id")]
        public int ProductID { get; set; }
        [JsonPropertyName("brandId")]
        public int BrandID { get; set; }
        [JsonPropertyName("articul")]
        public string Articul { get; set; }
        [JsonPropertyName("discountPrice")]
        public string? DiscountPrice { get; set; }
        [JsonPropertyName("discountSale")]
        public int? DiscountSale { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("metadescr")]
        public string Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("unique")]
        public string Url { get; set; }
        [JsonPropertyName("colorsFull")]
        public MegasportAvailableColor ColorResult { get; set; }
        [JsonPropertyName("sizes")]
        public Dictionary<string, int> Sizes { get; set; }
    }
    public class MegasportAvailableColor
    {
        [JsonPropertyName("colors")]
        public List<string> Colors { get; set; }
    }
}
