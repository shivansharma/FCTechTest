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

        private List<ItemOffer> ItemOffers;

        public CheckoutService(Dictionary<string, decimal> itemPrices, List<ItemOffer> itemOffers)
        {
            this.itemPrices = itemPrices;
            this.ItemOffers = itemOffers;
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
            decimal sum = 0;
            foreach(var item in itemQuantities)
            {
                ItemOffer offerOnItem = this.ItemOffers.FirstOrDefault(x => x.Sku.Equals(item.Key));
                if(offerOnItem != null && offerOnItem.Quantity.Equals(item.Value))
                {
                    sum += offerOnItem.OfferPrice;
                }
                else
                {
                    sum += item.Value * itemPrices[item.Key];
                }
            }

            return sum;
        }


    }
}
