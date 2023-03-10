using AutoMapper;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Converters
{
    [ExcludeFromCodeCoverage]
    public class GeoJsonConverter :
            IValueConverter<string, NetTopologySuite.Geometries.Geometry>,
            IValueConverter<NetTopologySuite.Geometries.Geometry, string>,
            ITypeConverter<string, NetTopologySuite.Geometries.Geometry>,
            ITypeConverter<NetTopologySuite.Geometries.Geometry, string>
    {
        public Geometry Convert(string source, Geometry dest, ResolutionContext context)
        {
            return this.Convert(source, context);
        }
        public string Convert(Geometry source, string dest, ResolutionContext context)
        {
            return this.Convert(source, context);
        }


        public NetTopologySuite.Geometries.Geometry Convert(string source, ResolutionContext context)
        {
            var serializer = NetTopologySuite.IO.GeoJsonSerializer.CreateDefault();
            var feature = (Feature)serializer.Deserialize(new StringReader(source), typeof(Feature));
            return feature?.Geometry;
        }

        public string Convert(Geometry sourceMember, ResolutionContext context)
        {
            Feature feature = new(sourceMember, null);

            var serializer = GeoJsonSerializer.CreateDefault();

            StringWriter writer = new();
            serializer.Serialize(writer, feature);
            writer.Flush();

            return writer.ToString();
        }
    }
}
