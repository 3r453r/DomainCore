using DomainObjects.Company;
using DomainObjects.Customer;
using DomainObjects.Person;
using System;

namespace DomainInterface
{
    public interface IDomainInterface
    {
        string CreateCustomer(IPersonData personData);
        string CreateCustomer(ICompanyData personData);
        ICustomerData GetCustomerData(string id);

        IPersonData GetCustomerPersonData(string id);

        ICompanyData GetCustomerCorporateData(string id);
    }
}
