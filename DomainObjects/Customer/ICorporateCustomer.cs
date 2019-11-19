using DomainObjects.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainObjects.Customer
{
    public interface ICorporateCustomer : ICustomer, ICompany
    {
    }
}
