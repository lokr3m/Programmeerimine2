using KooliProjekt.Data;
using KooliProjekt.Services;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class CategoriesServiceTests : ServiceTestBase
    {
        private readonly CategoriesService _service;

        public CategoriesServiceTests()
        {
            _service = new CategoriesService(DbContext);
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var list = new Category { Name = "Test", Description = "Test"};
            DbContext.Categories.Add(list);
            DbContext.SaveChanges();

            // Act
            await _service.Delete(list.Id);

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
            await _service.Delete(id);

            // Assert
            var count = DbContext.Categories.Count();
            Assert.Equal(0, count);
        }
    }
}