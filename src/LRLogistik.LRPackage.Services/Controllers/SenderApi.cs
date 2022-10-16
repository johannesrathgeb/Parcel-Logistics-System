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
using LRLogistik.LRPackage.BusinessLogic;

namespace LRLogistik.LRPackage.Services.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class SenderApiController : ControllerBase
    {

        private readonly IMapper _mapper;

        public SenderApiController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Submit a new parcel to the logistics service. 
        /// </summary>
        /// <param name="parcel"></param>
        /// <response code="201">Successfully submitted the new parcel</response>
        /// <response code="400">The operation failed due to an error.</response>
        /// <response code="404">The address of sender or receiver was not found.</response>
        [HttpPost]
        [Route("/parcel")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("SubmitParcel")]
        [SwaggerResponse(statusCode: 201, type: typeof(NewParcelInfo), description: "Successfully submitted the new parcel")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        [SwaggerResponse(statusCode: 404, type: typeof(Error), description: "The address of sender or receiver was not found.")]
        public virtual IActionResult SubmitParcel([FromBody] Parcel parcel)
        {
            var parcelEntity = _mapper.Map<BusinessLogic.Entities.Parcel>(parcel);
            var submissionLogic = new SubmissionLogic();
            var result = submissionLogic.SubmitParcel(parcelEntity);


            if (result.GetType() == typeof(BusinessLogic.Entities.Parcel))
            {
                //return new ObjectResult(_mapper.Map<NewParcelInfo>(result));
                return StatusCode(201, new ObjectResult(_mapper.Map<NewParcelInfo>(result)).Value);
            }
            else
            {
                return StatusCode(400, new ObjectResult(_mapper.Map<DTOs.Error>(result)).Value);
            }




            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(NewParcelInfo));
            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404, default(Error));
        }
    }
}
