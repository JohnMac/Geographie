using CsvHelper.Configuration;
using Geographie.API.Models;

namespace Geographie.API.Mappings
{
    public class GeographieDataMap : ClassMap<GeographieData>
    {
        public GeographieDataMap()
        {
            Map(m => m.Openbareruimte).Name("openbareruimte");
            Map(m => m.Huisnummer).Name("huisnummer");
            Map(m => m.Huisletter).Name("huisletter");
            Map(m => m.Huisnummertoevoeging).Name("huisnummertoevoeging");
            Map(m => m.Postcode).Name("postcode");
            Map(m => m.Woonplaats).Name("woonplaats");
            Map(m => m.Gemeente).Name("gemeente");
            Map(m => m.Provincie).Name("provincie");
            Map(m => m.Nummeraanduiding).Name("nummeraanduiding");
            Map(m => m.Verblijfsobjectgebruiksdoel).Name("verblijfsobjectgebruiksdoel");
            Map(m => m.Oppervlakteverblijfsobject).Name("oppervlakteverblijfsobject");
            Map(m => m.Verblijfsobjectstatus).Name("verblijfsobjectstatus");
            Map(m => m.ObjectId).Name("object_id");
            Map(m => m.ObjectType).Name("object_type");
            Map(m => m.Nevenadres).Name("nevenadres");
            Map(m => m.PandId).Name("pandid");
            Map(m => m.Pandstatus).Name("pandstatus");
            Map(m => m.Pandbouwjaar).Name("pandbouwjaar");
            Map(m => m.X).Name("x");
            Map(m => m.Y).Name("y");
            Map(m => m.Lon).Name("lon");
            Map(m => m.Lat).Name("lat");
        }
    }
}
