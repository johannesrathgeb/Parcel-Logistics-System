using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BusinessLogicException : Exception
    {
        public string Id { get; set; }
        public BusinessLogicException()
        {

        }
        public BusinessLogicException(string id, string message, Exception innerException) : base(message, innerException)
        {

        }
        public BusinessLogicException(string id, string message) : base(message)
        {

        }
    }
}
