using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.ServiceAgents.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ServiceAgentsNotFoundException : ServiceAgents.Exceptions.ServiceAgentsException
    {
        public ServiceAgentsNotFoundException(string id, string message) : base(id, message)
        {
        }

        public ServiceAgentsNotFoundException(string id, string message, Exception innerException) : base(id, message, innerException)
        {
        }
    }
}
