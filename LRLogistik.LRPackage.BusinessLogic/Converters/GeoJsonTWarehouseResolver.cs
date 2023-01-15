using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Converters
{
    //public class GeoJsonTWarehouseResolver : IValueResolver<Transferwarehouse, DataAccess.Entities.Transferwarehouse, Geometry>, IValueResolver<DataAccess.Entities.Transferwarehouse, Transferwarehouse, string>
    //{
    //    public Geometry Resolve(Transferwarehouse source, DataAccess.Entities.Transferwarehouse destination, Geometry destMember, ResolutionContext context)
    //    {
    //        var jsonserialize = NetTopologySuite.IO.GeoJsonSerializer.CreateDefault();
    //        var feature = (Feature)jsonserialize.Deserialize(new StringReader(source.RegionGeoJson), typeof(Feature));
    //        return feature.Geometry;
    //    }

    //    public string Resolve(DataAccess.Entities.Transferwarehouse source, Transferwarehouse destination, string destMember, ResolutionContext context)
    //    {
    //        Feature feature = new(source.Region, null);

    //        var jsonserializer = GeoJsonSerializer.CreateDefault();
    //        StringWriter swriter = new();
    //        jsonserializer.Serialize(swriter, feature);
    //        swriter.Flush();
    //        return swriter.ToString();
    //    }

    //}
}
