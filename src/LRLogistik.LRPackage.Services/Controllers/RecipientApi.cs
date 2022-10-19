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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using LRLogistik.LRPackage.Services.Attributes;
using LRLogistik.LRPackage.Services.DTOs;
using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LRLogistik.LRPackage.Services.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [ApiController]
    public class RecipientApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITrackingLogic _trackingLogic;

        [ActivatorUtilitiesConstructor]
        public RecipientApiController(IMapper mapper)
        {
            _mapper = mapper;
            _trackingLogic = new TrackingLogic();   
        }

        public RecipientApiController(IMapper mapper, ITrackingLogic trackingLogic)
        {
            _mapper = mapper;
            _trackingLogic = trackingLogic;
        }

        /// <summary>
        /// Find the latest state of a parcel by its tracking ID. 
        /// </summary>
        /// <param name="trackingId">The tracking ID of the parcel. E.g. PYJRB4HZ6 </param>
        /// <response code="200">Parcel exists, here&#39;s the tracking information.</response>
        /// <response code="400">The operation failed due to an error.</response>
        /// <response code="404">Parcel does not exist with this tracking ID.</response>
        [HttpGet]
        [Route("/parcel/{trackingId}")]
        [ValidateModelState]
        [SwaggerOperation("TrackParcel")]
        [SwaggerResponse(statusCode: 200, type: typeof(TrackingInformation), description: "Parcel exists, here&#39;s the tracking information.")]
        [SwaggerResponse(statusCode: 400, type: typeof(DTOs.Error), description: "The operation failed due to an error.")]
        public virtual IActionResult TrackParcel([FromRoute(Name = "trackingId")][Required][RegularExpression("^[A-Z0-9]{9}$")] string trackingId)
        {


            var result = _trackingLogic.TrackPackage(trackingId);

            if (result.GetType() == typeof(BusinessLogic.Entities.Parcel))
            {
                //return new ObjectResult(_mapper.Map<NewParcelInfo>(result));
                return StatusCode(200, new ObjectResult(_mapper.Map<TrackingInformation>(result)).Value);
            }
            else
            {
                return StatusCode(400, new ObjectResult(_mapper.Map<DTOs.Error>(result)).Value);
            }
        }
    }
}
