using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.ServiceAgents.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ServiceAgentsException : Exception
    {
        public string Id { get; set; }

        public ServiceAgentsException(string id, string message, Exception innerException) : base(message, innerException)
        {

        }
        public ServiceAgentsException(string id, string message) : base(message)
        {

        }
    }
}
