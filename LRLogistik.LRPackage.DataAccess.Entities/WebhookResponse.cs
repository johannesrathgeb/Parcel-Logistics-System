using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    [ExcludeFromCodeCoverage]
    public class WebhookResponse
    {
        public int Id { get; set; } 
        public string TrackingId { get; set; }  
        public string Url { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
