using System.Text.Json.Serialization;

namespace DecorStudio_api.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Size { get; set; }
        public int StoreId { get; set; }
        [JsonIgnore]
        public Store Store { get; set; }
        public List<Warehouse_Decor> Warehouse_Decors { get; set; }
    }
}
