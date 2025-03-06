namespace KooliProjekt.WpfApp.Api
{
    public interface IApiClient
    {
        Task<IList<Category>> List();
        Task Save(Category list);
        Task Delete(int id);
    }
}