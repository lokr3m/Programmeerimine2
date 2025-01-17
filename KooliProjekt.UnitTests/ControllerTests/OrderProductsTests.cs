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
    public class OrderProductsTests
    {
        private readonly Mock<IOrderProductsService> _orderProductsServiceMock;
        private readonly OrderProductsController _controller;

        public OrderProductsTests()
        {
            _orderProductsServiceMock = new Mock<IOrderProductsService>();
            _controller = new OrderProductsController(_orderProductsServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<OrderProduct>
            {
                new OrderProduct { Id = 1, Title = "Test 1" },
                new OrderProduct { Id = 2, Title = "Test 2" }
            };
            var pagedResult = new PagedResult<OrderProduct> { Results = data };
            _orderProductsServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = (OrderProductsIndexModel)result.Model;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, model.Data);
        }
    }
}
