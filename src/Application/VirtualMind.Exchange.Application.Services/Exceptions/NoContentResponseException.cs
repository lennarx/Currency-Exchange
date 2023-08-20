using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMind.Exchange.Application.Services.Exceptions
{
    public class NoContentResponseException : Exception
    {
        public NoContentResponseException(string url) : base($"No content was returned from the url: {url}") { }
    }
}
