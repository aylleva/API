using APIproject.DTO;
using APIproject.DTO.Category;
using APIproject.Repositories.Interfaces;
using APIproject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIproject.Services.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
           _repository = repository;
        }

       

        public async Task<IEnumerable<GetCategoryDTO>> GetAllAsync(int page, int take)
        {
           IEnumerable<GetCategoryDTO> categories= await _repository.GetAll(skip:(page-1)*take,take:take)
                .Select(c=>new GetCategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductCount=c.Products.Count,
                }).ToListAsync();

            return categories;  
        }

        public async Task<GetCategoryDetailDTO> GetbyIdAsync(int id)
        {
          Category category= await _repository.GetbyIdAsync(id);

            if (category is null) throw new Exception("Category does not exist");
            GetCategoryDetailDTO categorydto = new()
            { 
                Id = category.Id,
                Name = category.Name,
               ProductsDto=category.Products.Select(p=> new GetProductDTO
               {
                   Id = p.Id,
                   Name = p.Name,
                   Price = p.Price,
               }).ToList()
            };
            return categorydto;
        }

        public async Task CreateAsync(CreateCategoryDTO categoryDTO)
        {
            if (await _repository.AnyAsync(c => c.Name == categoryDTO.Name)) throw new Exception("Category is already exists");
             await _repository.AddAsync(new() { Name = categoryDTO.Name });
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateCategoryDTO categoryDTO)
        {
           Category category=await _repository.GetbyIdAsync(id);
            if (category is null) throw new Exception("Not Found");

            if (await _repository.AnyAsync(c => c.Name == categoryDTO.Name && c.Id != id)) throw new Exception("Category is already exists");

            category.Name = categoryDTO.Name;
            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }
     

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetbyIdAsync(id);
            if (category is null) throw new Exception("Not Found");

            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }
    }
}
