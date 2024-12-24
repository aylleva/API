using APIproject.Repositories.Interfaces;

namespace APIproject.Repositories.Implementations
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context) { }

    
    }
}
