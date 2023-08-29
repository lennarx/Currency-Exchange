using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMind.Exchange.Application.Services.Contracts.External.Clients
{
    public interface IExternalHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
