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
    public class OrdersTests
    {
        private readonly Mock<IOrdersService> _orderServiceMock;
        private readonly OrdersController _controller;

        public OrdersTests()
        {
            _orderServiceMock = new Mock<IOrdersService>();
            _controller = new OrdersController(_orderServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Order>
            {
                new Order { Id = 1, Title = "Test 1" },
                new Order { Id = 2, Title = "Test 2" }
            };
            var pagedResult = new PagedResult<Order> { Results = data };
            _orderServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = (OrdersIndexModel)result.Model;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, model.Data);
        }
    }
}
