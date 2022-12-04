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
    public class WarehouseManagementApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IWarehouseLogic _warehouseLogic;

        public WarehouseManagementApiController(IMapper mapper, IWarehouseLogic warehouseLogic, ILogger<WarehouseManagementApiController> logger)
        {
            _mapper = mapper;
            _warehouseLogic = warehouseLogic;
            _logger = logger;
        }

        /// <summary>
        /// Exports the hierarchy of Warehouse and Truck objects. 
        /// </summary>
        /// <response code="200">Successful response</response>
        /// <response code="400">The operation failed due to an error.</response>
        /// <response code="404">No hierarchy loaded yet.</response>
        [HttpGet]
        [Route("/warehouse")]
        [ValidateModelState]
        [SwaggerOperation("ExportWarehouses")]
        [SwaggerResponse(statusCode: 200, type: typeof(Warehouse), description: "Successful response")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult ExportWarehouses()
        {
            try
            {
                _logger.LogInformation($"Exporting Warehouse");
                var result = _warehouseLogic.ExportWarehouse();

                return Ok(_mapper.Map<Warehouse>(result));                
            }
            catch(BusinessLogicNotFoundException e)
            {
                _logger.LogDebug($"Warehouse Export was invalid");
                return BadRequest(new Error { ErrorMessage= e.Message });
            }

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Warehouse));
            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
        }

        /// <summary>
        /// Get a certain warehouse or truck by code
        /// </summary>
        /// <param name="code"></param>
        /// <response code="200">Successful response</response>
        /// <response code="400">The operation failed due to an error.</response>
        /// <response code="404">No hop with the specified id could be found.</response>
        [HttpGet]
        [Route("/warehouse/{code}")]
        [ValidateModelState]
        [SwaggerOperation("GetWarehouse")]
        [SwaggerResponse(statusCode: 200, type: typeof(Hop), description: "Successful response")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult GetWarehouse([FromRoute(Name = "code")][Required] string code)
        {
            try
            {
                _logger.LogInformation($"Getting Warehouse : {JsonConvert.SerializeObject(code)}");
                var result = _warehouseLogic.GetWarehouse(code);

                return Ok(_mapper.Map<DTOs.Hop>(result));               
            }
            catch(BusinessLogicNotFoundException e)
            {
                _logger.LogDebug($"Getting Warehouse was invalid");
                return BadRequest(new Error { ErrorMessage= e.Message });
            }

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Hop));
            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
        }

        /// <summary>
        /// Imports a hierarchy of Warehouse and Truck objects. 
        /// </summary>
        /// <param name="warehouse"></param>
        /// <response code="200">Successfully loaded.</response>
        /// <response code="400">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/warehouse")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("ImportWarehouses")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult ImportWarehouses([FromBody] Warehouse warehouse)
        {
            try
            {
                _logger.LogInformation($"Importing Warehouse: {JsonConvert.SerializeObject(warehouse)}");
                var warehouseEntity = _mapper.Map<BusinessLogic.Entities.Warehouse>(warehouse);
                _logger.LogInformation($"Warehouse mapped to BL: {JsonConvert.SerializeObject(warehouse)}");

                var result = _warehouseLogic.ImportWarehouse(warehouseEntity);

                return Ok(new ObjectResult(result));
            }
            catch(BusinessLogicNotCreatedException e)
            {
                _logger.LogDebug($"Warehouse import was invalid");
                return BadRequest(new Error { ErrorMessage= e.Message });
            }

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400, default(Error));
        }
    }
}
