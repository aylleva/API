using APIproject.DTO;
using APIproject.DTO.Category;

namespace APIproject.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDTO>> GetAllAsync(int page, int take);

        Task<GetCategoryDetailDTO> GetbyIdAsync(int id);

        Task CreateAsync(CreateCategoryDTO categoryDTO);

        Task UpdateAsync(int id, UpdateCategoryDTO categoryDTO);

        Task DeleteAsync(int id);
    }
}
