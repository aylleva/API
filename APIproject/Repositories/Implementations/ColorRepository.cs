using APIproject.Repositories.Interfaces;

namespace APIproject.Repositories.Implementations
{
    public class ColorRepository:Repository<Color>,IColorRepository
    {
        public ColorRepository(AppDbContext context):base(context) { }
       
    }
}
