using DomainObjects.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestPersistence.Entities
{
    public class Customer : ICustomerData
    {
        string ICustomerData.Id
        {
            get
            {
                return Id.ToString();
            }

            set
            {
                Id = long.Parse(value);
            }
        }
        public long Id { get; set; }
        public CustomerType CustomerType => PersonId == 0 ? CustomerType.Corporate : CustomerType.Individual;
        public long? PersonId { get; set; }
        public long? CompanyId { get; set; }

        public Person Person { get; set; }
        public Company Company { get; set; }
    }
}
