using Fc.Kata.Checkout.Interfaces;
using Fc.Kata.Checkout.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fc.Kata.Checkout.Test
{
    public class CheckoutServiceTest
    {

        private ICheckoutService checkoutService;

        private Dictionary<string, decimal> listOfItemsInShop = new Dictionary<string, decimal>
        {
            { "A99", 0.50m},
            { "B15", 0.30m},
            { "C40", 0.60m}
        };

        private List<ItemOffer> listOfOffer = new List<ItemOffer>
        {
           new ItemOffer
           {
               Sku = "A99",
               Quantity = 3,
               OfferPrice = 1.30m
           },
            new ItemOffer
           {
               Sku = "B15",
               Quantity = 2,
               OfferPrice = 0.45m
           }
        };

        [Fact]
        public void Scan_ItemNullTest()
        {
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);
            Assert.Throws<Exception>(() => checkoutService.Scan(null));
        }

       
        [Fact]
        public void Scan_ItemNotInShopTest()
        {
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item = new Item
            {
                Sku = "A43",
            };

            var exception = Assert.Throws<Exception>(() => checkoutService.Scan(item));
            Assert.Equal("Item Not Found in the shop", exception.Message);
        }

        [Fact]
        public void Scan_ItemInShopAndAddedToBasketTest()
        {
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item = new Item
            {
                Sku = "A99",
            };

            this.checkoutService.Scan(item);



            var sum = this.checkoutService.Total();

            Assert.Equal(0.5m, sum);
        }

        [Fact]
        public void Scan_Same_ItemInShopAndAddedToBasketTest()
        {
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item = new Item
            {
                Sku = "A99",
            };

            var item1 = new Item
            {
                Sku = "A99",
            };


            this.checkoutService.Scan(item);

            this.checkoutService.Scan(item1);

            var sum = this.checkoutService.Total();

            Assert.Equal(1, sum);
        }


        [Fact]
        public void Total_ItemInShopAndAddedToBasketTest()
        {
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item = new Item
            {
                Sku = "A99",
            };

            this.checkoutService.Scan(item);

            var total = this.checkoutService.Total();

             Assert.Equal(0.5m, total);
           
        }



        [Fact]
        public void Total_Item1And2InShopAndAddedToBasketTest()
        {
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item = new Item
            {
                Sku = "A99",
            };

            var item1 = new Item
            {
                Sku = "C40",
            };

            this.checkoutService.Scan(item);
            this.checkoutService.Scan(item1);

            var total = this.checkoutService.Total();

            Assert.Equal(1.1m, total);

        }

        [Fact]
        public void Total_Item1And2And3InShopAndAddedToBasketTest()
        {
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item1 = new Item
            {
                Sku = "A99",
            };

            var item2 = new Item
            {
                Sku = "C40",
            };

            var item3 = new Item
            {
                Sku = "B15",
            };

            this.checkoutService.Scan(item1);
            this.checkoutService.Scan(item2);
            this.checkoutService.Scan(item3);

            var total = this.checkoutService.Total();

            Assert.Equal(1.4m, total);

        }

        [Fact]
        public void Total_Item1And2And3SAmeInShopAndAddedToBasketTest()
        {
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item1 = new Item
            {
                Sku = "A99",
            };

            var item2 = new Item
            {
                Sku = "A99",
            };

            var item3 = new Item
            {
                Sku = "A99",
            };

            this.checkoutService.Scan(item1);
            this.checkoutService.Scan(item2);
            this.checkoutService.Scan(item3);

            var total = this.checkoutService.Total();

            Assert.Equal(1.3m, total);

        }

        [Fact]
        public void Total_Item1And2And3SAmeInShopAndAddedToBasketTest_1()
        {
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item1 = new Item
            {
                Sku = "A99",
            };

            var item2 = new Item
            {
                Sku = "A99",
            };

            var item3 = new Item
            {
                Sku = "B15",
            };

            this.checkoutService.Scan(item1);
            this.checkoutService.Scan(item2);
            this.checkoutService.Scan(item3);

            var total = this.checkoutService.Total();

            Assert.Equal(1.3m, total);

        }

        [Fact]
        public void Total_Item1And2And3SAmeInShopAndAddedToBasketTest_2()
        {
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item1 = new Item
            {
                Sku = "A99",
            };

            var item2 = new Item
            {
                Sku = "A99",
            };

            var item3 = new Item
            {
                Sku = "B15",
            };

            var item4 = new Item
            {
                Sku = "B15",
            };

            this.checkoutService.Scan(item1);
            this.checkoutService.Scan(item2);
            this.checkoutService.Scan(item3);
            this.checkoutService.Scan(item4);

            var total = this.checkoutService.Total();

            Assert.Equal(1.45m, total);

        }
    }
}
