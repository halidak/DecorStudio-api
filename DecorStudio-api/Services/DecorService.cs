﻿using DecorStudio_api.DTOs;
using DecorStudio_api.Migrations;
using DecorStudio_api.Models;
using Microsoft.EntityFrameworkCore;

namespace DecorStudio_api.Services
{
    public class DecorService
    {
        private readonly AppDbContext context;
        public DecorService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Decor>> GetAllDecors()
        {
            var list = await context.Decors.ToListAsync();
            return list;
        }

        public async Task<Decor> GetDecorById(int id)
        {
            var decor = await context.Decors.FirstOrDefaultAsync(s => s.Id == id);
            if (decor == null)
            {
                throw new Exception("Decor doesn't exist");
            }
            return decor;
        }


        public async Task AddDecor(int warehouseId, DecorDto decor)
        {
            var warehouse = await context.Warehouses.FirstOrDefaultAsync(s => s.Id == warehouseId);
            if (warehouse == null)
            {
                throw new Exception("Warehouse doesn't exist");
            }
           
            var d = new Decor
            {
                Name = decor.Name,
                Price = decor.Price,
                Description = decor.Description,
                Type = decor.Type,
                Image = decor.Image
            };
            context.Decors.Add(d);
            await context.SaveChangesAsync();

            var w = new Warehouse_Decor
            {
                WarehouseId = warehouseId,
                DecorId = d.Id,
                Amount = decor.Amount
            };
            context.Warehouse_Decors.Add(w);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDecor(int id)
        {
            var d = context.Decors.FirstOrDefault(x => x.Id == id);
            if(d == null)
            {
                throw new Exception("Decor doesn't exist");
            }
            context.Decors.Remove(d);
            await context.SaveChangesAsync();
        }

        public async Task UpdateDecor(int id, DecorDto decor)
        {
            var d = context.Decors.FirstOrDefault(x => x.Id == id);
            if(d == null)
            {
                throw new Exception("Decor doesn't exist");
            }
            d.Name = decor.Name;
            d.Price = decor.Price;
            d.Description = decor.Description;
            d.Type = decor.Type;
            d.Image = decor.Image;
            await context.SaveChangesAsync();
        }

        //svi dekori iz magacina
        public async Task<List<Decor>> GetAllDecorsFromWarehouse(int warehouseId)
        {
            var list = await context.Warehouse_Decors
                .Where(c => c.WarehouseId == warehouseId)
                .Include(c => c.Decor)
                .Select(c => c.Decor)
                .ToListAsync();

            return list;
        }

        //svi dekori iz kataloga
        public async Task<List<Decor>> GetAllDecorsFromCatalog(int catalogId)
        {
            var list = await context.Catalog_Decors
                .Where(c => c.CatalogId == catalogId)
                .Include(c => c.Decor)
                .Select(c => c.Decor)
                .ToListAsync();
            return list;
        }

        //svi dekori iz magacina iz neke radnje
        public async Task<List<Decor>> GetAllDecorsFromWarehouseFromStore(int storeId)
        {
            var list = await context.Warehouses
                .Where(c => c.StoreId == storeId)
                .Include(c => c.Warehouse_Decors)
                .SelectMany(c => c.Warehouse_Decors)
                .Include(c => c.Decor)
                .Select(c => c.Decor)
                .ToListAsync();
            return list;
        }

    }
}
