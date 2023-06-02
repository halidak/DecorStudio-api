using DecorStudio_api.DTOs;
using DecorStudio_api.Models;
using Microsoft.EntityFrameworkCore;

namespace DecorStudio_api.Services
{
    public class StoreService
    {
        private readonly AppDbContext context;

        public StoreService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Store>> GetAllStores()
        {
            var list = await context.Stores.ToListAsync();
            return list;
        }

        public async Task<Store> GetStoreById(int id)
        {
            var store = await context.Stores.FirstOrDefaultAsync(s => s.Id == id);
            if (store == null)
            {
                throw new Exception("Store doesn't exist");
            }
            return store;
        }

        public async Task AddStore(StoreDto store)
        {
            var s = new Store
            {
                Name = store.Name,
                City = store.City,
                Address = store.Address,
                Size = store.Size,
                NumberOfEmployees = store.NumberOfEmployees
            };
            context.Stores.Add(s);
            await context.SaveChangesAsync();
        }

        public async Task DeleteStore(int id)
        {
            var s = context.Stores.FirstOrDefault(x => x.Id == id);
            if(s == null)
            {
                throw new Exception("Store doesn't exist");
            }

            context.Stores.Remove(s);
            await context.SaveChangesAsync();
        }

        public async Task UpdateStore(int id, StoreDto store)
        {
            var s = context.Stores.FirstOrDefault(x => x.Id == id);
            if (s == null)
            {
                throw new Exception("Store doesn't exist");
            }
            else
            {
                s.Name = store.Name;
                s.City = store.City;
                s.Address = store.Address;
                s.Size = store.Size;
                s.NumberOfEmployees = store.NumberOfEmployees;
                await context.SaveChangesAsync();
            }

        }
    }
}
