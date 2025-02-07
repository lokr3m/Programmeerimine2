using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrderProductServiceTests : ServiceTestBase
    {
        private readonly OrderProductsService _orderProductsService;

        public OrderProductServiceTests()
        {
            _orderProductsService = new OrderProductsService(DbContext);
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var list = new OrderProduct { Title = "Test" };
            DbContext.OrderProducts.Add(list);
            DbContext.SaveChanges();

            // Act
            await _orderProductsService.Delete(list.Id);

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
            await _orderProductsService.Delete(id);

            // Assert
            var count = DbContext.OrderProducts.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_existing_order_products()
        {
            // Arrange
            var orderProducts = new OrderProduct { Name = "Test", Quantity = 3};
            DbContext.OrderProducts.Add(orderProducts);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _orderProductsService.Get(orderProducts.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderProducts.Id, result.Id);
        }

        [Fact]
        public async Task Save_should_add_new_order_products()
        {
            // Arrange
            var orderProducts = new OrderProduct { Name = "New Cat", Quantity = 3 };

            // Act
            await _orderProductsService.Save(orderProducts);

            // Assert
            var savedorderProducts = await DbContext.OrderProducts.FirstOrDefaultAsync(c => c.Name == "New Cat");
            Assert.NotNull(savedorderProducts);
            Assert.Equal(3, savedorderProducts.Quantity);
        }

        [Fact]
        public async Task Save_should_update_existing_order_products()
        {
            // Arrange
            var service = new OrderProductsService(DbContext);

            DbContext.OrderProducts.RemoveRange(DbContext.OrderProducts);
            await DbContext.SaveChangesAsync();

            var existingOrderProducts = new OrderProduct
            {
                Id = 1,
                Name = "Clothes",
                Quantity = 3,
                Title = "Test",
            };

            DbContext.OrderProducts.Add(existingOrderProducts);
            await DbContext.SaveChangesAsync();

            existingOrderProducts.Title = "Test";

            await service.Save(existingOrderProducts);

            // Act
            var updatedOrderProduct = await DbContext.OrderProducts.FindAsync(existingOrderProducts.Id);

            // Assert
            Assert.NotNull(updatedOrderProduct);
            Assert.Equal("Test", updatedOrderProduct.Title);
        }

        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            DbContext.OrderProducts.AddRange(new List<OrderProduct>
            {
                new OrderProduct { Name = "Apple" },
                new OrderProduct { Name = "Pear" },
                new OrderProduct { Name = "Orange" },
            });
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _orderProductsService.List(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Results.Count);
        }

        [Fact]
        public async Task List_should_filter_by_keyword()
        {
            // Arrange
            DbContext.OrderProducts.AddRange(new List<OrderProduct>
            {
                new OrderProduct
                {
                    Name = "Apple",
                    Product = new Product
                    {
                        Name = "Juice",
                        Description = "-"
                    },
                    Order = new Order
                    {
                        Name = "Apple",
                        Status = "Done"
                    }
                },
            });
            await DbContext.SaveChangesAsync();

            var search = new OrderProductsSearch { Keyword = "Apple" };

            // Act
            var result = await _orderProductsService.List(1, 2, search);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Results);
            Assert.Equal("Apple", result.Results.First().Name);
        }
    }
}