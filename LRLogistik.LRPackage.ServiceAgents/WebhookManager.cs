using AutoMapper;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using LRLogistik.LRPackage.ServiceAgents.Interfaces;
using LRLogistik.LRPackage.Services.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.ServiceAgents
{
    public class WebhookManager : IWebhookManager
    {
        private readonly HttpClient _httpClient;
        private readonly IParcelRepository _parcelRepository;
        private readonly IWebhookRepository _webhookRepository;
        private readonly IMapper _mapper;


        [ActivatorUtilitiesConstructor]
        public WebhookManager(IParcelRepository parcelRepository, IWebhookRepository webhookRepository, IMapper mapper)
        {
            _httpClient = new HttpClient();
            _parcelRepository = parcelRepository;
            _webhookRepository = webhookRepository;
            _mapper = mapper;
        }

        public WebhookManager(HttpClient httpClient, IParcelRepository parcelRepository, IWebhookRepository webhookRepository, IMapper mapper)
        {
            _httpClient = httpClient;
            _parcelRepository = parcelRepository;
            _webhookRepository = webhookRepository;
            _mapper = mapper;
        }

        public async Task NotifySubscribers(string trackingId)
        {
            var webhooks = _webhookRepository.GetWebhooksForParcel(trackingId);

            var parcel = _parcelRepository.GetByTrackingId(trackingId);

            WebhookMessage webhookMessage;

            try
            {
                webhookMessage = _mapper.Map<WebhookMessage>(_mapper.Map<BusinessLogic.Entities.Parcel>(parcel));
            }
            catch(Exception e)
            {
                throw;
            }

            var payload = JsonConvert.SerializeObject(webhookMessage);

            try
            {
                await Task.WhenAll(webhooks.Select(wh =>
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, wh.Url);
                    request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
                    try
                    {
                        return _httpClient.SendAsync(request);
                    }
                    catch (Exception ex)
                    {
                        return Task.FromResult(new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest });
                    }
                }));
            }

            catch (Exception ex)
            {
                //TODO
            }

            if(parcel.State == DataAccess.Entities.Parcel.StateEnum.DeliveredEnum)
            {
                webhooks.ForEach(webhook => _webhookRepository.DeleteWebhook(webhook.Id));
            }

        }
    }
}
