using System;
using System.Collections.Generic;
using System.Text;

namespace DomainObjects.Customer
{
    public interface ICustomerData
    {
        string Id { get; set; }
        CustomerType CustomerType { get; }
    }

    public enum CustomerType
    { 
        Individual,
        Corporate
    }
}
