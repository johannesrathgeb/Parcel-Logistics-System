using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Entities
{
    public class WebhookResponse
    {
        public int Id { get; set; }

        public string TrackingId { get; set; }

        public string Url { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
