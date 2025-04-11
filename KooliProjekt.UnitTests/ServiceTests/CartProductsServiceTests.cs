using KooliProjekt.Data;
using KooliProjekt.Data.Migrations;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class CartProductsServiceTests : ServiceTestBase
    {
        private readonly CartProductsService _cartProductsService;

        public CartProductsServiceTests()
        {
            _cartProductsService = new CartProductsService(DbContext);
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var list = new CartProduct { Title = "Test" };
            DbContext.CartProducts.Add(list);
            DbContext.SaveChanges();

            // Act
            await _cartProductsService.Delete(list.Id);

            // Assert
            var count = DbContext.CartProducts.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Delete_should_return_if_list_was_not_found()
        {
            // Arrange
            var id = -100;
            // Act
            await _cartProductsService.Delete(id);

            // Assert
            var count = DbContext.CartProducts.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_existing_order_products()
        {
            // Arrange
            var cartProducts = new CartProduct { ProductName = "Test", Quantity = 3 };
            DbContext.CartProducts.Add(cartProducts);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _cartProductsService.Get(cartProducts.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cartProducts.Id, result.Id);
        }

        [Fact]
        public async Task Save_should_add_new_order_cart_products()
        {
            // Arrange
            var cartProduct = new CartProduct { ProductName = "New Cat", Quantity = 3 };

            // Act
            await _cartProductsService.Save(cartProduct);

            // Assert
            var savedcartProducts = await DbContext.CartProducts.FirstOrDefaultAsync(c => c.ProductName == "New Cat");
            Assert.NotNull(savedcartProducts);
            Assert.Equal(3, savedcartProducts.Quantity);
        }

        [Fact]
        public async Task Save_should_update_existing_order_products()
        {
            // Arrange
            var service = new CartProductsService(DbContext);

            DbContext.CartProducts.RemoveRange(DbContext.CartProducts);
            await DbContext.SaveChangesAsync();

            var existingCartProducts = new CartProduct
            {
                Id = 1,
                ProductName = "Clothes",
                Quantity = 3,
                Title = "Test",
            };

            DbContext.CartProducts.Add(existingCartProducts);
            await DbContext.SaveChangesAsync();

            existingCartProducts.Title = "Test";

            await service.Save(existingCartProducts);

            // Act
            var updatedCartProduct = await DbContext.CartProducts.FindAsync(existingCartProducts.Id);

            // Assert
            Assert.NotNull(updatedCartProduct);
            Assert.Equal("Test", updatedCartProduct.Title);
        }

        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            var product = new Product { Name = "Product1", Description = "Product1" };
            var cart = new ShoppingCart { Title = "ShoppingCart1" };
            DbContext.CartProducts.AddRange(new List<CartProduct>
            {
                new CartProduct { ProductName = "Wallet", Title = "Wallet for money", Product = product, ShoppingCart = cart },
                new CartProduct { ProductName = "Watches", Title = "Pocket waches", Product = product, ShoppingCart = cart},
                new CartProduct { ProductName = "Clothes", Title = "Men clothes", Product = product, ShoppingCart = cart},
            });
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _cartProductsService.List(1, 3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Results.Count);
        }

        [Fact]
        public async Task List_should_filter_by_keyword()
        {
            // Arrange
            DbContext.CartProducts.AddRange(new List<CartProduct>
            {
                new CartProduct 
                { 
                    Title = "Wallet", 
                    ProductName = "GoldWallet",
                    Product = new Product 
                    {
                        Name = "SilverWallet",
                        Description = "-"
                    },
                    ShoppingCart = new ShoppingCart
                    {
                        Title = "Wallet"
                    }
                },
            });
            await DbContext.SaveChangesAsync();

            var search = new CartProductsSearch { Keyword = "Wallet" };

            // Act
            var result = await _cartProductsService.List(1, 2, search);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Results);
            Assert.Equal("Wallet", result.Results.First().Title);
        }
    }
}