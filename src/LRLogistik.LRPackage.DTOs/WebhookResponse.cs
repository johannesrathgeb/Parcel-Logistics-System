using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.Services.DTOs
{

    [DataContract]
    public partial class WebhookResponse
    {

        [DataMember(Name = "id", EmitDefaultValue = true)]
        public int Id { get; set; }

        [DataMember(Name = "trackingId", EmitDefaultValue = false)]
        public string TrackingId { get; set; }

        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

    }
}
