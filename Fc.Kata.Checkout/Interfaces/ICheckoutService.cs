using Fc.Kata.Checkout.Models;

namespace Fc.Kata.Checkout.Interfaces
{
    public interface ICheckoutService
    {
        decimal Total();

        void Scan(Item item);
    }
}
