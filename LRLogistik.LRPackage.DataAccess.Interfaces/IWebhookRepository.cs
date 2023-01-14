using LRLogistik.LRPackage.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Interfaces
{
    public interface IWebhookRepository
    {
        public WebhookResponse GetWebhook(int id);

        public void DeleteWebhook(int id);

        public WebhookResponse CreateWebhook(WebhookResponse webhookResponse);

        public WebhookResponse UpdateWebhook(WebhookResponse webhookResponse);

        public List<WebhookResponse> GetWebhooksForParcel(string trackingId);
    }
}
