using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class DataAccessException : Exception
    {
        public string Id { get; set; }

        public DataAccessException(string id, string message, Exception innerException) : base(message, innerException)
        {
            
        }
        public DataAccessException(string id, string message) : base(message)
        {

        }
    }
}
