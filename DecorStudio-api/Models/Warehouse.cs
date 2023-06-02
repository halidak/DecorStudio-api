﻿namespace DecorStudio_api.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Size { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}