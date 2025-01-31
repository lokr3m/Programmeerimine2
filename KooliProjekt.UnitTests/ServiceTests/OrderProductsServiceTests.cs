using KooliProjekt.Data;
using KooliProjekt.Services;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrderProductServiceTests : ServiceTestBase
    {
        private readonly OrderProductsService _service;

        public OrderProductServiceTests()
        {
            _service = new OrderProductsService(DbContext);
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var list = new OrderProduct { Title = "Test" };
            DbContext.OrderProducts.Add(list);
            DbContext.SaveChanges();

            // Act
            await _service.Delete(list.Id);

            // Assert
            var count = DbContext.OrderProducts.Count();
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
            var count = DbContext.OrderProducts.Count();
            Assert.Equal(0, count);
        }
    }
}