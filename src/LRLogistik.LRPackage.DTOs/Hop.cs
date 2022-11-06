/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.22.1
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using JsonSubTypes;
using Swashbuckle.AspNetCore.Annotations;
using LRLogistik.LRPackage.Services.DTOs.Converters;
using System.Diagnostics.CodeAnalysis;

namespace LRLogistik.LRPackage.Services.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    [DataContract]
    [JsonConverter(typeof(JsonSubtypes), "HopType")]
    [SwaggerDiscriminator("HopType")]
    [JsonSubtypes.KnownSubType(typeof(Transferwarehouse), "transferwarehouse")]
    [SwaggerSubType(typeof(Transferwarehouse), DiscriminatorValue =  "transferwarehouse")]
    [JsonSubtypes.KnownSubType(typeof(Truck), "truck")]
    [SwaggerSubType(typeof(Truck), DiscriminatorValue =  "truck")]
    [JsonSubtypes.KnownSubType(typeof(Warehouse), "warehouse")]
    [SwaggerSubType(typeof(Warehouse), DiscriminatorValue =  "warehouse")]
    public partial class Hop 
    {
        /// <summary>
        /// Gets or Sets HopType
        /// </summary>
        [Required]
        [DataMember(Name="hopType", EmitDefaultValue=false)]
        public string HopType { get; set; }

        /// <summary>
        /// Unique CODE of the hop.
        /// </summary>
        /// <value>Unique CODE of the hop.</value>
        [Required]
        [RegularExpression("^[A-Z]{4}\\d{1,4}$")]
        [DataMember(Name="code", EmitDefaultValue=false)]
        public string Code { get; set; }

        /// <summary>
        /// Description of the hop.
        /// </summary>
        /// <value>Description of the hop.</value>
        [Required]
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Delay processing takes on the hop.
        /// </summary>
        /// <value>Delay processing takes on the hop.</value>
        [Required]
        [DataMember(Name="processingDelayMins", EmitDefaultValue=true)]
        public int ProcessingDelayMins { get; set; }

        /// <summary>
        /// Name of the location (village, city, ..) of the hop.
        /// </summary>
        /// <value>Name of the location (village, city, ..) of the hop.</value>
        [Required]
        [DataMember(Name="locationName", EmitDefaultValue=false)]
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or Sets LocationCoordinates
        /// </summary>
        [Required]
        [DataMember(Name="locationCoordinates", EmitDefaultValue=false)]
        public GeoCoordinate LocationCoordinates { get; set; }

    }
}
