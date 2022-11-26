using System;

namespace RailStoreDiscount.Domain
{
    public class Customer
    {
        public string Title { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsAffiliate { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
