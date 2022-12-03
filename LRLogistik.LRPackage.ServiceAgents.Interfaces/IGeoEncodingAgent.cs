﻿
using LRLogistik.LRPackage.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.ServiceAgents.Interfaces
{
    public interface IGeoEncodingAgent
    {
        public GeoCoordinate EncodeAddress(Recipient r);
    }
}
