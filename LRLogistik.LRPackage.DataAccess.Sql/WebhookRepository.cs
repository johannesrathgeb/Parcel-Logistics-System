using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using LRLogistik.LRPackage.ServiceAgents.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Sql
{
    public class WebhookRepository : IWebhookRepository
    {
        SampleContext _dbContext;
        private readonly ILogger _logger;

        public WebhookRepository(SampleContext dbContext, ILogger<WebhookRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public WebhookResponse CreateWebhook(WebhookResponse webhookResponse)
        {
            try
            {
                _dbContext.WebhookResponse.Add(webhookResponse);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Webhook successfully created: {JsonConvert.SerializeObject(webhookResponse)}");
                return webhookResponse;
            }
            catch(ArgumentNullException e)
            {
                _logger.LogError($"Webhook Creation was invalid");
                throw new Entities.Exceptions.DataAccessNotCreatedException("CreateWebhook", "Webhook was not created", e);
            }
        }

        public void DeleteWebhook(int id)
        {
            var result = GetWebhook(id);
            if(result == null)
            {
                throw new DataAccess.Entities.Exceptions.DataAccessNotFoundException("DeleteWebhook", "Webhook not found!");
            }
            try
            {
                _dbContext.WebhookResponse.Remove(result);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Webhook with id {id} successfully deleted");
            }
            catch(InvalidOperationException e)
            {
                _logger.LogError($"Deleting Webhook was invalid");
                throw new Entities.Exceptions.DataAccessNotFoundException("DeleteWebhook", "Webhook with Id " + id + " was not deleted", e);
            }
        }

        public WebhookResponse GetWebhook(int id)
        {
            try
            {
                var result = _dbContext.WebhookResponse.Single(wh => wh.Id == id);
                _logger.LogInformation($"Found Webhook: {JsonConvert.SerializeObject(result)}");
                return result;
            }
            catch(InvalidOperationException e){
                _logger.LogError($"Getting Webhook was invalid");
                throw new Entities.Exceptions.DataAccessNotFoundException("GetWebhook", "Webhook with Id " + id + " not found", e);
            }
        }

        public List<WebhookResponse> GetWebhooksForParcel(string trackingId)
        {
            try
            {
                var result = _dbContext.WebhookResponse.Where(wh => wh.TrackingId == trackingId).ToList();
                _logger.LogInformation($"Found Webhooks for Parcel with TrackingID: {trackingId}");
                return result;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Getting Webhooks for Parcel was unsuccesful!");
                throw new Entities.Exceptions.DataAccessNotFoundException("GetWebhooksForParcel", "Webhooks for Parcel with Id " + trackingId + " not found", e);
            }
        }

    } 
}