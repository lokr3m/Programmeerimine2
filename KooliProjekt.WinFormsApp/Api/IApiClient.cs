namespace KooliProjekt.WinFormsApp.Api
{
    public interface IApiClient
    {
        Task<Result> Save(Category category);
        Task<Result> Delete(int id);
        Task<Result<List<Category>>> List();
    }
}