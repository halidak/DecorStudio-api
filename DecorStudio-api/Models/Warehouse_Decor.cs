using System.Text.Json.Serialization;

namespace DecorStudio_api.Models
{
    public class Warehouse_Decor
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int DecorId { get; set; }
        public Decor Decor { get; set; }
        public int Amount { get; set; }

    }
}
