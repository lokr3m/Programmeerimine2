﻿using System;
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
        public async Task Index_should_return_view_and_data()
        {
            // Arrange
            var page = 1;
            var data = new List<Product>
            {
                new Product { Id = 1, Title = "Test 1" },
                new Product { Id = 2, Title = "Test 2" }
            };
            var pagedResult = new PagedResult<Product>
            {
                Results = data,
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 5,
                RowCount = 2
            };
            _productServiceMock
                .Setup(x => x.List(page, It.IsAny<int>(), null))
                .ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = (ProductsIndexModel)result.Model;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Index"
            );
            Assert.Equal(pagedResult, model.Data);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_null()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_item_was_not_found()
        {
            // Arrange
            int? id = 1;
            var todoList = (Product)null;
            _productServiceMock
                .Setup(x => x.Get(id.Value))
                .ReturnsAsync(todoList);

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_correct_view_with_model_when_item_was_found()
        {
            // Arrange
            int? id = 1;
            var todoList = new Product { Id = id.Value, Title = "Test 1" };
            _productServiceMock
                .Setup(x => x.Get(id.Value))
                .ReturnsAsync(todoList);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Details"
            );
            Assert.Equal(todoList, result.Model);
        }

        [Fact]
        public void Create_should_return_correct_view()
        {
            // Arrange

            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Create"
            );
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_null()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_item_was_not_found()
        {
            // Arrange
            int? id = 1;
            var todoList = (Product)null;
            _productServiceMock
                .Setup(x => x.Get(id.Value))
                .ReturnsAsync(todoList);

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_correct_view_with_model_when_item_was_found()
        {
            // Arrange
            int? id = 1;
            var todoList = new Product { Id = id.Value, Title = "Test 1" };
            _productServiceMock
                .Setup(x => x.Get(id.Value))
                .ReturnsAsync(todoList);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Delete"
            );
            Assert.Equal(todoList, result.Model);
        }
    }
}
