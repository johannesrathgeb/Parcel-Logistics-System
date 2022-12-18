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
    public class GeoJsonResolver : IValueResolver<Truck, DataAccess.Entities.Truck, Geometry>, IValueResolver<DataAccess.Entities.Truck, Truck, string>
    {
        public Geometry Resolve(Truck source, DataAccess.Entities.Truck destination, Geometry destMember, ResolutionContext context)
        {
            var jsonserialize = NetTopologySuite.IO.GeoJsonSerializer.CreateDefault();
            var feature = (Feature)jsonserialize.Deserialize(new StringReader(source.RegionGeoJson), typeof(Feature));
            return feature.Geometry; 
        }

        public string Resolve(DataAccess.Entities.Truck source, Truck destination, string destMember, ResolutionContext context)
        {
            Feature feature = new(source.Region, null);

            var jsonserializer = GeoJsonSerializer.CreateDefault();
            StringWriter swriter = new();
            jsonserializer.Serialize(swriter, feature);
            swriter.Flush(); 
            return swriter.ToString();
        }
    }
}
