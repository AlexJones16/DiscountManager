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
    public class MegasportParser : IProviderParser
    {
        private readonly HttpClient _httpClient;
        private readonly string ProviderName = "Megasport";

        public MegasportParser()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<ProductDTO>> GetAllProduct()
        {
            string url = "https://megasport.ua/api/filters-service/?%2A=page-3%2F&language=ru&page=3&genders%5B%5D=male&offset=136&limit=68&language=ru";
            string content = await GetPageAsync(url);
            if (content is null)
            {
                throw new Exception($"Не удалось получить ответ от {ProviderName}");
            }
            var json = JsonSerializer.Deserialize<MegasportSearchResult>(content);
            if (json is null)
            {
                throw new Exception($"Не удалось прочитать json ответ {ProviderName}");
            }
            List<ProductDTO> result = await ConvertToDTO(json.Result.Products);
            return result;
        }
        public async Task<List<ProductDTO>> ConvertToDTO(List<MegasportSearchResultProduct> products)
        {
            try
            {
                List<ProductDTO> result = new();
                foreach (var product in products)
                {
                    //var exists = result.Find(x => x.BrandID == product.BrandID);  
                    //List<string> colors = new List<string>();
                    //if (exists != null)
                    //{
                    //    exists.Colors.Add(product.ColorResult.Colors.FirstOrDefault()); 
                    //}
                    var dto = new ProductDTO
                    {
                        ProductID = product.ProductID,
                        SKU = product.Articul,
                        Provider = this.ProviderName,
                        Name = product.Description,
                        Url = ($"https://megasport.ua/ru/product/{product.Url}"),
                        Price = ProcessPrice(Convert.ToString(product.Price)),
                        Color = product.ColorResult.Colors.FirstOrDefault(),
                        Sizes = product.Sizes.Select(x => x.Key).ToList()
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
        public static decimal ProcessPrice(string price)
        {
            if (price == null)
            {
                return 0;
            }
            bool success = decimal.TryParse(price, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result);
            return success ? (decimal)result : 0;
        }
        public Task<ProductDTO> GetProductDTO(string productUrl)
        {
            throw new NotImplementedException();
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
    }
}
