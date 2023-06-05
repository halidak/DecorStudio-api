using System.Text.Json.Serialization;

namespace DecorStudio_api.Models
{
    public class Catalog_Decor
    {
        public int Id { get; set; }
        public int CatalogId { get; set; }
        [JsonIgnore]
        public Catalog Catalog { get; set; }
        public int DecorId { get; set; }
        [JsonIgnore]
        public Decor Decor { get; set; }
    }
}
