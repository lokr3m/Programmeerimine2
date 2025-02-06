using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ProductsServiceTests : ServiceTestBase
    {
        private readonly ProductsService _productsService;

        public ProductsServiceTests()
        {
            _productsService = new ProductsService(DbContext);
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var list = new Product { Name = "Test", Description = "Test" };
            DbContext.Products.Add(list);
            DbContext.SaveChanges();

            // Act
            await _productsService.Delete(list.Id);

            // Assert
            var count = DbContext.Products.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Delete_should_return_if_list_was_not_found()
        {
            // Arrange
            var id = -100;

            // Act
            await _productsService.Delete(id);

            // Assert
            var count = DbContext.Products.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_existing_products()
        {
            // Arrange
            var product = new Product { Name = "Test", Description = "Test" };
            DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await _productsService.Get(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact]
        public async Task Save_should_add_new_products()
        {
            // Arrange
            var product = new Product { Name = "New Cat", Description = "New Desc" };

            // Act
            await _productsService.Save(product);

            // Assert
            var savedProducts = await DbContext.Products.FirstOrDefaultAsync(c => c.Name == "New Cat");
            Assert.NotNull(savedProducts);
            Assert.Equal("New Desc", savedProducts.Description);
        }

        [Fact]
        public async Task Save_should_update_existing_products()
        {
            // Arrange
            var service = new ProductsService(DbContext);

            DbContext.Products.RemoveRange(DbContext.Products);
            await DbContext.SaveChangesAsync();

            var existingProduct = new Product
            {
                Id = 1,
                Name = "Clothes",
                Description = "Clothes",
                Title = "Test",
            };

            DbContext.Products.Add(existingProduct);
            await DbContext.SaveChangesAsync();

            existingProduct.Title = "Test";

            await service.Save(existingProduct);

            // Act
            var updatedProduct = await DbContext.Products.FindAsync(existingProduct.Id);

            // Assert
            Assert.NotNull(updatedProduct);
            Assert.Equal("Test", updatedProduct.Title);
        }
    }
}