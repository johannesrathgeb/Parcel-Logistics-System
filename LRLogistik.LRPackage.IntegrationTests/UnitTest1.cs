//using LRLogistik.LRPackage.Services.DTOs;
//using Newtonsoft.Json;
//using System.Net;
//using System.Web;

//namespace LRLogistik.LRPackage.IntegrationTests
//{
//    public class Tests
//    {
//        public class UnitTest1
//        {
//            private readonly HttpClient _httpClient = new();
//            private readonly string _url = "https://ossv-ptt.azurewebsites.net";

//            public async Task<HttpResponseMessage> WarehouseManagementApi_POST_warehouse()
//            {
//                string data = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "dataset-light.json"));
//                var response = await _httpClient.PostAsync($"{_url}/warehouse", new StringContent(data, System.Text.Encoding.UTF8, "application/json"));
//                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//                return response;
//            }

//            public async Task<HttpResponseMessage> SenderApi_POST_parcel()
//            {
//                var response = await _httpClient.PostAsync($"{_url}/parcel",
//                    new StringContent(@"{
//                  ""weight"": 0.69,
//                  ""recipient"": {
//                    ""name"": ""Johannes"",
//                    ""street"": ""Jochbergengasse 1"",
//                    ""postalCode"": ""A-1210"",
//                    ""city"": ""Wien"",
//                    ""country"": ""Austria""
//                  },
//                  ""sender"": {
//                    ""name"": ""Florian"",
//                    ""street"": ""Kinskygasse 15"",
//                    ""postalCode"": ""A-1230"",
//                    ""city"": ""Wien"",
//                    ""country"": ""Austria""
//                  }
//                }", System.Text.Encoding.UTF8, "application/json"));
//                System.Console.WriteLine($"Content: {await response.Content.ReadAsStringAsync()}");
//                System.Console.WriteLine($"StatusCode: {response.StatusCode}");

//                //TODO
//                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//                return response;
//            }

//            public async Task<HttpResponseMessage?> RecipientApi_GET_parcel(string trackingId)
//            {
//                var response = await _httpClient.GetAsync($"{_url}/parcel/{trackingId}");
//                System.Console.WriteLine($"Content: {await response.Content.ReadAsStringAsync()}");
//                System.Console.WriteLine($"StatusCode: {response.StatusCode}");
//                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//                return response;
//            }

//            public async Task<HttpResponseMessage?> StaffApi_POST_reportHop(string trackingId, string code)
//            {
//                System.Console.WriteLine($"Reporting Hop: {trackingId}, {code}");
//                var response = await _httpClient.PostAsync($"{_url}/parcel/{trackingId}/reportHop/{code}", new StringContent(""));
//                System.Console.WriteLine($"StatusCode: {response.StatusCode}");
//                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//                return response;
//            }

//            public async Task<HttpResponseMessage?> StaffApi_POST_reportDelivery(string trackingId)
//            {
//                var response = await _httpClient.PostAsync($"{_url}/parcel/{trackingId}/reportDelivery/", new StringContent(""));
//                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//                return response;
//            }

//            public async Task<HttpResponseMessage?> ParcelWebhooksApi_POST_webhooks(string trackingId, string url)
//            {
//                var encodedUrl = HttpUtility.UrlEncode(url);
//                var response = await _httpClient.PostAsync($"{_url}/parcel/{trackingId}/webhooks?url={encodedUrl}",
//                    new StringContent(""));
//                System.Console.WriteLine($"POST /webhooks StatusCode: {response.StatusCode}");
//                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//                return response;
//            }

//            public async Task<HttpResponseMessage?> ParcelWebhooksApi_GET_webhooks(string trackingId)
//            {
//                var response = await _httpClient.GetAsync($"{_url}/parcel/{trackingId}/webhooks/");
//                System.Console.WriteLine($"Content: {await response.Content.ReadAsStringAsync()}");
//                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//                return response;
//            }

//            [Test]
//            [Category("Integration")]
//            public async Task ParcelJourney()
//            {
//                var firstHopCode = "WTTA09";
//                var webhookUrl = "http://test.test.fake/webhooks";

//                await WarehouseManagementApi_POST_warehouse();

//                var response = await SenderApi_POST_parcel();
//                Assert.NotNull(response);
//                var newParcelInfo = JsonConvert.DeserializeObject<NewParcelInfo>(await response.Content.ReadAsStringAsync());

