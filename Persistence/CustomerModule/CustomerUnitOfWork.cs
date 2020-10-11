using CustomerModule;
using CustomerModule.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.CustomerModule
{
    public class CustomerUnitOfWork : ICustomerUnitOfWork
    {
        private CustomerContext context;
        public CustomerUnitOfWork(CustomerContext context)
        {
            this.context = context;
            _customerRepository = new CustomerRepository(context);
        }
        public ICustomerRepository CustomerRepository { get => _customerRepository; set { _customerRepository = (CustomerRepository)value; } }
        private CustomerRepository _customerRepository;

        public void RejectChanges()
        {            
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void StageChanges()
        {
            context.SaveChanges();
        }
    }
}
