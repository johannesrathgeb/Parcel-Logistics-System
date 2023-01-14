using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic
{
    public class ParcelWebhookLogic : IParcelWebhookLogic
    {
        private readonly IWebhookRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<ParcelWebhookLogic> _logger;
        private readonly IParcelRepository _parcelRepo;

        public ParcelWebhookLogic(IWebhookRepository repo, IMapper mapper, ILogger<ParcelWebhookLogic> logger, IParcelRepository parcelRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
            _parcelRepo = parcelRepo;
        }
        public List<WebhookResponse> ListParcelWebhooks(string trackingId)
        {
            try
            {
                var webhooksDA = _repo.GetWebhooksForParcel(trackingId);
                return _mapper.Map<List<WebhookResponse>>(webhooksDA);
            }
            catch (DataAccess.Entities.Exceptions.DataAccessException ex)
            {
                throw; 
                //throw new BusinessLogicException($"Error listing Webhooks for Parcel with TrackingID [{trackingId}]", ex);
            }
        }

        public WebhookResponse SubscribeParcelWebhook(string trackingid, string url)
        {
            try
            {
                var p = _parcelRepo.GetByTrackingId(trackingid); 
                var webhookResponse = new WebhookResponse() { TrackingId = trackingid, CreatedAt = DateTime.Now, Url = url };
                return _mapper.Map<WebhookResponse>(_repo.CreateWebhook(_mapper.Map<DataAccess.Entities.WebhookResponse>(webhookResponse)));
            }
            catch (DataAccess.Entities.Exceptions.DataAccessException ex)
            {
                throw; 
                //throw new BusinessLogicException($"Error subscribing to a Webhook", ex);
            }
        }

        public void UnsubscribeParcelWebhook(int id)
        {
            try
            {
                _repo.DeleteWebhook(id);
            }
            catch (DataAccess.Entities.Exceptions.DataAccessException ex)
            {
                throw; 
                //throw new BusinessLogicException($"Error deleting a Webhook", ex);
            }
        }
    }
}
