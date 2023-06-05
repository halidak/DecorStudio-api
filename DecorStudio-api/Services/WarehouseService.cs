using DecorStudio_api.DTOs;
using DecorStudio_api.Models;
using Microsoft.EntityFrameworkCore;

namespace DecorStudio_api.Services
{
    public class WarehouseService
    {
        private readonly AppDbContext context;

        public WarehouseService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Warehouse>> GetAllWarehouses(int storeId)
        {
            var list = await context.Warehouses
                 .Where(c => c.StoreId == storeId)
                 .Include(c => c.Warehouse_Decors) // Include Warehouse_Decors
                 .ToListAsync();
            return list;
        }

        public async Task<List<Warehouse_Decor>> GetAllDecorsFromWarehouse(int warehouseId)
        {
            var list = await context.Warehouse_Decors
                .Where(c => c.WarehouseId == warehouseId)
                .Include(c => c.Decor)
                .ToListAsync();

            return list;
        }


        public async Task<Warehouse> GetWarehouseById(int storeId, int id)
        {
            var warehouse = await context.Warehouses.Include(c => c.Store).Include(w => w.Warehouse_Decors).FirstOrDefaultAsync(s => s.Id == id && s.StoreId == storeId);
            if (warehouse == null)
            {
                throw new Exception("Warehouse doesn't exist");
            }
            return warehouse;
        }

        public async Task AddWarehouse(WarehouseDto warehouse)
        {
            var store = await context.Stores.FirstOrDefaultAsync(s => s.Id == warehouse.StoreId);
            if (store == null)
            {
                throw new Exception("Store doesn't exist");
            }
            var w = new Warehouse
            {
                Name = warehouse.Name,
                Address = warehouse.Address,
                Size = warehouse.Size,
                StoreId = warehouse.StoreId
            };
            context.Warehouses.Add(w);
            await context.SaveChangesAsync();
        }

        public async Task DeleteWarehouse(int id)
        {
            var w = context.Warehouses.FirstOrDefault(x => x.Id == id);
            if(w == null)
            {
                throw new Exception("Warehouse doesn't exist");
            }
            context.Warehouses.Remove(w);
            await context.SaveChangesAsync();
        }

        public async Task UpdateWarehouse(int id, WarehouseDto warehouse)
        {
            var store = await context.Stores.FirstOrDefaultAsync(s => s.Id == warehouse.StoreId);
            if (store == null)
            {
                throw new Exception("Store doesn't exist");
            }
            var w = context.Warehouses.FirstOrDefault(x => x.Id == id);
            if(w == null)
            {
                throw new Exception("Warehouse doesn't exist");
            }
            w.Name = warehouse.Name;
            w.Address = warehouse.Address;
            w.Size = warehouse.Size;
            w.StoreId = warehouse.StoreId;
            await context.SaveChangesAsync();
        }
    }
}
