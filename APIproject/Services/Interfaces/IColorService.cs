using APIproject.DTO.Color;

namespace APIproject.Services.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<GetColorDTO>> GetAllAsync(int page,int take);

        Task<GetColorDetailDTO> GetByIdAsync(int id);

        Task CreateAsync(CreateColorDTO colordto);
        Task UpdateAsync(int Id,UpdateColorDTO colordto);

        Task DeleteAsync(int Id);
    }
}
