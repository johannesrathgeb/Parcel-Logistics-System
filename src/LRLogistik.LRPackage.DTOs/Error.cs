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
using System.Diagnostics.CodeAnalysis;

namespace LRLogistik.LRPackage.Services.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    [DataContract]
    public partial class Error 
    {
        /// <summary>
        /// The error message.
        /// </summary>
        /// <value>The error message.</value>
        [Required]
        [DataMember(Name="errorMessage", EmitDefaultValue=false)]
        public string ErrorMessage { get; set; }

    }
}
