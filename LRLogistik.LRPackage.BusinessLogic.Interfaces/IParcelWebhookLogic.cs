using LRLogistik.LRPackage.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Interfaces
{
    public interface IParcelWebhookLogic
    {
        public List<WebhookResponse> ListParcelWebhooks(string trackingId);

        public WebhookResponse SubscribeParcelWebhook(string trackinId, string url);

        public void UnsubscribeParcelWebhook(int id);

    }
}
