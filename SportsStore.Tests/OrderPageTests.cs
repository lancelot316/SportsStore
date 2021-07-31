using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using SportsStore.Web.Models.Domain;
using SportsStore.Web.Pages.Orders;
using Xunit;

namespace SportsStore.Tests
{
    public class OrderPageTests
    {
        
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            CheckoutModel target = new CheckoutModel(mock.Object, cart);

            // Act
            var result = target.OnPost(new Order());

            // Assert
            Assert.IsType<PageResult>(result);
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange 
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CheckoutModel target = new CheckoutModel(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");

            // Act
            var result = target.OnPost(new Order());

            // Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CheckoutModel target = new CheckoutModel(mock.Object, cart);

            // Act
            var result = target.OnPost(new Order());

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/Orders/Completed", (result as RedirectToPageResult).PageName);
        }
    }
}
