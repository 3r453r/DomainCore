using CustomerModule.Persistence.Repositories;
using DomainObjects.Customer;
using DomainObjects.Person;
using DomainObjects.Repository;
using DomainObjects.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule
{
    public interface ICustomerWork : IUnitOfWork
    {
        ICorporateCustomerRepository CorporateCustomers { get; }
        IIndividualCustomerRepository IndividualCustomers { get; }
    }
}
