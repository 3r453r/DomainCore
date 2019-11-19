using DomainObjects.Company;
using DomainObjects.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule
{
    public class CorporateCustomer : ICorporateCustomer
    {
        public CorporateCustomer()
        { }

        public CorporateCustomer(ICompanyData companyData)
        {
            Name = companyData.Name;
            Regon = companyData.Regon;
        }
        public string Name { get; set; }

        public string Regon { get; set; }

        public string Id { get; set; }

        public CustomerType CustomerType => CustomerType.Corporate;
    }
}
