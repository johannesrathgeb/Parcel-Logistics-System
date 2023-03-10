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
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LRLogistik.LRPackage.BusinessLogic.Exceptions;

namespace LRLogistik.LRPackage.Services.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class SenderApiController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISubmissionLogic _submissionLogic;

        public SenderApiController(IMapper mapper, ISubmissionLogic submissionLogic, ILogger<SenderApiController> logger)
        {
            _mapper = mapper;
            _submissionLogic = submissionLogic;
            _logger = logger;
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
            try
            {
                _logger.LogInformation($"Submitting Parcel: {JsonConvert.SerializeObject(parcel)}");
                var parcelEntity = _mapper.Map<BusinessLogic.Entities.Parcel>(parcel);
                _logger.LogInformation($"Parcel mapped to BL: {JsonConvert.SerializeObject(parcel)}");
                var result = _submissionLogic.SubmitParcel(parcelEntity);

                return Created("", _mapper.Map<NewParcelInfo>(result));
                //return StatusCode(201, new ObjectResult(_mapper.Map<NewParcelInfo>(result)).Value);
            }
            catch(BusinessLogicNotFoundException e)
            {
                _logger.LogError($"Parcel Submission was invalid");
                return BadRequest(new Error { ErrorMessage= e.Message });
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
