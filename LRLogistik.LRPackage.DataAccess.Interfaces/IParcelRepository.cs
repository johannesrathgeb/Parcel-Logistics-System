﻿using LRLogistik.LRPackage.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Interfaces
{
    public interface IParcelRepository
    {
        Parcel Create(Parcel p);
        Parcel Update(Parcel p);
        void Delete(string TrackingId);

        // Get by ID
        Parcel GetByTrackingId(string trackingid);
    }
}
