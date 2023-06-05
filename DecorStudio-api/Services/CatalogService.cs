using DecorStudio_api.DTOs;
using DecorStudio_api.Models;
using Microsoft.EntityFrameworkCore;

namespace DecorStudio_api.Services
{
    public class CatalogService
    {
        private readonly AppDbContext context;

        public CatalogService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Catalog> CreateCatalog(CatalogDto catalogDto)
        {
            var store = await context.Stores.FirstOrDefaultAsync(s => s.Id == catalogDto.StoreId);
            if (store == null)
            {
                throw new Exception("Store not found");
            }
            var catalog = new Catalog
            {
                Name = catalogDto.Name,
                StoreId = catalogDto.StoreId
            };
            await context.Catalogs.AddAsync(catalog);
            await context.SaveChangesAsync();
            return catalog;
        }

        public async Task<List<Catalog>> GetCatalogs(int storeId)
        {
            return await context.Catalogs.Where(c => c.StoreId == storeId).Include(c => c.Store).Include(c => c.Catalog_Decors).ToListAsync();
        }

        public async Task<Catalog> GetCatalog(int storeId, int catalogId)
        {
            return await context.Catalogs.Include(c => c.Store).Include(c => c.Catalog_Decors).FirstOrDefaultAsync(c => c.Id == catalogId && c.StoreId == storeId);
        }

        public async Task<Catalog> UpdateCatalog(int id, CatalogDto catalogDto)
        {
            var store = await context.Stores.FirstOrDefaultAsync(s => s.Id == catalogDto.StoreId);
            if (store == null)
            {
                throw new Exception("Store not found");
            }
            var catalog = await context.Catalogs.FirstOrDefaultAsync(c => c.Id == id);
            if (catalog == null)
            {
                throw new Exception("Catalog not found");
            }
            catalog.Name = catalogDto.Name;
            catalog.StoreId = catalogDto.StoreId;
            await context.SaveChangesAsync();
            return catalog;
        }

        public async Task<Catalog> DeleteCatalog(int id)
        {
            var catalog = await context.Catalogs.FirstOrDefaultAsync(c => c.Id == id);
            if (catalog == null)
            {
                return null;
            }
            context.Catalogs.Remove(catalog);
            await context.SaveChangesAsync();
            return catalog;
        }

        //decor-catalog
        public async Task<Catalog_Decor> CreateCatalog_Decor(Catalog_DecorDto catalog_DecorDto)
        {
            var catalog = await context.Catalogs.FirstOrDefaultAsync(c => c.Id == catalog_DecorDto.CatalogId);
            if (catalog == null)
            {
                throw new Exception("Catalog not found");
            }
            var decor = await context.Decors.FirstOrDefaultAsync(d => d.Id == catalog_DecorDto.DecorId);
            if (decor == null)
            {
                throw new Exception("Decor not found");
            }
            var catalog_Decor = new Catalog_Decor
            {
                CatalogId = catalog_DecorDto.CatalogId,
                DecorId = catalog_DecorDto.DecorId
            };
            await context.Catalog_Decors.AddAsync(catalog_Decor);
            await context.SaveChangesAsync();
            return catalog_Decor;
        }

        //delete decor from catalog
        public async Task<Catalog_Decor> DeleteCatalog_Decor(int catalogId, int decorId)
        {
            var catalog_Decor = await context.Catalog_Decors.FirstOrDefaultAsync(cd => cd.CatalogId == catalogId && cd.DecorId == decorId);
            if (catalog_Decor == null)
            {
                throw new Exception("not found");
            }
            context.Catalog_Decors.Remove(catalog_Decor);
            await context.SaveChangesAsync();
            return catalog_Decor;
        }

        //all decors from catalog
        public async Task<List<Catalog_Decor>> GetCatalog_Decors(int catalogId)
        {
            return await context.Catalog_Decors.Where(cd => cd.CatalogId == catalogId).Include(cd => cd.Catalog).Include(cd => cd.Decor).ToListAsync();
        }
    }
}
