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
        public void Test_Scan_ItemNullTest()
        {
            // Arrange
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            // Act & Assert
            Assert.Throws<Exception>(() => checkoutService.Scan(null));
        }

       
        [Fact]
        public void Test_Scan_ItemNotInShopTest()
        {
            // Arrange
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item = new Item
            {
                Sku = "A43",
            };

            // Act
            var exception = Assert.Throws<Exception>(() => checkoutService.Scan(item));

            // Assert
            Assert.Equal("Item Not Found in the shop", exception.Message);
        }

        [Fact]
        public void Test_Scan_ItemInShopAndAddedToBasketTest()
        {
            // Arrange
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item = new Item
            {
                Sku = "A99",
            };

            // Act
            this.checkoutService.Scan(item);
            var sum = this.checkoutService.Total();

            // Assert
            Assert.Equal(0.5m, sum);
        }

        [Fact]
        public void Test_Scan_Same_ItemInShopAndAddedToBasketTest()
        {
            // Arrange
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item = new Item
            {
                Sku = "A99",
            };

            var item1 = new Item
            {
                Sku = "A99",
            };

            // Act
            this.checkoutService.Scan(item);
            this.checkoutService.Scan(item1);
            var sum = this.checkoutService.Total();

            // ASsert
            Assert.Equal(1, sum);
        }

        [Fact]
        public void Test_Total_ItemInShopAndAddedToBasketTest()
        {
            // Arrange
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item = new Item
            {
                Sku = "A99",
            };

            // Act
            this.checkoutService.Scan(item);
            var total = this.checkoutService.Total();

            // Assert
            Assert.Equal(0.5m, total);
        }

        [Fact]
        public void Test_Total_AddTwoDifferentItemInBasketTest()
        {
            // Arrange
            this.checkoutService = new CheckoutService(listOfItemsInShop, listOfOffer);

            var item = new Item
            {
                Sku = "A99",
            };

            var item1 = new Item
            {
                Sku = "C40",
            };

            // Act
            this.checkoutService.Scan(item);
            this.checkoutService.Scan(item1);
            var total = this.checkoutService.Total();

            // Assert
            Assert.Equal(1.1m, total);
        }

        [Fact]
        public void Test_Total_AddAllDifferentItemInBasketTest()
        {
            // Arrange
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

            // Act
            this.checkoutService.Scan(item1);
            this.checkoutService.Scan(item2);
            this.checkoutService.Scan(item3);

            var total = this.checkoutService.Total();

            // Assert
            Assert.Equal(1.4m, total);

        }

        [Fact]
        public void Test_Total_SameITem_WithOfferInBasketTest()
        {
            // Arrange
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

            // Act
            this.checkoutService.Scan(item1);
            this.checkoutService.Scan(item2);
            this.checkoutService.Scan(item3);

            var total = this.checkoutService.Total();

            // Assert
            Assert.Equal(1.3m, total);

        }

        [Fact]
        public void Test_DifferentItemWithoutDiscountBasketTest()
        {
            // Arrange
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

            // Act
            this.checkoutService.Scan(item1);
            this.checkoutService.Scan(item2);
            this.checkoutService.Scan(item3);
            var total = this.checkoutService.Total();

            // Assert
            Assert.Equal(1.3m, total);
        }

        [Fact]
        public void Test_Total_SecondItemWithOFferAddedInBasket()
        {
            // Arrange
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

            // Act
            this.checkoutService.Scan(item1);
            this.checkoutService.Scan(item2);
            this.checkoutService.Scan(item3);
            this.checkoutService.Scan(item4);

            var total = this.checkoutService.Total();

            // ASsert
            Assert.Equal(1.45m, total);

        }

        [Fact]
        public void Test_Total_ItemWithNullOFferAddedInBasket()
        {
            // Arrange
            this.checkoutService = new CheckoutService(listOfItemsInShop, null);

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

            // Act
            this.checkoutService.Scan(item1);
            this.checkoutService.Scan(item2);
            this.checkoutService.Scan(item3);
            this.checkoutService.Scan(item4);

            var total = this.checkoutService.Total();

            // ASsert
            Assert.Equal(1.60m, total);

        }
    }
}
