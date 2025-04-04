
using KooliProjekt.WinFormsApp.Api;
using System.ComponentModel;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly IApiClient _apiClient;

        public Form1()
        {
            InitializeComponent();

            _apiClient = new ApiClient();

            CategoryGrid.SelectionChanged += CategoryGrid_SelectionChanged;

            NewButton.Click += NewButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;
        }
        private async Task LoadDataAsync()
        {
            var response = await _apiClient.List();
            if (response.IsSuccess)
            {
                CategoryGrid.DataSource = response.Value;
            }
            else
            {
                MessageBox.Show($"Andmete laadimine ebaõnnestus: {response.Error}",
                        "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (CategoryGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Palun vali kategooria, mida kustutada!", "Hoiatus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var category = (Category)CategoryGrid.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show($"Kas oled kindel, et soovid kategooria '{category.Title}' kustutada?",
                                         "Kustutamise kinnitus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                var deleteResult = await _apiClient.Delete(category.Id);
                if (deleteResult.IsSuccess)
                {
                    MessageBox.Show("Kategooria kustutatud!", "Teade", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show($"Kustutamine ebaõnnestus: {deleteResult.Error}",
                                    "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleField.Text))
            {
                MessageBox.Show("Palun sisesta kategooria nimi!", "Hoiatus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Category category;
            if (string.IsNullOrWhiteSpace(IdField.Text) || IdField.Text == "0")
            {
                // Luuakse uus kategooria
                category = new Category { Title = TitleField.Text, Name = NameField.Text, Description = DescriptionField.Text };
                var createResult = await _apiClient.Save(category);
                if (!createResult.IsSuccess)
                {
                    MessageBox.Show($"Lisamine ebaõnnestus: {createResult.Error}",
                                    "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                // Uuendatakse olemasolevat kategooriat
                category = new Category
                {
                    Id = int.Parse(IdField.Text),
                    Title = TitleField.Text,
                    Name = NameField.Text,
                    Description = DescriptionField.Text
                };

                var updateResult = await _apiClient.Save(category);
                if (!updateResult.IsSuccess)
                {
                    MessageBox.Show($"Uuendamine ebaõnnestus: {updateResult.Error}",
                                    "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            MessageBox.Show("Kategooria salvestatud!", "Teade", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadDataAsync();
        }

        private void NewButton_Click(object? sender, EventArgs e)
        {
            IdField.Text = string.Empty;
            TitleField.Text = string.Empty;
        }

        private void CategoryGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (CategoryGrid.SelectedRows.Count == 0)
            {
                return;
            }

            var category = (Category)CategoryGrid.SelectedRows[0].DataBoundItem;

            if (category == null)
            {
                IdField.Text = string.Empty;
                TitleField.Text = string.Empty;
            }
            else
            {
                IdField.Text = category.Id.ToString();
                TitleField.Text = category.Title;
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var apiClient = new ApiClient();
            var response = await apiClient.List();

            CategoryGrid.AutoGenerateColumns = true;
            CategoryGrid.DataSource = response.Value;

        }

        private void TitleField_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewButton_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
