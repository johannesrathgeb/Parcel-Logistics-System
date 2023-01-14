﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Entities
{
    public class WebhookResponse
    {
        public int Id { get; set; } 
        public string trackingId { get; set; }  
        public string Url { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
