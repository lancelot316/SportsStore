﻿using Moq;
using SportsStore.Web.Models.Domain;
using SportsStore.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Tests
{ 
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            });

            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object);

            // Action
            Product[] result = ((IEnumerable<Product>)target.Index().
                ViewData.Model).ToArray();

            // Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        [Fact]
        public void Can_Edit_Product()
        {

            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            });

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);

            // Act
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            // Assert
            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
        {

            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            });

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);

            // Act
            Product result = (Product)target.Edit(4).ViewData.Model;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a product
            Product product = new Product { Name = "Test" };

            // Act - try to save the product
            ActionResult result = target.Edit(product);

            // Assert - check that the repository was called
            mock.Verify(m => m.SaveProduct(product));

            // Assert - check the method result type
            Assert.IsNotType<ViewResult>(result);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {

            // Arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Arrange - create a product
            Product product = new Product { Name = "Test" };
            // Arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // Act - try to save the product
            ActionResult result = target.Edit(product);

            // Assert - check that the repository was not called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            // Assert - check the method result type
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Valid_Products()
        {

            // Arrange - create a Product
            Product prod = new Product { ProductID = 2, Name = "Test" };

            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                prod,
                new Product {ProductID = 3, Name = "P3"},
            });

            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);

            // Act - delete the product
            target.Delete(prod.ProductID);

            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mock.Verify(m => m.DeleteProduct(prod.ProductID));
        }
    }
}
