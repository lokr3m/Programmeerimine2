using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ShoppingCartsServiceTests : ServiceTestBase
    {
        private readonly ShoppingCartService _shoppingCartsService;

        public ShoppingCartsServiceTests()
        {
            _shoppingCartsService = new ShoppingCartService(DbContext);
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var list = new ShoppingCart { Title = "Test" };
            DbContext.ShoppingCarts.Add(list);
            DbContext.SaveChanges();

            // Act
            await _shoppingCartsService.Delete(list.Id);

            // Assert
            var count = DbContext.ShoppingCarts.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Delete_should_return_if_list_was_not_found()
        {
            // Arrange
            var id = -100;

            // Act
            await _shoppingCartsService.Delete(id);

            // Assert
            var count = DbContext.ShoppingCarts.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_existing_shopping_carts()
        {
            // Arrange
            var shoppingCarts = new ShoppingCart { TotalPrice = 3, TotalQuantity = 3 };
            DbContext.ShoppingCarts.Add(shoppingCarts);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _shoppingCartsService.Get(shoppingCarts.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(shoppingCarts.Id, result.Id);
        }

        [Fact]
        public async Task Save_should_add_new_shopping_carts()
        {
            // Arrange
            var shoppingCarts = new ShoppingCart { TotalPrice = 3, TotalQuantity = 3 };

            // Act
            await _shoppingCartsService.Save(shoppingCarts);

            // Assert
            var savedshoppingCarts = await DbContext.ShoppingCarts.FirstOrDefaultAsync(c => c.TotalPrice == 3);
            Assert.NotNull(savedshoppingCarts);
            Assert.Equal(3, savedshoppingCarts.TotalQuantity);
        }

        [Fact]
        public async Task Save_should_update_existing_shopping_carts()
        {
            // Arrange
            var service = new ShoppingCartService(DbContext);

            DbContext.ShoppingCarts.RemoveRange(DbContext.ShoppingCarts);
            await DbContext.SaveChangesAsync();

            var existingShoppingCart = new ShoppingCart
            {
                Id = 1,
                TotalPrice = 3,
                TotalQuantity = 3,
                Title = "Test",
            };

            DbContext.ShoppingCarts.Add(existingShoppingCart);
            await DbContext.SaveChangesAsync();

            existingShoppingCart.Title = "Test";

            await service.Save(existingShoppingCart);

            // Act
            var updatedShoppingCarts = await DbContext.ShoppingCarts.FindAsync(existingShoppingCart.Id);

            // Assert
            Assert.NotNull(updatedShoppingCarts);
            Assert.Equal("Test", updatedShoppingCarts.Title);
        }

        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            DbContext.ShoppingCarts.AddRange(new List<ShoppingCart>
            {
                new ShoppingCart { TotalPrice = 100, TotalQuantity = 3},
                new ShoppingCart { TotalPrice = 130, TotalQuantity = 2},
                new ShoppingCart { TotalPrice = 12, TotalQuantity = 5},
            });
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _shoppingCartsService.List(1, 2, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Results.Count);
        }

        [Fact]
        public async Task List_should_filter_by_keyword()
        {
            // Arrange
            DbContext.ShoppingCarts.AddRange(new List<ShoppingCart>
            {
                new ShoppingCart { Title = "Bottle of water", TotalPrice = 12, TotalQuantity = 5},
            });
            await DbContext.SaveChangesAsync();

            var search = new ShoppingCartsSearch { Keyword = "Bottle of water" };

            // Act
            var result = await _shoppingCartsService.List(1, 2, search);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Results);
            Assert.Equal("Bottle of water", result.Results.First().Title);
        }
    }
}