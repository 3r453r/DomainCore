using DomainCore;
using DomainObjects.Company;
using DomainObjects.Customer;
using DomainObjects.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule
{
    public class CustomerUnitOfWork : ICustomerUnitOfWork
    {
        public CustomerUnitOfWork(ICustomerPersistence persistence, int transactionId)
        {
            UnitOfWork = persistence.GetUnitOfWork(transactionId);
            TransactionId = transactionId;
        }

        public event Action<int> UnitOfWorkDisposing;

        private ICustomerWork UnitOfWork { get; }
        public int TransactionId { get; }

        public void FinalizeChanges()
        {
            UnitOfWork.FinalizeChanges();
            UnitOfWorkDisposing(TransactionId);
        }

        public void StageChanges()
        {
            UnitOfWork.StageChanges();
        }

        public void UnstageChanges()
        {
            UnitOfWork.UnstageChanges();
            UnitOfWorkDisposing(TransactionId);
        }

        public IIndividualCustomer CreateCustomer(IPersonData personData)
        {
            var individualCustomer = new IndividualCustomer(new Person(personData));
            UnitOfWork.IndividualCustomers.Add(individualCustomer);
            return individualCustomer;
        }

        public ICorporateCustomer CreateCustomer(ICompanyData companyData)
        {
            var corpoCustomer = new CorporateCustomer(companyData);
            UnitOfWork.CorporateCustomers.Add(corpoCustomer);
            return corpoCustomer;
        }
        
        public ICorporateCustomer GetCorporateCustomer(string id)
        {
            var customer = UnitOfWork.CorporateCustomers.Get(id);
            return customer.CustomerType == CustomerType.Corporate ? 
                customer as ICorporateCustomer : null;
        }

        public ICustomer GetCustomer(string id)
        {
            var individualCustomer = UnitOfWork.IndividualCustomers.Get(id);
            return individualCustomer is object ? (ICustomer)individualCustomer :
                UnitOfWork.CorporateCustomers.Get(id);
        }

        public IIndividualCustomer GetIndividualCustomer(string id)
        {
            var customer = UnitOfWork.IndividualCustomers.Get(id);
            return customer.CustomerType == CustomerType.Individual ?
                customer as IIndividualCustomer : null;
        }
    }
}
