using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class LinkRepository
    {
        private readonly ApplicationDBContext _context;
        public LinkRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<List<Link>> GetAllAsync()
        {
            var links = await _context.Links.ToListAsync();
            // TODO: фильтрация по ссылкам текущего пользователя
            return links;
        }

        public async Task<Link?> GetByCodeAsync(string code)
        {
            return await _context.Links.FirstOrDefaultAsync(l => l.Code == code);
        }

        public async Task<Link> IncreaseVisitCounter(Link linkModel)
        {
            linkModel.VisitCount++;
            await _context.SaveChangesAsync();
            return linkModel;
        }

        public async Task<Link?> CreateAsync(Link linkModel)
        {
            await _context.Links.AddAsync(linkModel);
            await _context.SaveChangesAsync();
            return linkModel;
        }
    }
}
