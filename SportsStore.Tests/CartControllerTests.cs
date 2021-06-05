using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Web.Controllers;
using SportsStore.Web.Models.Domain;
using SportsStore.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Xunit;

namespace SportsStore.Tests
{
    public class CartControllerTests
    {
        [Fact]
        public void Can_Add_To_Cart()
        {

            // Arrange
            // - create a mock repository
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(m => m.Products).Returns((new Product[] {
                p1, p2
            }).AsQueryable<Product>());
            
            // - create a cart
            Cart testCart = new Cart();
            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);

            // - create a mock page context and session
            byte[] data =
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));

            Mock<ISession> mockSession = new Mock<ISession>();
            mockSession.Setup(c => c.TryGetValue(It.IsAny<string>(), out data));
            
            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(c => c.Session).Returns(mockSession.Object); ;

            // Arrange - create the controller
            CartController target = new CartController(mockRepo.Object, null);
            target.ControllerContext.HttpContext = mockContext.Object;

            // Act - add a product to the cart
            target.AddToCart(1, null);

            // Assert
            Assert.Equal(2, testCart.Lines.Count);
            Assert.Equal(1, testCart.Lines.ToArray()[0].Product.ProductID);
        }

        [Fact]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {

            // Arrange - create the mock repository
            Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
            }.AsQueryable());

            // Arrange - create a Cart
            Cart testCart = new Cart();

            // - create a mock page context and session
            byte[] data =
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));

            Mock<ISession> mockSession = new Mock<ISession>();
            mockSession.Setup(c => c.TryGetValue(It.IsAny<string>(), out data));

            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(c => c.Session).Returns(mockSession.Object); ;

            // Arrange - create the controller
            CartController target = new CartController(mockRepo.Object, null);
            target.ControllerContext.HttpContext = mockContext.Object;

            // Act - add a product to the cart
            RedirectToActionResult result = target.AddToCart(2, "myUrl");

            // Assert
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("myUrl", result.RouteValues["returnUrl"]);

        }

        [Fact]
        public void Can_View_Cart_Contents()
        {
            // Arrange - create a Cart
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Cart testCart = new Cart();

            testCart.AddItem(p1, 1);

            // - create a mock page context and session
            byte[] data =
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));

            Mock<ISession> mockSession = new Mock<ISession>();
            mockSession.Setup(c => c.TryGetValue(It.IsAny<string>(), out data));

            Mock<HttpContext> mockContext = new Mock<HttpContext>();
            mockContext.SetupGet(c => c.Session).Returns(mockSession.Object); ;

            // Arrange - create the controller
            CartController target = new CartController(null, null);
            target.ControllerContext.HttpContext = mockContext.Object;

            // Act - call the Index action method
            CartIndexViewModel result
                = (CartIndexViewModel)target.Index("myUrl").ViewData.Model;

            // Assert
            Assert.Single(result.Cart.Lines);
            Assert.Equal(result.Cart.Lines.First<CartLine>().Product.ProductID, p1.ProductID);
            Assert.Equal("myUrl", result.ReturnUrl);
        }
    }
}
