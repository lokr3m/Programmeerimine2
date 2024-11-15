using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface ICategoriesService
    {
        Task<PagedResult<Category>> List(int page, int pageSize);
        Task<Category> Get(int id);
        Task Save(Category list);
        Task Delete(int id);
    }
}