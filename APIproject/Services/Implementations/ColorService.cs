using APIproject.DTO.Color;
using APIproject.Repositories.Interfaces;
using APIproject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIproject.Services.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repository;

        public ColorService(IColorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetColorDTO>> GetAllAsync(int page, int take)
        {
          IEnumerable<GetColorDTO> colordto=await _repository.GetAll(skip:(page-1)*take,take:take)
                .Select(c=>new GetColorDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();

            return colordto;
        }

        public async Task<GetColorDetailDTO> GetByIdAsync(int id)
        {
           Color color=await _repository.GetbyIdAsync(id);
           if (color == null) throw new Exception("Not Found");

            GetColorDetailDTO colordto = new()
            {
                Id = color.Id,
                Name = color.Name,
            };
            return colordto;
        }

        public async Task CreateAsync(CreateColorDTO colordto)
        {
            if (await _repository.AnyAsync(c => c.Name == colordto.Name)) throw new Exception("This Color is already Exist");
           await _repository.AddAsync(new() { Name = colordto.Name });
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int Id, UpdateColorDTO colordto)
        {
            Color color=await _repository.GetbyIdAsync(Id);
            if (color == null) throw new Exception("Not Found");

            if (await _repository.AnyAsync(c => c.Name == colordto.Name && c.Id!=Id)) throw new Exception("This Color is already Exist");

            color.Name = colordto.Name;
            _repository.Update(color);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Color color = await _repository.GetbyIdAsync(Id);
            if (color == null) throw new Exception("Not Found");

            _repository.Delete(color);

        await _repository.SaveChangesAsync();
        }
    }
}
