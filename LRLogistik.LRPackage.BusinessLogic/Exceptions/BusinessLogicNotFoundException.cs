using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BusinessLogicNotFoundException : BusinessLogicException
    {
        public BusinessLogicNotFoundException()
        {

        }
        public BusinessLogicNotFoundException(string id, string message) : base(id, message)
        {
        }

        public BusinessLogicNotFoundException(string id, string message, Exception innerException) : base(id, message, innerException)
        {
        }
    }
}
