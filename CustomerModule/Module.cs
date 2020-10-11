using Domain.Entities;
using Domain.Modules;
using System;

namespace CustomerModule
{
    public class Module : ICustomerModule
    {
        private ICustomerUnitOfWork _customerUnitOfWork;
        public ICustomerUnitOfWork CustomerUnitOfWork { set { _customerUnitOfWork = value; } }

        public Module(ICustomerUnitOfWork customerUnitOfWork)
        {
            _customerUnitOfWork = customerUnitOfWork;
        }
        public Customer CreateCustomer(CustomerInfo customer)
        {
            return _customerUnitOfWork.CustomerRepository.New(customer);
        }

        public Customer GetCustomer(long customerId)
        {
            return _customerUnitOfWork.CustomerRepository.Get(customerId);
        }

        public void RejectChanges()
        {
            _customerUnitOfWork.RejectChanges();
        }

        public void SaveChanges()
        {
            _customerUnitOfWork.SaveChanges();
        }

        public void StageChanges()
        {
            _customerUnitOfWork.StageChanges();
        }
    }
}
