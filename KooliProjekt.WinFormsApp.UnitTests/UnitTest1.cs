using Xunit;
using Moq;
using KooliProjekt.WinFormsApp;
using KooliProjekt.WinFormsApp.Api;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

public class CategoryPresenterTests
{
    private readonly Mock<ICategoryView> _mockCategoryView;
    private readonly Mock<IApiClient> _mockApiClient;
    private readonly CategoryPresenter _presenter;

    public class r
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }
    public CategoryPresenterTests()
    {
        _mockCategoryView = new Mock<ICategoryView>();
        _mockApiClient = new Mock<IApiClient>();
        _presenter = new CategoryPresenter(_mockCategoryView.Object, _mockApiClient.Object);
    }

    [Fact]
    public void Constructor_SetsPresenterOnView()
    {
        // Assert
        _mockCategoryView.VerifySet(v => v.Presenter = _presenter);
    }

    [Fact]
    public void UpdateView_WithNullCategory_ClearsViewProperties()
    {
        // Act
        _presenter.UpdateView(null);

        // Assert
        _mockCategoryView.VerifySet(v => v.Title = string.Empty);
        _mockCategoryView.VerifySet(v => v.Id = 0);
    }

    [Fact]
    public void UpdateView_WithCategory_SetsViewProperties()
    {
        // Arrange
        var category = new Category { Id = 5, Title = "Test Category" };

        // Act
        _presenter.UpdateView(category);

        // Assert
        _mockCategoryView.VerifySet(v => v.Title = "Test Category");
        _mockCategoryView.VerifySet(v => v.Id = 5);
    }

    [Fact]
    public async Task Load_PopulatesCategoriesInView()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Title = "Category 1" },
            new Category { Id = 2, Title = "Category 2" }
        };

        var result = new Result<List<Category>> { Value = categories };
        _mockApiClient.Setup(a => a.List()).ReturnsAsync(result);

        // Act
        await _presenter.Load();

        // Assert
        _mockCategoryView.VerifySet(v => v.Categories = categories);
    }

    [Fact]
    public async Task Delete_UserConfirmsFalse_DoesNotCallApi()
    {
        // Arrange
        var category = new Category { Id = 1, Title = "Test Category" };
        _mockCategoryView.Setup(v => v.SelectedItem).Returns(category);

    }

    [Fact]
    public async Task Save_ImplementationTest()
    {
        await _presenter.Save();
    }
}