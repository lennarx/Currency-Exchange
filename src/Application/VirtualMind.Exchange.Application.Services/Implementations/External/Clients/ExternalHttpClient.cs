using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exchange.Application.Services.Contracts.External.Clients;

namespace VirtualMind.Exchange.Application.Services.Implementations.External.Clients
{
    public class ExternalHttpClient : IExternalHttpClient
    {
        private readonly HttpClient _httpClient;
        public ExternalHttpClient(HttpClient client) 
        {
            _httpClient = client;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }
    }
}
