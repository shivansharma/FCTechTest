using Fc.Kata.Checkout.Interfaces;
using Fc.Kata.Checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fc.Kata.Checkout
{
    public class CheckoutService : ICheckoutService
    {

        private readonly List<Item> TotalItemsInShop;

        private List<Item> TotalItemsInBasket = new List<Item>();
        public CheckoutService(List<Item> TotalItemsInShop)
        {
            this.TotalItemsInShop = TotalItemsInShop;
        }

        public void Scan(Item item)
        {
            if (item == null)
                throw new Exception("Item can't be null");

           
            if(this.TotalItemsInShop.Any())
            {
                Item selectedItem = this.TotalItemsInShop.FirstOrDefault(x => x.Sku.Equals(item.Sku));

                if (selectedItem == null)
                    throw new Exception("Item Not Found in the shop");

                if (!string.IsNullOrEmpty(selectedItem.Sku) && selectedItem.UnitPrice <= 0)
                    throw new ArgumentOutOfRangeException("Item Price can't be zero or negative");


                if (this.TotalItemsInBasket.Contains(selectedItem))
                {
                    this.TotalItemsInBasket.Find(x => x.Sku.Equals(selectedItem.Sku)).UnitPrice += selectedItem.UnitPrice;
                }
                else
                {
                    this.TotalItemsInBasket.Add(selectedItem);
                }
            }
        }

        public List<Item> GetAllItemInBasket()
        {
            return this.TotalItemsInBasket;
        }

        public decimal Total()
        {
            throw new NotImplementedException();
        }
    }
}
