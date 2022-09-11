using Fc.Kata.Checkout.Interfaces;
using Fc.Kata.Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fc.Kata.Checkout
{
    public class CheckoutService : ICheckoutService
    {

        private Dictionary<string, decimal> itemPrices;

        private Dictionary<string, int> itemQuantities = new Dictionary<string, int>();

        public CheckoutService(Dictionary<string, decimal> itemPrices)
        {
            this.itemPrices = itemPrices;
        }

        public void Scan(Item item)
        {
            if (item == null)
                throw new Exception("Item can't be null");

            int quantity;

            if(itemPrices.ContainsKey(item.Sku))
            {
                if (itemQuantities.TryGetValue(item.Sku, out quantity))
                {
                    itemQuantities[item.Sku] = ++quantity;
                }
                else
                {
                    itemQuantities.Add(item.Sku, quantity = 1);
                }
            }
            else
            {
                throw new Exception("Item Not Found in the shop");
            }
        }

        public decimal Total()
        {
              return itemQuantities.Sum(item => item.Value * itemPrices[item.Key]);
        }


    }
}
