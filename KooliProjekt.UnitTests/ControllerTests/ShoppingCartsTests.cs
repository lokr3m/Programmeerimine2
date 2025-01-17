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
    public class ShoppingCartsTests
    {
        private readonly Mock<IShoppingCartService> _shoppingCartServiceMock;
        private readonly ShoppingCartsController _controller;

        public ShoppingCartsTests()
        {
            _shoppingCartServiceMock = new Mock<IShoppingCartService>();
            _controller = new ShoppingCartsController(_shoppingCartServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<ShoppingCart>
            {
                new ShoppingCart { Id = 1, Title = "Test 1" },
                new ShoppingCart { Id = 2, Title = "Test 2" }
            };
            var pagedResult = new PagedResult<ShoppingCart> { Results = data };
            _shoppingCartServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = (ShoppingCartsIndexModel)result.Model;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, model.Data);
        }
    }
}
