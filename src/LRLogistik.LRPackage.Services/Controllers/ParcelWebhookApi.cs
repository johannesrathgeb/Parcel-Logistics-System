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
    public class ParcelWebhookApi : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IParcelWebhookLogic _parcelWebhookLogic;

        public ParcelWebhookApi(IMapper mapper, IParcelWebhookLogic parcelWebhookLogic, ILogger<ParcelWebhookApi> logger)
        {
            _mapper = mapper;
            _parcelWebhookLogic = parcelWebhookLogic;
            _logger = logger;
        }

        /// <summary>
        /// Get all registered subscriptions for the parcel webhook.
        /// </summary>
        /// <param name="trackingId"></param>
        /// <response code="200">List of webooks for the &#x60;trackingId&#x60;</response>
        /// <response code="400">The operation failed due to an error.</response>
        /// <response code="404">No parcel found with that tracking ID.</response>
        [HttpGet]
        [Route("/parcel/{trackingId}/webhooks")]
        [ValidateModelState]
        [SwaggerOperation("ListParcelWebhooks")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<WebhookResponse>), description: "List of webooks for the &#x60;trackingId&#x60;")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult ListParcelWebhooks([FromRoute(Name = "trackingId")][Required][RegularExpression("^[A-Z0-9]{9}$")] string trackingId)
        {
            List<BusinessLogic.Entities.WebhookResponse> webhooksBL;
            try
            {
                webhooksBL = _parcelWebhookLogic.ListParcelWebhooks(trackingId);
                var webhooks = _mapper.Map<List<WebhookResponse>>(webhooksBL);
                _logger.LogInformation($"Got List of Webhooks for Parcel with TrackingID: {trackingId}");
                return StatusCode(200, webhooks);
            }
            catch (BusinessLogicNotFoundException e)
            {
                _logger.LogError($"Webhooks for Parcel with TrackingID {trackingId} not found.");
                return BadRequest(new Error { ErrorMessage = e.Message });
            }           
        }

        /// <summary>
        /// Subscribe to a webhook notification for the specific parcel.
        /// </summary>
        /// <param name="trackingId"></param>
        /// <param name="url"></param>
        /// <response code="200">Successful response</response>
        /// <response code="400">The operation failed due to an error.</response>
        /// <response code="404">No parcel found with that tracking ID.</response>
        [HttpPost]
        [Route("/parcel/{trackingId}/webhooks")]
        [ValidateModelState]
        [SwaggerOperation("SubscribeParcelWebhook")]
        [SwaggerResponse(statusCode: 200, type: typeof(WebhookResponse), description: "Successful response")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult SubscribeParcelWebhook([FromRoute(Name = "trackingId")][Required][RegularExpression("^[A-Z0-9]{9}$")] string trackingId, [FromQuery(Name = "url")][Required()] string url)
        {

            BusinessLogic.Entities.WebhookResponse webhookResponseBL;
            try
            {
                webhookResponseBL = _parcelWebhookLogic.SubscribeParcelWebhook(trackingId, url);
                var webhookResponse = _mapper.Map<WebhookResponse>(webhookResponseBL);
                _logger.LogInformation($"Subscribed to Parcel with TrackingID: {trackingId}");
                return StatusCode(200, webhookResponse);
            }
            catch (BusinessLogicNotFoundException e)
            {
                _logger.LogError($"No Parcel with TrackingID {trackingId} found.");
                return BadRequest(new Error { ErrorMessage = e.Message });
            }
            catch (BusinessLogicNotCreatedException e)
            {
                _logger.LogError($"Webhooks for Parcel with TrackingID {trackingId} couldn't be created.");
                return BadRequest(new Error { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Remove an existing webhook subscription.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Success</response>
        /// <response code="400">The operation failed due to an error.</response>
        /// <response code="404">Subscription does not exist.</response>
        [HttpDelete]
        [Route("/parcel/webhooks/{id}")]
        [ValidateModelState]
        [SwaggerOperation("UnsubscribeParcelWebhook")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult UnsubscribeParcelWebhook([FromRoute(Name = "id")][Required] int id)
        {
            try
            {
                var res = _parcelWebhookLogic.UnsubscribeParcelWebhook(id);
                _logger.LogInformation($"Unsubscribed from Parcel with Webhook with ID: {id}");
                return StatusCode(200, res);
            }
            catch (BusinessLogicNotFoundException e)
            {
                _logger.LogError($"Couldn't unsubscribe from Parcel with Webhook with ID: {id}.");
                return BadRequest(new Error { ErrorMessage = e.Message });
            }
        }
    }
}