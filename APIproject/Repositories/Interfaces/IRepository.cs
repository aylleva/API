namespace APIproject.Repositories.Interfaces
{
    public interface IRepository
    {
        IQueryable<Category> GetAll();

        Task<Category> GetbyIdAsync(int? id);  

        Task AddAsync(Category category);

        void Delete(Category category);

        void Update(Category category);

        Task<bool> Check(Category category);
        Task<int> SaveChangesAsync();
    }
}
