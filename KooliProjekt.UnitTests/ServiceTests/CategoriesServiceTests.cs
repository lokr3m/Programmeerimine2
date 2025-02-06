using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class CategoriesServiceTests : ServiceTestBase
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesServiceTests()
        {
            _categoriesService = new CategoriesService(DbContext);
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var list = new Category { Name = "Test", Description = "Test"};
            DbContext.Categories.Add(list);
            DbContext.SaveChanges();

            // Act
            await _categoriesService.Delete(list.Id);

            // Assert
            var count = DbContext.Categories.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Delete_should_return_if_list_was_not_found()
        {
            // Arrange
            var id = -100;

            // Act
            await _categoriesService.Delete(id);

            // Assert
            var count = DbContext.Categories.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_existing_category()
        {
            // Arrange
            var category = new Category { Name = "Test", Description = "Test" };
            DbContext.Categories.Add(category);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _categoriesService.Get(category.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(category.Id, result.Id);
        }

        [Fact]
        public async Task Save_should_add_new_category()
        {
            // Arrange
            var category = new Category { Name = "New Cat", Description = "New Desc" };

            // Act
            await _categoriesService.Save(category);

            // Assert
            var savedCategory = await DbContext.Categories.FirstOrDefaultAsync(c => c.Name == "New Cat");
            Assert.NotNull(savedCategory);
            Assert.Equal("New Desc", savedCategory.Description);
        }

        [Fact]
        public async Task Save_should_update_existing_categories()
        { 
            // Arrange
            var service = new CategoriesService(DbContext);

            DbContext.Categories.RemoveRange(DbContext.Categories);
            await DbContext.SaveChangesAsync();

            var existingCategory = new Category {
                Id = 1,
                Name = "Clothes",
                Description = "Clothes",
                Title = "Test",
            };

            DbContext.Categories.Add(existingCategory);
            await DbContext.SaveChangesAsync();

            existingCategory.Title = "Test";

            await service.Save(existingCategory);

            // Act
            var updatedCategory = await DbContext.Categories.FindAsync(existingCategory.Id);

            // Assert
            Assert.NotNull(updatedCategory);
            Assert.Equal("Test", updatedCategory.Title);  // Corrected property name here as well
        }
    }
}