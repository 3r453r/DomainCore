using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule.Repositories
{
    public interface ICustomerRepository
    {
        Customer New(CustomerInfo customerInfo);
        void Delete(Customer customer);
        Customer Get(long id);

        IEnumerable<Customer> CustomersByName(string name);
    }
}
