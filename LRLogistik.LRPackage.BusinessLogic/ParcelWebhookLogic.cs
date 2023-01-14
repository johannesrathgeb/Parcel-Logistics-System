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
                _logger.LogInformation($"Recieved List of all Webhooks for Parcel with TrackingID: {trackingId}");
                return _mapper.Map<List<WebhookResponse>>(webhooksDA);
            }
            catch (DataAccess.Entities.Exceptions.DataAccessNotFoundException e)
            {
                _logger.LogError($"No Webhooks found for Parcel with ID {trackingId}");
                throw new BusinessLogic.Exceptions.BusinessLogicNotFoundException("ListParcelWebhooks",$"No Webhooks found for Parcel with TrackingID: {trackingId}", e);
            }
        }

        public WebhookResponse SubscribeParcelWebhook(string trackingid, string url)
        {
            try
            {
                _parcelRepo.GetByTrackingId(trackingid); 
                var webhookResponse = new WebhookResponse() { TrackingId = trackingid, CreatedAt = DateTime.Now, Url = url };
                _logger.LogInformation($"{url} trying to subscribe to Parcel with TrackingID: {trackingid}");
                return _mapper.Map<WebhookResponse>(_repo.CreateWebhook(_mapper.Map<DataAccess.Entities.WebhookResponse>(webhookResponse)));
            }
            catch (DataAccess.Entities.Exceptions.DataAccessNotCreatedException e)
            {
                _logger.LogError($"Webhook not created for Parcel with Id: {trackingid}");
                throw new BusinessLogic.Exceptions.BusinessLogicNotCreatedException("SubscribeParcelWebhook", $"Webhook not created for Parcel with Id: {trackingid}", e);
            }
            catch(DataAccess.Entities.Exceptions.DataAccessNotFoundException e)
            {
                _logger.LogError($"Couldn't find Parcel with Id: {trackingid}");
                throw new BusinessLogic.Exceptions.BusinessLogicNotFoundException("SubscribeParcelWebhook", $"Couldn't find Parcel with Id: {trackingid}", e);
            }
        }

        public void UnsubscribeParcelWebhook(int id)
        {
            try
            {
                _repo.DeleteWebhook(id);
                _logger.LogInformation($"Webhook with id: {id} deleted");
            }
            catch (DataAccess.Entities.Exceptions.DataAccessNotFoundException e)
            {
                _logger.LogError($"Couldn't delete Webhook with Id: {id}");
                throw new BusinessLogic.Exceptions.BusinessLogicNotFoundException("UnsubscribeParcelWebhook", $"Couldn't delete Webhook with Id: {id}", e);
            }
        }
    }
}
