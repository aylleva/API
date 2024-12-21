using APIproject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIproject.Repositories.Implementations
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
          _context = context;
        }
        public IQueryable<Category> GetAll()
        {
           return _context.Categories;
        }

        public async Task<Category> GetbyIdAsync(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);
        }


        public async Task AddAsync(Category category)
        {
           await _context.Categories.AddAsync(category);
        }

        public void Delete(Category category)
        {
           _context.Categories.Remove(category);
        }


        public void Update(Category category)
        {
          _context.Categories.Update(category);
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public async Task<bool> Check(Category category)
        {
            return await _context.Categories.AnyAsync(c => c.Name.Trim() == category.Name.Trim());
        }
    }
}
