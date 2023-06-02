using DecorStudio_api.DTOs;
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

        public async Task AddDecor(DecorDto decor)
        {
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
    }
}
