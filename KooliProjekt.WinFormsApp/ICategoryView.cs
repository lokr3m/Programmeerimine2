using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public interface ICategoryView
    {
        IList<Category> Categories { get; set; }
        Category SelectedItem { get; set; }
        string Title { get; set; }
        int Id { get; set; }
        CategoryPresenter Presenter { get; set; }
    }
}
