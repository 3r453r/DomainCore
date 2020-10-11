using CustomerModule.Repositories;
using DomainCustomer = Domain.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using Persistence.CustomerModule.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.CustomerModule
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerContext context;
        public CustomerRepository(CustomerContext context)
        {
            this.context = context;
        }

        public IEnumerable<DomainCustomer> CustomersByName(string name)
        {
            return context.Customers.Where(c => c.Name == name);
        }

        public void Delete(DomainCustomer customer)
        {
            context.Customers.Remove(context.Customers.Find(customer.Id));
        }

        public DomainCustomer Get(long id)
        {
            return context.Customers.Find(id);
        }

        public DomainCustomer New(Domain.Entities.CustomerInfo customerInfo)
        {
            var newCustomer = new Customer { Name = customerInfo.Name };
            context.Customers.Add(newCustomer);
            return newCustomer;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void RejectChanges()
        {
        }

        public void StageChanges()
        {
        }
    }
}
