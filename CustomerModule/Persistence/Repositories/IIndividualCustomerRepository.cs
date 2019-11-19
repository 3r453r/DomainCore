using DomainObjects.Customer;
using DomainObjects.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule.Persistence.Repositories
{
    public interface IIndividualCustomerRepository : IRepository<IIndividualCustomer>
    {
    }
}
