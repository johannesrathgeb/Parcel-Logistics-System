using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class DataAccessNotFoundException : DataAccessException
    {
        

        public DataAccessNotFoundException(string id, string message, Exception innerException) : base(id, message, innerException)
        {
            
        }
        public DataAccessNotFoundException(string id, string message) : base(id, message)
        {

        }
    }
}
