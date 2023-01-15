using LRLogistik.LRPackage.Services.DTOs;
using Newtonsoft.Json;
using System.Net;
using System.Web;

namespace LRLogistik.LRPackage.IntegrationTests
{
    public class IntegrationTest
    {
        private readonly HttpClient _httpClient = new();
        private readonly string _url = "https://lrlogistik.azurewebsites.net";

        public async Task<HttpResponseMessage> CreateWarehouses_POST()
        {
            string data = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "dataset-light.json"));
            var response = await _httpClient.PostAsync($"{_url}/warehouse", new StringContent(data, System.Text.Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            return response;
        }

        public async Task<HttpResponseMessage> SubmitParcel_POST()
        {
            var response = await _httpClient.PostAsync($"{_url}/parcel",
                new StringContent(@"{
                ""weight"": 0.69,
                ""recipient"": {
                ""name"": ""Johannes"",
                ""street"": ""Jochbergengasse 1"",
                ""postalCode"": ""A-1210"",
                ""city"": ""Wien"",
                ""country"": ""Austria""
                },
                ""sender"": {
                ""name"": ""Florian"",
                ""street"": ""Kinskygasse 15"",
                ""postalCode"": ""A-1230"",
                ""city"": ""Wien"",
                ""country"": ""Austria""
                }
            }", System.Text.Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            return response;
        }

        public async Task<HttpResponseMessage?> GetParcel_GET(string trackingId)
        {
            var response = await _httpClient.GetAsync($"{_url}/parcel/{trackingId}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            return response;
        }

        public async Task<HttpResponseMessage?> ReportHop_POST(string trackingId, string code)
        {
            var response = await _httpClient.PostAsync($"{_url}/parcel/{trackingId}/reportHop/{code}", new StringContent(""));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            return response;
        }

        public async Task<HttpResponseMessage?> ReportDelivery_POST(string trackingId)
        {
            var response = await _httpClient.PostAsync($"{_url}/parcel/{trackingId}/reportDelivery/", new StringContent(""));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            return response;
        }

        public async Task<HttpResponseMessage?> SubscribeWebhook_POST(string trackingId, string url)
        {
            var encodedUrl = HttpUtility.UrlEncode(url);
            var response = await _httpClient.PostAsync($"{_url}/parcel/{trackingId}/webhooks?url={encodedUrl}",
                new StringContent(""));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            return response;
        }

        public async Task<HttpResponseMessage?> GetWebhooks_GET(string trackingId)
        {
            var response = await _httpClient.GetAsync($"{_url}/parcel/{trackingId}/webhooks/");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            return response;
        }

        [Test]
        [Category("Integration")]
        public async Task FullParcelTrackingTest()
        {
            var firstHopCode = "WTTA09";
            var webhookUrl = "http://test.test.fake/webhooks";

            await CreateWarehouses_POST();

            var response = await SubmitParcel_POST();
            Assert.NotNull(response);
            var newParcelInfo = JsonConvert.DeserializeObject<NewParcelInfo>(await response.Content.ReadAsStringAsync());

            response = await GetParcel_GET(newParcelInfo.TrackingId);
            Assert.NotNull(response);
            var trackedParcel = JsonConvert.DeserializeObject<TrackingInformation>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(trackedParcel);
            Assert.AreEqual(3, trackedParcel.FutureHops.Count);
            Assert.AreEqual(firstHopCode, trackedParcel.FutureHops[0].Code);

            await SubscribeWebhook_POST(newParcelInfo.TrackingId, webhookUrl);
            response = await GetWebhooks_GET(newParcelInfo.TrackingId);
            Assert.NotNull(response);
            var webhooksList = JsonConvert.DeserializeObject<List<WebhookResponse>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(webhooksList);
            Assert.AreEqual(1, webhooksList.Count);
            Assert.AreEqual(webhookUrl, webhooksList[0].Url);

            await ReportHop_POST(newParcelInfo.TrackingId, firstHopCode);

            response = await GetParcel_GET(newParcelInfo.TrackingId);
            Assert.NotNull(response);
            trackedParcel = JsonConvert.DeserializeObject<TrackingInformation>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(trackedParcel);
            Assert.AreEqual(2, trackedParcel.FutureHops.Count);
            Assert.AreEqual(1, trackedParcel.VisitedHops.Count);
            Assert.AreEqual(firstHopCode, trackedParcel.VisitedHops[0].Code);
            Assert.AreEqual(TrackingInformation.StateEnum.InTruckDeliveryEnum, trackedParcel.State);

            await ReportHop_POST(newParcelInfo.TrackingId, trackedParcel.FutureHops[0].Code);

            response = await GetParcel_GET(newParcelInfo.TrackingId);
            Assert.NotNull(response);
            trackedParcel = JsonConvert.DeserializeObject<TrackingInformation>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(trackedParcel);
            Assert.AreEqual(1, trackedParcel.FutureHops.Count);
            Assert.AreEqual(2, trackedParcel.VisitedHops.Count);
            Assert.AreEqual(TrackingInformation.StateEnum.InTransportEnum, trackedParcel.State);

            await ReportHop_POST(newParcelInfo.TrackingId, trackedParcel.FutureHops[0].Code);

            response = await GetParcel_GET(newParcelInfo.TrackingId);
            Assert.NotNull(response);
            trackedParcel = JsonConvert.DeserializeObject<TrackingInformation>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(trackedParcel);
            Assert.AreEqual(0, trackedParcel.FutureHops.Count);
            Assert.AreEqual(3, trackedParcel.VisitedHops.Count);
            Assert.AreEqual(TrackingInformation.StateEnum.InTruckDeliveryEnum, trackedParcel.State);

            await ReportDelivery_POST(newParcelInfo.TrackingId);

            response = await GetParcel_GET(newParcelInfo.TrackingId);
            Assert.NotNull(response);
            trackedParcel = JsonConvert.DeserializeObject<TrackingInformation>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(trackedParcel);
            Assert.AreEqual(0, trackedParcel.FutureHops.Count);
            Assert.AreEqual(3, trackedParcel.VisitedHops.Count);
            Assert.AreEqual(TrackingInformation.StateEnum.DeliveredEnum, trackedParcel.State);

            response = await GetWebhooks_GET(newParcelInfo.TrackingId);
            Assert.NotNull(response);
            webhooksList = JsonConvert.DeserializeObject<List<WebhookResponse>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(webhooksList);
            Assert.AreEqual(0, webhooksList.Count);
        }
    }

}