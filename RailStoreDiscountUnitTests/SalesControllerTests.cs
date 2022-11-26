using RailStoreDiscount.Controllers;
using RailStoreDiscount.Domain;
using System;
using Xunit;

namespace RailStoreDiscountUnitTests
{
    public class SalesControllerTests
    {
        [Fact]
        public void GetDiscountedAmount_ForGroceryContent_ReturnsNoDiscount()
        {
            // Arrange
            var controller = new SalesController();
            var today = DateTime.Today;
            var invoice = new Invoice {
                Amount = 95,
                GroceryContent = true
            };
            var customer = new Customer {
                Title = "Test Customer",
                IsEmployee = false,
                IsAffiliate = false,
                DateCreated = today
            };

            // Act
            var discountedAmount = controller.GetDiscountedAmount(customer, invoice, today);
                
            // Assert
            Assert.Equal(discountedAmount, invoice.Amount);
        }

        [Fact]
        public void GetDiscountedAmount_TimesOfHundred_ReturnsNoPercentageDiscount()
        {
            // Arrange
            var controller = new SalesController();
            var today = DateTime.Today;
            var amount = 100.0;
            var expectedAmount = 95.0;
            var invoice = new Invoice
            {
                Amount = amount,
                GroceryContent = true
            };
            var customer = new Customer
            {
                Title = "Test Customer",
                IsEmployee = false,
                IsAffiliate = false,
                DateCreated = today
            };

            // Act
            var discountedAmount = controller.GetDiscountedAmount(customer, invoice, today);
            
            // Assert
            Assert.Equal(discountedAmount, expectedAmount);
        }

        [Fact]
        public void GetDiscountedAmount_EmployeeDiscount_Returns30Per100Discounted()
        {
            // Arrange
            var controller = new SalesController();
            var today = DateTime.Today;
            var amount = 90.0;
            var expectedAmount = amount * 70 / 100;
            var invoice = new Invoice
            {
                Amount = amount,
                GroceryContent = false
            };
            var customer = new Customer
            {
                Title = "Test Customer",
                IsEmployee = true,
                IsAffiliate = false,
                DateCreated = today
            };

            // Act
            var discountedAmount = controller.GetDiscountedAmount(customer, invoice, today);
            
            // Assert
            Assert.Equal(discountedAmount, expectedAmount);
        }

        [Fact]
        public void GetDiscountedAmount_AffiliateDiscount_Returns10Per100Discounted()
        {
            // Arrange
            var controller = new SalesController();
            var today = DateTime.Today;
            var amount = 90.0;
            var expectedAmount = amount * 90 / 100;
            var invoice = new Invoice
            {
                Amount = amount,
                GroceryContent = false
            };
            var customer = new Customer
            {
                Title = "Test Customer",
                IsEmployee = false,
                IsAffiliate = true,
                DateCreated = today
            };

            // Act
            var discountedAmount = controller.GetDiscountedAmount(customer, invoice, today);
            
            // Assert
            Assert.Equal(discountedAmount, expectedAmount);
        }

        [Fact]
        public void GetDiscountedAmount_Over2Years_Returns5Per100Discounted()
        {
            // Arrange
            var controller = new SalesController();
            var today = DateTime.Today;
            var amount = 90.0;
            var expectedAmount = amount * 95 / 100;
            var invoice = new Invoice
            {
                Amount = amount,
                GroceryContent = false
            };
            var customer = new Customer
            {
                Title = "Test Customer",
                IsEmployee = false,
                IsAffiliate = false,
                DateCreated = today.AddYears(-2).AddDays(-1)
            };

            // Act
            var discountedAmount = controller.GetDiscountedAmount(customer, invoice, today);
            
            // Assert
            Assert.Equal(discountedAmount, expectedAmount);
        }
    }
}
