using DecorStudio_api.Models;

namespace DecorStudio_api.DTOs
{
    public class WarehouseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Size { get; set; }
        public int StoreId { get; set; }
    }
}
