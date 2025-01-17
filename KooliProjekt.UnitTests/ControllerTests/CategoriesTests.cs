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
    public class CategoriesTests
    {
        private readonly Mock<ICategoriesService> _categoryServiceMock;
        private readonly CategoriesController _controller;

        public CategoriesTests()
        {
            _categoryServiceMock = new Mock<ICategoriesService>();
            _controller = new CategoriesController(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Category>
            {
                new Category { Id = 1, Title = "Test 1" },
                new Category { Id = 2, Title = "Test 2" }
            };
            var pagedResult = new PagedResult<Category> { Results = data };
            _categoryServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = (CategoriesIndexModel)result.Model;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, model.Data);
        }
    }
}
