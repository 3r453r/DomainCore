using DomainObjects.Company;
using DomainObjects.Customer;
using DomainObjects.Person;
using DomainObjects.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainCore
{
    public interface ICustomerUnitOfWork : IUnitOfWork
    {
        IIndividualCustomer CreateCustomer(IPersonData personData);
        ICorporateCustomer CreateCustomer(ICompanyData companyData);
        ICustomer GetCustomer(string id);
        IIndividualCustomer GetIndividualCustomer(string id);
        ICorporateCustomer GetCorporateCustomer(string id);
    }
}
