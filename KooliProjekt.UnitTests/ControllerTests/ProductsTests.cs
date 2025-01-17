using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class ProductsTests
    {
        private readonly Mock<IProductsService> _productServiceMock;
        private readonly ProductsController _controller;

        public  ProductsTests()
        {
            _productServiceMock = new Mock<IProductsService>();
            _controller = new ProductsController(_productServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Product>
            {
                new Product { Id = 1, Title = "Test 1" },
                new Product { Id = 2, Title = "Test 2" }
            };
            var pagedResult = new PagedResult<Product> { Results = data };
            _productServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = (ProductsIndexModel)result.Model;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, model.Data);
        }
    }
}
