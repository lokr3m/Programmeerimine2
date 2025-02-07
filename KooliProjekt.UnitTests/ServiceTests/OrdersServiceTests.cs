using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrdersServiceTests : ServiceTestBase
    {
        private readonly OrdersService _ordersService;

        public OrdersServiceTests()
        {
            _ordersService = new OrdersService(DbContext);
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var list = new Order { Status = "Test" };
            DbContext.Orders.Add(list);
            DbContext.SaveChanges();

            // Act
            await _ordersService.Delete(list.Id);

            // Assert
            var count = DbContext.Orders.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Delete_should_return_if_list_was_not_found()
        {
            // Arrange
            var id = -100;

            // Act
            await _ordersService.Delete(id);

            // Assert
            var count = DbContext.Orders.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_existing_orders()
        {
            // Arrange
            var orders = new Order { Name = "Test", Status = "Test" };
            DbContext.Orders.Add(orders);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _ordersService.Get(orders.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orders.Id, result.Id);
        }

        [Fact]
        public async Task Save_should_add_new_orders()
        {
            // Arrange
            var order = new Order { Name = "New Cat", Status = "New Desc" };

            // Act
            await _ordersService.Save(order);

            // Assert
            var savedOrders = await DbContext.Orders.FirstOrDefaultAsync(c => c.Name == "New Cat");
            Assert.NotNull(savedOrders);
            Assert.Equal("New Desc", savedOrders.Status);
        }

        [Fact]
        public async Task Save_should_update_existing_orders()
        {
            // Arrange
            var service = new OrdersService(DbContext);

            DbContext.Orders.RemoveRange(DbContext.Orders);
            await DbContext.SaveChangesAsync();

            var existingOrders = new Order
            {
                Id = 1,
                Name = "Clothes",
                Status = "Done",
                Title = "Test",
            };

            DbContext.Orders.Add(existingOrders);
            await DbContext.SaveChangesAsync();

            existingOrders.Title = "Test";

            await service.Save(existingOrders);

            // Act
            var updatedOrder = await DbContext.Orders.FindAsync(existingOrders.Id);

            // Assert
            Assert.NotNull(updatedOrder);
            Assert.Equal("Test", updatedOrder.Title);
        }

        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            DbContext.Orders.AddRange(new List<Order>
            {
                new Order { Name = "Tech", Status = "Done"},
                new Order { Name = "Sports", Status = "Shipped"},
                new Order { Name = "Food", Status = "Unavailable"}
            });
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _ordersService.List(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task List_should_filter_by_keyword()
        {
            // Arrange
            DbContext.Orders.AddRange(new List<Order>
            {
                new Order { Name = "Tech", Status = "Done"},
                new Order { Name = "Sports", Status = "Shipped"},
                new Order { Name = "Food", Status = "Unavailable"}
            });
            await DbContext.SaveChangesAsync();

            var search = new OrdersSearch { Keyword = "Tech" };

            // Act
            var result = await _ordersService.List(1, 2, search);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Results);
            Assert.Equal("Tech", result.Results.First().Name);
        }
    }
}