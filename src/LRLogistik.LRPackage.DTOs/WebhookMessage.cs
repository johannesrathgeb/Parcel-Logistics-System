using LRLogistik.LRPackage.Services.DTOs.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.Json.Serialization;

namespace LRLogistik.LRPackage.Services.DTOs
{
    [DataContract]
    public partial class WebhookMessage
    {
        [TypeConverter(typeof(CustomEnumConverter<StateEnum>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum StateEnum
        {
            [EnumMember(Value = "Pickup")]
            PickupEnum = 1,

            [EnumMember(Value = "InTransport")]
            InTransportEnum = 2,

            [EnumMember(Value = "InTruckDelivery")]
            InTruckDeliveryEnum = 3,

            [EnumMember(Value = "Transferred")]
            TransferredEnum = 4,

            [EnumMember(Value = "Delivered")]
            DeliveredEnum = 5
        }

        [Required]
        [DataMember(Name = "state", EmitDefaultValue = true)]
        public StateEnum State { get; set; }

        [Required]
        [DataMember(Name = "visitedHops", EmitDefaultValue = false)]
        public List<HopArrival> VisitedHops { get; set; }

        [Required]
        [DataMember(Name = "futureHops", EmitDefaultValue = false)]
        public List<HopArrival> FutureHops { get; set; }

        [DataMember(Name = "trackingId", EmitDefaultValue = false)]
        public string TrackingId { get; set; }

    }
}
