using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RailStoreDiscount.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailStoreDiscount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {        
        public SalesController()
        {
        }

        [HttpGet]
        public double GetDiscountedAmount(Customer customer, Invoice invoice, DateTime salesDate)
        {
            double discount = 0.0;

            if (!invoice.GroceryContent)
            {
                if (customer.IsEmployee)
                {
                    discount += invoice.Amount * 30 / 100;
                }
                else if (customer.IsAffiliate)
                {
                    discount += invoice.Amount * 10 / 100;
                }
                else
                {
                    if ((salesDate - customer.DateCreated).Days / 365 >= 2.0)
                    {
                        discount += invoice.Amount * 5 / 100;
                    }
                }
            }

            if (invoice.Amount >= 100)
            {
                int timesOfHundred = ((int)invoice.Amount) / 100;
                discount += timesOfHundred * 5.0;
            }

            return invoice.Amount - discount;
        }
    }
}
