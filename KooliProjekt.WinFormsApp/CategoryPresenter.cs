using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public class CategoryPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly ICategoryView _categoryView;

        public CategoryPresenter(ICategoryView categoryView, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _categoryView = categoryView;

            categoryView.Presenter = this;
        }

        public void UpdateView(Category list)
        {
            if (list == null)
            {
                _categoryView.Title = string.Empty;
                _categoryView.Id = 0;
            }
            else
            {
                _categoryView.Id = list.Id;
                _categoryView.Title = list.Title;
            }
        }

        public async Task Delete()
        {
            if (_categoryView.SelectedItem == null)
            {
                MessageBox.Show("Palun vali kategooria, mida kustutada!", "Hoiatus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var category = _categoryView.SelectedItem;

            var result = MessageBox.Show($"Kas oled kindel, et soovid kategooria '{category.Title}' kustutada?",
                                        "Kustutamise kinnitus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                var deleteResult = await _apiClient.Delete(category.Id);
                if (deleteResult.IsSuccess)
                {
                    MessageBox.Show("Kategooria kustutatud!", "Teade", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Lae andmed uuesti
                    await Load();
                }
                else
                {
                    MessageBox.Show($"Kustutamine ebaõnnestus: {deleteResult.Error}",
                                    "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public async Task Save()
        {

        }

        public async Task Load()
        {
            var category = await _apiClient.List();

            _categoryView.Categories = category.Value;
        }
    }
}