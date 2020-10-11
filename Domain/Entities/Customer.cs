using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public interface ICustomer
    {
        long Id { get; }
        string Name { get; }
    }

    public class Customer : ICustomer
    {
        public long Id {get;set;}

        public string Name { get; set; }
    }

    public class CustomerInfo
    {
        public string Name { get; set; }
    }
}
