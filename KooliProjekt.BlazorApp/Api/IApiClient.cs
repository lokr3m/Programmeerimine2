using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.BlazorApp
{
    public interface IApiClient
    {
        Task<Result<Category>> Get(int id);
        Task<Result<List<Category>>> List();
        Task<Result> Save(Category category);
        Task Delete(int id);
    }
}