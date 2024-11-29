using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _context;

        public CategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Category>> List(int page, int pageSize, CategoriesSearch search = null)
        {
            var query = _context.Categories.AsQueryable();

            if (search != null)
            {
                if(!string.IsNullOrEmpty(search.Keyword))
                {
                    query = query.Where(c => 
                        c.Name.Contains(search.Keyword) ||
                        c.Description.Contains(search.Keyword)
                    );
                }
            }

            return await query.GetPagedAsync(page, 5);
        }

        public async Task<Category> Get(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Category list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Category = await _context.Categories.FindAsync(id);
            if (Category != null)
            {
                _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();
            }
        }
    }
}