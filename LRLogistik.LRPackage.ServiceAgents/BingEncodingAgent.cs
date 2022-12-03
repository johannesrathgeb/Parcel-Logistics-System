using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using BingMapsRESTToolkit;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.ServiceAgents.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Http;


namespace LRLogistik.LRPackage.ServiceAgents
{
    public class BingEncodingAgent : IGeoEncodingAgent
    {
        public GeoCoordinate EncodeAddress(Recipient r)
        {
            string address = (r.Country + r.PostalCode + r.City + r.Street);
            string url = "http://dev.virtualearth.net/REST/v1/Locations?query=" + address + "&key=ApXpNBTanGJMbV8xoem8FWdvckZ6anMTEcjBFgE-PVsJR0ETiUNT3_Jv2mqrRhwB";

            using (var client = new WebClient())
            {
                string response = client.DownloadString(url);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));
                using (var es = new MemoryStream(Encoding.Unicode.GetBytes(response)))
                {
                    var mapResponse = (ser.ReadObject(es) as Response); //Response is one of the Bing Maps DataContracts
                    Location location = (Location)mapResponse.ResourceSets.First().Resources.First();
                    return new GeoCoordinate()
                    {
                        Lat = location.Point.Coordinates[0],
                        Lon = location.Point.Coordinates[1]
                    };
                }
            }
        }
    }
}