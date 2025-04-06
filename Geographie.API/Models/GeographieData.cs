namespace Geographie.API.Models
{
    public class GeographieData
    {
        public int Id { get; set; }
        public string Openbareruimte { get; set; } = "";
        public string Huisnummer { get; set; } = "";
        public string Huisletter { get; set; } = "";
        public string Huisnummertoevoeging { get; set; } = "";
        public string Postcode { get; set; } = "";
        public string Woonplaats { get; set; } = "";
        public string Gemeente { get; set; } = "";
        public string Provincie { get; set; } = "";
        public string Nummeraanduiding { get; set; } = "";
        public string Verblijfsobjectgebruiksdoel { get; set; } = "";
        public string Oppervlakteverblijfsobject { get; set; } = "";
        public string Verblijfsobjectstatus { get; set; } = "";
        public string ObjectId { get; set; } = "";
        public string ObjectType { get; set; } = "";
        public string Nevenadres { get; set; } = "";
        public string PandId { get; set; } = "";
        public string Pandstatus { get; set; } = "";
        public string Pandbouwjaar { get; set; } = "";
        public string X { get; set; } = "";
        public string Y { get; set; } = "";
        public string Lon { get; set; } = "";
        public string Lat { get; set; } = "";
    }
}
