namespace KooliProjekt.WinFormsApp.Api
{
    public interface IApiClient
    {
        Task<Result<List<Category>>> List();
        Task Save(Category list);
        Task Delete(int id);
    }
}