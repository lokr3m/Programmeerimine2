using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class OrderProductsTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public OrderProductsTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]

        public async Task Index_should_return_success()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/OrderProducts");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/OrderProducts/Details")]
        [InlineData("/OrderProducts/Details/100")]
        [InlineData("/OrderProducts/Delete")]
        [InlineData("/OrderProducts/Delete/100")]
        [InlineData("/OrderProducts/Edit")]
        [InlineData("/OrderProducts/Edit/100")]
        public async Task Should_return_notfound(string url)
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/OrderProducts/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]

        public async Task Details_should_return_success_when_list_was_found()
        {
            // Arrange
            var category = new Category { Name = "Product1", Description = "Test" };
            var product = new Product { Name = "Product1", Description = "Test", Category = category };
            var order = new Order { Title = "Test", Status = "Done", OrderDate = DateTime.Now};
            var orderProduct = new OrderProduct { Title = "Test", Product = product, Order = order };
            _context.Add(product);
            _context.Add(orderProduct);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/OrderProducts/Details/" + orderProduct.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_should_save_new_order_product()
        {
            // Arrange
            var category = new Category { Name = "Test", Title = "test", Description = "´Test" };
            var product = new Product { Name = "Product1", Description = "Test", Category = category };
            var order = new Order { Title = "Test", Status = "Done", OrderDate = DateTime.Now };
            _context.Add(product);
            _context.Add(order);
            await _context.SaveChangesAsync();

            var formValues = new Dictionary<string, string>();
            formValues.Add("ProductName", "Product1");
            formValues.Add("Title", "Test");
            formValues.Add("ProductId", product.Id.ToString());
            formValues.Add("OrderId", order.Id.ToString());


            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/OrderProducts/Create", content);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.OrderProducts.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Id);
            Assert.Equal("Product1", list.ProductName);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("Quantity", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/OrderProducts/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.False(_context.OrderProducts.Any());
        }
    }
}