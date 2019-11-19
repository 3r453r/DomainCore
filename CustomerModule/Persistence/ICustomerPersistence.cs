using DomainObjects.Customer;
using DomainObjects.Person;
using DomainObjects.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule
{
    public interface ICustomerPersistence
    {
        ICustomerWork GetUnitOfWork(int transactionId);
    }
}
