using CustomerModule;
using CustomerModule.Persistence.Repositories;
using DomainObjects.Customer;
using DomainObjects.Person;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestPersistence.Entities;

namespace TestPersistence.CustomerModulePersistence.Repositories
{
    public class IndividualCustomerRepository : IIndividualCustomerRepository
    {

        public IndividualCustomerRepository(DbSet<Customer> customers)
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

        public void Add(IIndividualCustomer entity)
        {
            var customer = new Customer { Person = new Entities.Person(entity as IPersonData) };

            AddedCustomersMap.Add(customer, entity);
            Customers.Add(customer);
        }

        public IIndividualCustomer Get(string id)
        {
            var customer = Customers.Find(long.Parse(id));
            var individualCustomer = new IndividualCustomer(new CustomerModule.Person
            {
                FirstName = customer.Person.FirstName,
                LastName = customer.Person.LastName,
                Pesel = customer.Person.Pesel,
                OtherNames = (customer.Person as IPersonData).OtherNames,
                BirthDate = customer.Person.BirthDate,
                Id = customer.Id.ToString()
            });

            return individualCustomer;
        }

        public void Remove(IIndividualCustomer entity)
        {
            Customers.Remove(Customers.Find(long.Parse(entity.Id)));
        }
    }
}
