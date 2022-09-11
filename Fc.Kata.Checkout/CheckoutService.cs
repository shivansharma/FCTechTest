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

        private List<ItemOffer> itemOffers;

        private Dictionary<string, int> itemQuantities = new Dictionary<string, int>();

        public CheckoutService(Dictionary<string, decimal> itemPrices, List<ItemOffer> itemOffers)
        {
            this.itemPrices = itemPrices;
            this.itemOffers = itemOffers;
        }

        public void Scan(Item item)
        {
            if (item == null)
                throw new Exception("Item can't be null");

            int quantity;

            if(itemPrices != null && itemPrices.ContainsKey(item.Sku))
            {
                if (itemQuantities.TryGetValue(item.Sku, out quantity))
                {
                    itemQuantities[item.Sku] = ++quantity;
                }
                else
                {
                    itemQuantities.Add(item.Sku, 1);
                }
            }
            else
            {
                throw new Exception("Item Not Found in the shop");
            }
        }

        public decimal Total()
        {
            decimal sum = 0;
            foreach(var item in itemQuantities)
            {
                sum += CalculateTotalWithOffer(item.Key, item.Value, sum);
            }

            return sum;
        }

        private decimal CalculateTotalWithOffer(string sku, int price, decimal sum)
        {
            if (this.itemOffers != null)
            {
                ItemOffer offerOnItem = this.itemOffers.FirstOrDefault(x => x.Sku.Equals(sku));
                if (offerOnItem != null && offerOnItem.Quantity.Equals(price))
                {
                    sum = offerOnItem.OfferPrice;
                    return sum;
                }
            }

            sum = price * itemPrices[sku];
            return sum;
        }
    }
}
