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
    public class CartProductsControllerTests
    {
        private readonly Mock<ICartProductsService> _cartproductMock;
        private readonly CartProductsController _controller;

        public CartProductsControllerTests()
        {
            _cartproductMock = new Mock<ICartProductsService>();
            _controller = new CartProductsController(_cartproductMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<CartProduct>
            {
                new CartProduct { Id = 1, Title = "Test 1" },
                new CartProduct { Id = 2, Title = "Test 2" }
            };
            var pagedResult = new PagedResult<CartProduct> { Results = data };
            _cartproductMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = (CartProductsIndexModel)result.Model;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, model.Data);
        }
    }
}
