using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exchange.Domain.Domain.Domain.Enums;

namespace VirtualMind.Exchange.Application.Services.Settings.External
{
    public class ExternalServiceUrl
    {
        public ISOCode ISOCode { get; set; }
        public string Url { get; set; } 
    }
}
