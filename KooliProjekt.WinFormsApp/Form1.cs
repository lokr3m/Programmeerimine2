using KooliProjekt.WinFormsApp.Api;

namespace KooliProjekt.WinFormsApp
{
    public partial class Form1 : Form, ICategoryView
    {
        public IList<Category> Categories
        {
            get
            {
                return (IList<Category>)CategoryGrid.DataSource;
            }
            set
            {
                CategoryGrid.DataSource = value;
            }
        }

        public Category SelectedItem { get; set; }

        public CategoryPresenter Presenter { get; set; }

        public string Title
        {
            get
            {
                return TitleField.Text; ;
            }
            set
            {
                TitleField.Text = value;
            }
        }

        public int Id
        {
            get
            {
                return int.Parse(IdField.Text);
            }
            set
            {
                IdField.Text = value.ToString();
            }
        }

        public Form1()
        {
            InitializeComponent();

            CategoryGrid.AutoGenerateColumns = true;
            CategoryGrid.SelectionChanged += CategoryGrid_SelectionChanged;

            NewButton.Click += AddButton_Click;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;

            Load += Form1_Load;
        }

        private async void DeleteButton_Click(object? sender, EventArgs e)
        {
            // Kutsu presenteri Delete meetodi
            await Presenter.Delete();
            
        }

        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            //var category = new Category
            //{
            //    Id = this.Id,
            //    Title = this.Title
            //};

            await Presenter.Save();

            //if (result.IsSuccess)
            //{
            //    MessageBox.Show("Kategooria salvestatud!", "Teade", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Presenter.Load().Wait();
            //}
            //else
            //{
            //    MessageBox.Show($"Salvestamine ebaõnnestus: {result.Error}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void AddButton_Click(object? sender, EventArgs e)
        {
            Presenter.UpdateView(new Category());
        }

        private void CategoryGrid_SelectionChanged(object? sender, EventArgs e)
        {
            if (CategoryGrid.SelectedRows.Count == 0)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = (Category)CategoryGrid.SelectedRows[0].DataBoundItem;
            }

            Presenter.UpdateView(SelectedItem);
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            await Presenter.Load();
        }
    }
}
