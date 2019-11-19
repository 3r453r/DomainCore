using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CustomerModule;
using DomainObjects.Company;
using DomainObjects.Customer;
using DomainObjects.Person;
using DomainObjects.Repository;
using Microsoft.EntityFrameworkCore;
using TestPersistence.Entities;

namespace TestPersistence.CustomerModulePersistence.Repositories
{
    internal class CorporateCustomerRepository : ICorporateCustomerRepository
    {
        public CorporateCustomerRepository(DbSet<Customer> customers)
        {
            Customers = customers;
        }
        private Dictionary<Customer, ICustomer> AddedCustomersMap { get; } = new Dictionary<Customer, ICustomer>();
        private DbSet<Customer> Customers { get; }

        public void ChangesFinalized()
        {
            foreach (var entry in AddedCustomersMap)
            {
                entry.Value.Id = entry.Key.Id.ToString();
            }
        }

        public void Add(ICorporateCustomer entity)
        {
            var customer = new Customer { Company = new Company(entity as ICompanyData) };

            AddedCustomersMap.Add(customer, entity);
            Customers.Add(customer);
        }

        public ICorporateCustomer Get(string id)
        {
            var customer = Customers.Find(long.Parse(id));

            return new CorporateCustomer
            {
                Name = customer.Company.Name,
                Regon = customer.Company.Regon
            };
        }

        public void Remove(ICorporateCustomer entity)
        {
            Customers.Remove(Customers.Find(long.Parse(entity.Id)));
        }
    }
}