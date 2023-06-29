using DecorStudio_api.DTOs;
using DecorStudio_api.Migrations;
using DecorStudio_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var decor = await context.Decors.Include(d => d.Warehouse_Decors).FirstOrDefaultAsync(s => s.Id == id);
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

        //svi dekori iz liste magacina jedne radnje koji se ne nalaze u katalogu
        public async Task<List<Decor>> GetAllDecorsFromWarehouseNotInCatalog(int storeId, int catalogId)
        {
            var warehouseIds = await context.Warehouses
                .Where(c => c.StoreId == storeId)
                .Select(c => c.Id)
                .ToListAsync();

            var decorsFromWarehouses = await context.Warehouse_Decors
                .Where(c => warehouseIds.Contains(c.WarehouseId))
                .Include(c => c.Decor)
                .Select(c => c.Decor)
                .ToListAsync();

            var decorsInCatalog = await context.Catalog_Decors
                .Where(c => c.CatalogId == catalogId)
                .Include(c => c.Decor)
                .Select(c => c.Decor)
                .ToListAsync();

            var decorsNotInCatalog = decorsFromWarehouses.Except(decorsInCatalog).ToList();

            return decorsNotInCatalog;
        }




        //svi dekori iz kataloga
        public async Task<List<Decor>> GetAllDecorsFromCatalog(int catalogId, string? filter)
        {
            var list = await context.Catalog_Decors
                .Where(c => c.CatalogId == catalogId)
                .Include(c => c.Decor)
                .Select(c => c.Decor)
                .ToListAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                list = list.Where(d => d.Type.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

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

        //dekor rezervisan od strane korisnika i njihovi termini
        //public async Task<List<Decor>> GetAllDecorsFromReservationWithAppointments(string userId)
        //{
        //    var list = await context.Decors
        //        .Where(d => d.Decor_Reservations.Any(dr => dr.Reservation.UserId == userId))
        //        .Include(d => d.Decor_Reservations)
        //        .SelectMany(d => d.Decor_Reservations)
        //        .Include(dr => dr.Reservation)
        //        .SelectMany(dr => dr.Reservation.Appointments)
        //        .ToListAsync();
        //    return list;
        //}


        public async Task<List<Decor>> GetAllDecorsFromReservation(string userId)
        {
            var list = await context.Decors.Where(d => d.Decor_Reservations.Any(dr => dr.Reservation.UserId == userId)).ToListAsync();


            return list;

        }


        //dekori rezervisani iz jedne prodavnice
        //public async Task<List<Decor>> GetReservedDecorsFromStore(int storeId)
        //{

        //}

        // sve rezervcije jedne prodavnice
        public async Task<List<Catalog_Decor>> GetAllReservationsFromStore(int storeId)
        {
            var catalogDecors = await context.Catalog_Decors
                .Where(cd => cd.Catalog.StoreId == storeId)
                .Include(cd => cd.Decor)
                    .ThenInclude(d => d.Decor_Reservations)
                        .ThenInclude(dr => dr.Reservation)
                            .ThenInclude(r => r.Appointments)
                .Where(cd => cd.Decor.Decor_Reservations.Any()) // Dodajte ovaj filter da biste uključili samo dekore sa rezervacijama
                .ToListAsync();

            return catalogDecors;
        }




        //dekori na kojima radi osoblje
        public async Task<List<Decor>> GetAllDecorsFromEmployee(string userId)
        {
            var decorIds = await context.Appointments
                 .Where(a => a.UserId == userId && a.ReservationId != null)
                 .SelectMany(a => a.Reservation.Decor_Reservations.Select(dr => dr.DecorId))
                 .ToListAsync();

            var decorations = await context.Decors
                .Where(d => decorIds.Contains(d.Id))
                .ToListAsync();


            return decorations;

        }

    }
}
