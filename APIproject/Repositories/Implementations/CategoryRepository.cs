using APIproject.Repositories.Interfaces;

namespace APIproject.Repositories.Implementations
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context) { }
        
    }
}
