using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
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

        public WebhookRepository(SampleContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WebhookResponse CreateWebhook(WebhookResponse webhookResponse)
        {
            try
            {
                _dbContext.WebhookResponse.Add(webhookResponse);
                _dbContext.SaveChanges();
                return webhookResponse;
            }
            catch(Exception ex)
            {
                //TODO
                throw;
            }
        }

        public void DeleteWebhook(int id)
        {
            var result = GetWebhook(id);
            if(result == null)
            {
                //TODO
                //throw new DataAccess.Entities.Exceptions.DataAccessNotFoundException();
            }
            try
            {
                _dbContext.WebhookResponse.Remove(result);
                _dbContext.SaveChanges();

            }
            catch(Exception ex)
            {
                //TODO
                throw;
            }
        }

        public WebhookResponse GetWebhook(int id)
        {
            var result = _dbContext.WebhookResponse.SingleOrDefault(wh => wh.Id == id);
            if(result == null)
            {
                //throw
            }

            return result;
        }

        public List<WebhookResponse> GetWebhooksForParcel(string trackingId)
        {
            var result = _dbContext.WebhookResponse.Where(wh => wh.trackingId == trackingId).ToList();
            if(result == null)
            {
                //throw new DataAccess.Entities.Exceptions.DataAccessNotFoundException()
            }
            return result;
        }

        public WebhookResponse UpdateWebhook(WebhookResponse webhookResponse)
        {
            var result = GetWebhook(webhookResponse.Id);
            if(result == null)
            {
                //throw
            }

            _dbContext.Update(webhookResponse);
            _dbContext.SaveChanges();
            return result;
        }
    } 
}