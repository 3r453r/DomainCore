using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Modules
{
    public interface ICustomerModule : IModule
    {
        Customer GetCustomer(long customerId);
        Customer CreateCustomer(CustomerInfo customer);
    }
}
