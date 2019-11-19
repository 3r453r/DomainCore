using DomainObjects.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestPersistence.Entities
{
    public class Company : ICompanyData
    {
        public Company(ICompanyData data)
        {
            Name = data.Name;
            Regon = data.Regon;
        }

        public Company()
        { }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Regon { get; set; }
    }
}
