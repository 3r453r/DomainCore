using DomainObjects.Customer;
using DomainObjects.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule
{
    public interface ICorporateCustomerRepository : IRepository<ICorporateCustomer>
    {
    }
}
