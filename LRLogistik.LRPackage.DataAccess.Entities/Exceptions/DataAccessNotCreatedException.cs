using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class DataAccessNotCreatedException : DataAccessException
    {
        public DataAccessNotCreatedException(string id, string message, Exception innerException) : base(id, message, innerException)
        {

        }
        public DataAccessNotCreatedException(string id, string message) : base(id, message)
        {

        }
    }
}
