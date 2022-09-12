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
using LRLogistik.LRPackage.Services.DTOs.Converters;

namespace LRLogistik.LRPackage.Services.DTOs
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class NewParcelInfo 
    {
        /// <summary>
        /// The tracking ID of the parcel. 
        /// </summary>
        /// <value>The tracking ID of the parcel. </value>
        [RegularExpression("^[A-Z0-9]{9}$")]
        [DataMember(Name="trackingId", EmitDefaultValue=false)]
        public string TrackingId { get; set; }

    }
}
