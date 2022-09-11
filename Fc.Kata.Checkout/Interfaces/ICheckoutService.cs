using Fc.Kata.Checkout.Models;
using System.Collections.Generic;

namespace Fc.Kata.Checkout.Interfaces
{
    public interface ICheckoutService
    {
        decimal Total();


        void Scan(Item item);
    }
}