//                response = await RecipientApi_GET_parcel(newParcelInfo.TrackingId);
//                Assert.NotNull(response);
//                var trackedParcel = JsonConvert.DeserializeObject<TrackingInformation>(await response.Content.ReadAsStringAsync());
//                Assert.NotNull(trackedParcel);

//                //TODO
//                Assert.AreEqual(3, trackedParcel.FutureHops.Count);
//                Assert.AreEqual(firstHopCode, trackedParcel.FutureHops[0].Code);

//                await ParcelWebhooksApi_POST_webhooks(newParcelInfo.TrackingId, webhookUrl);
//                response = await ParcelWebhooksApi_GET_webhooks(newParcelInfo.TrackingId);
//                Assert.NotNull(response);
//                var webhooksList = JsonConvert.DeserializeObject<List<WebhookResponse>>(await response.Content.ReadAsStringAsync());
//                Assert.NotNull(webhooksList);
//                Assert.AreEqual(1, webhooksList.Count);
//                Assert.AreEqual(webhookUrl, webhooksList[0].Url);

//                await StaffApi_POST_reportHop(newParcelInfo.TrackingId, firstHopCode);

//                response = await RecipientApi_GET_parcel(newParcelInfo.TrackingId);
//                Assert.NotNull(response);
//                trackedParcel = JsonConvert.DeserializeObject<TrackingInformation>(await response.Content.ReadAsStringAsync());
//                Assert.NotNull(trackedParcel);

//                //TODO
//                Assert.AreEqual(2, trackedParcel.FutureHops.Count);
//                Assert.AreEqual(1, trackedParcel.VisitedHops.Count);
//                Assert.AreEqual(firstHopCode, trackedParcel.VisitedHops[0].Code);
//                Assert.AreEqual(TrackingInformation.StateEnum.InTransportEnum, trackedParcel.State);

//                await StaffApi_POST_reportHop(newParcelInfo.TrackingId, trackedParcel.FutureHops[0].Code);

//                response = await RecipientApi_GET_parcel(newParcelInfo.TrackingId);
//                Assert.NotNull(response);
//                trackedParcel = JsonConvert.DeserializeObject<TrackingInformation>(await response.Content.ReadAsStringAsync());
//                Assert.NotNull(trackedParcel);

//                //TODO
//                Assert.AreEqual(1, trackedParcel.FutureHops.Count);
//                Assert.AreEqual(2, trackedParcel.VisitedHops.Count);
//                Assert.AreEqual(TrackingInformation.StateEnum.InTransportEnum, trackedParcel.State);

//                await StaffApi_POST_reportHop(newParcelInfo.TrackingId, trackedParcel.FutureHops[0].Code);

//                response = await RecipientApi_GET_parcel(newParcelInfo.TrackingId);
//                Assert.NotNull(response);
//                trackedParcel = JsonConvert.DeserializeObject<TrackingInformation>(await response.Content.ReadAsStringAsync());
//                Assert.NotNull(trackedParcel);

//                //TODO
//                Assert.AreEqual(0, trackedParcel.FutureHops.Count);
//                Assert.AreEqual(3, trackedParcel.VisitedHops.Count);
//                Assert.AreEqual(TrackingInformation.StateEnum.InTruckDeliveryEnum, trackedParcel.State);

//                await StaffApi_POST_reportDelivery(newParcelInfo.TrackingId);

//                response = await RecipientApi_GET_parcel(newParcelInfo.TrackingId);
//                Assert.NotNull(response);
//                trackedParcel = JsonConvert.DeserializeObject<TrackingInformation>(await response.Content.ReadAsStringAsync());
//                Assert.NotNull(trackedParcel);

//                //TODO
//                Assert.AreEqual(0, trackedParcel.FutureHops.Count);
//                Assert.AreEqual(3, trackedParcel.VisitedHops.Count);
//                Assert.AreEqual(TrackingInformation.StateEnum.DeliveredEnum, trackedParcel.State);

//                response = await ParcelWebhooksApi_GET_webhooks(newParcelInfo.TrackingId);
//                Assert.NotNull(response);
//                webhooksList = JsonConvert.DeserializeObject<List<WebhookResponse>>(await response.Content.ReadAsStringAsync());
//                Assert.NotNull(webhooksList);
//                Assert.AreEqual(0, webhooksList.Count);
//            }
//        }
//    }
//}