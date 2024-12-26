using APIproject.DTO;
namespace APIproject
{
    public class GetCategoryDetailDTO
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public ICollection<GetProductDTO> ProductsDto { get; set; }
    }
}
