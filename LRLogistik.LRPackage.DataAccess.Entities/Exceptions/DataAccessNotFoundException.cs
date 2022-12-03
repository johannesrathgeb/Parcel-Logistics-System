using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities.Exceptions
{
    public class DataAccessNotFoundException : DataAccessException
    {
        public string Id { get; set; }

        public DataAccessNotFoundException(string id, string message, Exception innerException) : base(message, innerException)
        {
            this.Id = id;
        }
    }
}
