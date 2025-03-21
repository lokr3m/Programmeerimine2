﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Build.Evaluation;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class CartProductsTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public CartProductsTests()
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
            using var response = await _client.GetAsync("/CartProducts");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/CartProducts/Details")]
        [InlineData("/CartProducts/Details/100")]
        [InlineData("/CartProducts/Delete")]
        [InlineData("/CartProducts/Delete/100")]
        [InlineData("/CartProducts/Edit")]
        [InlineData("/CartProducts/Edit/100")]
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
            using var response = await _client.GetAsync("/CartProducts/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]

        public async Task Details_should_return_success_when_list_was_found()
        {
            // Arrange
            var category = new Category { Name = "Product1", Description = "Test" };
            var product = new Product { Name = "Product1", Description = "Test", Category = category };
            var cart = new ShoppingCart { DateCreated = DateTime.Now, Title = "Test" };
            var cartProduct = new CartProduct { Title = "Test", Product = product, ShoppingCart = cart };
            _context.Add(product);
            _context.Add(cartProduct);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/CartProducts/Details/" + cartProduct.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_should_save_new_cart_product()
        {
            // Arrange
            var category = new Category { Name = "Test", Title="test", Description="´Test" };
            var product = new Product { Name = "Product1", Description = "Test", Category = category };
            var cart = new ShoppingCart { DateCreated = DateTime.Now, Title = "Test" };
            _context.Add(product);
            _context.Add(cart);
            await _context.SaveChangesAsync();

            var formValues = new Dictionary<string, string>();
            formValues.Add("ProductName", "Product1");
            formValues.Add("Title", "Test");
            formValues.Add("ProductId", product.Id.ToString());
            formValues.Add("ShoppingCartId", cart.Id.ToString());


            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/CartProducts/Create", content);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.CartProducts.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Id);
            Assert.Equal("Product1", list.ProductName);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("ProductId", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/CartProducts/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.False(_context.CartProducts.Any());
        }
    }
}