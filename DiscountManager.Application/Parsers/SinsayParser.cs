using DiscountManager.Domain.Models;
using DiscountManager.Domain.Providers;
using DiscountManager.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscountManager.Application.Parsers
{
    public class SinsayParser : IProviderParser
    {
        private readonly HttpClient _httpClient;
        private readonly string ProviderName = "Sinsay";

        public SinsayParser()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<ProductDTO>> GetAllProduct()
        {
            string url = "https://arch.sinsay.com/api/1045/category/23741/products?filters%5BsortBy%5D=3&offset=200&pageSize=200&flags[enablediscountfilter]=true&flags[producttilewithpreview]=true&flags[quickshop]=true&flags[colorspreviewinfilters]=true&flags[couponstickerenabled]=true&flags[catalogcolorpicker]=1&flags[filterscounter]=1&flags[loadmorebutton]=true";
            string content = await GetPageAsync(url);
            if (content == null)
            {
                throw new Exception($"Не удалось получить ответ от {ProviderName}");
            }
            var json = JsonSerializer.Deserialize<SinsaySearchResult>(content);
            if (json == null)
            {
                throw new Exception($"Не удалось прочитать json ответ {ProviderName}");
            }
            List<ProductDTO> result = await ConvertToDTO(json.archResults);
            return result;
        }

        public async Task<List<ProductDTO>> ConvertToDTO(List<SinsayArchResult> products)
        {
            try
            {
                List<ProductDTO> result = new();
                foreach (var product in products)
                {
                    var dto = new ProductDTO
                    {
                        ProductID = ProductIDConvert(product.ProductID),
                        SKU = product.SKU,
                        Provider = this.ProviderName,
                        Name = product.ProductName,
                        Url = product.Url,
                        Price = ProcessPrice(Convert.ToString(product.PriceWithDiscount)),
                        Color = product.Colors.FirstOrDefault().ColorResult.Name,
                        Sizes = product.Sizes.Where(x => x.InStock == true).Select(x => x.Size).ToList()
                    };
                    result.Add(dto);
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
        public static int ProductIDConvert(string id)
        {
            bool success = int.TryParse(id, out var result);
            if (success)
            {
                return result;
            }
            return 0;
        }
        public static decimal ProcessPrice(string price)
        {
            if (price == null)
            {
                return 0;
            }
            bool success = decimal.TryParse(price, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result);
            return success ? (decimal)result : 0;
        }
        public async Task<string> GetPageAsync(string url)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
                    string content = await httpResponseMessage.Content.ReadAsStringAsync();
                    return content;
                }
            }
            catch
            {
                return null;
            }
        }

        public Task<ProductDTO> GetProductDTO(string productUrl)
        {
            throw new NotImplementedException();
        }
    }
}
