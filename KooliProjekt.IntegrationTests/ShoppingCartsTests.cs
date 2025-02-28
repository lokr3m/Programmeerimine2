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
    public class ShoppingCartsTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public ShoppingCartsTests()
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
            using var response = await _client.GetAsync("/ShoppingCarts");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/ShoppingCarts/Details")]
        [InlineData("/ShoppingCarts/Details/100")]
        [InlineData("/ShoppingCarts/Delete")]
        [InlineData("/ShoppingCarts/Delete/100")]
        [InlineData("/ShoppingCarts/Edit")]
        [InlineData("/ShoppingCarts/Edit/100")]
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
            using var response = await _client.GetAsync("/ShoppingCarts/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]

        public async Task Details_should_return_success_when_list_was_found()
        {
            // Arrange
            var list = new ShoppingCart { Title = "Test" };
            _context.ShoppingCarts.Add(list);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/ShoppingCarts/Details/" + list.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_should_save_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("Title", "Test");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/ShoppingCarts/Create", content);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.ShoppingCarts.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Id);
            Assert.Equal("Test", list.Title);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("TotalPrice", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/ShoppingCarts/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.False(_context.ShoppingCarts.Any());
        }
    }
}
