using Fc.Kata.Checkout.Interfaces;
using Xunit;

namespace Fc.Kata.Checkout.Test
{
    public class CheckoutServiceTest
    {

        private readonly ICheckoutService checkoutService;

        public CheckoutServiceTest()
        {
            this.checkoutService = new CheckoutService();
        }

        [Fact]
        public void Assert_MethodNotImplemented()
        {
           var a  = checkoutService.Total();
            Assert.Equal(10, 10);
        }
    }
}
