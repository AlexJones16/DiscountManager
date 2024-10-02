using DiscountManager.Application.Parsers;
using DiscountManager.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManager.Application
{
    public class ProviderFabric
    {
        private readonly MegasportParser _megasportParser;
        private readonly SinsayParser _sinsayParser;
        public ProviderFabric(MegasportParser megasportParser, SinsayParser sinsayParser)
        {
            _megasportParser = megasportParser;
            _sinsayParser = sinsayParser;
        }
        public IProviderParser Create(string domain)
        {
            IProviderParser provider = domain switch
            {
                "sinsay.com" => _sinsayParser,
                "megasport.com" => _megasportParser,
                _ => throw new Exception("Provider doesnt support")
            };
            return provider;
        }
        public IProviderParser CreateFromUrl(string url)
        {
            string domain = new Uri(url).Host;
            return Create(domain);
        }
    }
}
