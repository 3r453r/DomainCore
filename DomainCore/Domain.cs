using DomainInterface;
using DomainObjects.Company;
using DomainObjects.Customer;
using DomainObjects.Person;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainCore
{
    public class Domain : IDomain, IDomainInterface
    {
        public static Domain Instance { get; } = new Domain();

        private Domain()
        { }
        static Domain()
        { }

        private ISet<int> TransactionIds { get; } = new HashSet<int>();

        private int GetNewTransactionId()
        {
            int result = 1;
            while (TransactionIds.Contains(result))
                result++;
            return result;
        }

        public int RegisterNewTransactionId()
        {
            int result = GetNewTransactionId();
            TransactionIds.Add(result);
            return result;
        }

        public void UnregisterTransactionId(int transactionId) 
        {
            TransactionIds.Remove(transactionId);
        }

        public string CreateCustomer(IPersonData personData)
        {
            int transactionId = RegisterNewTransactionId();
            var uow = CustomerModule.GetUnitOfWork(transactionId);

            var customer = uow.CreateCustomer(personData);

            uow.FinalizeChanges();
            UnregisterTransactionId(transactionId);

            return (customer as ICustomer).Id;
        }

        public string CreateCustomer(ICompanyData companyData)
        {
            int transactionId = RegisterNewTransactionId();
            var uow = CustomerModule.GetUnitOfWork(transactionId);

            var customer = uow.CreateCustomer(companyData);

            uow.FinalizeChanges();
            UnregisterTransactionId(transactionId);

            return (customer as ICustomer).Id;
        }

        public ICustomerData GetCustomerData(string id)
        {
            int transactionId = RegisterNewTransactionId();
            var uow = CustomerModule.GetUnitOfWork(transactionId);

            var customer = uow.GetCustomer(id);

            uow.FinalizeChanges();
            UnregisterTransactionId(transactionId);

            return customer;
        }

        public IPersonData GetCustomerPersonData(string id)
        {
            int transactionId = RegisterNewTransactionId();
            var uow = CustomerModule.GetUnitOfWork(transactionId);

            var customer = uow.GetIndividualCustomer(id);

            uow.FinalizeChanges();
            UnregisterTransactionId(transactionId);

            return customer;
        }

        public ICompanyData GetCustomerCorporateData(string id)
        {
            int transactionId = RegisterNewTransactionId();
            var uow = CustomerModule.GetUnitOfWork(transactionId);

            var customer = uow.GetCorporateCustomer(id);

            uow.FinalizeChanges();
            UnregisterTransactionId(transactionId);

            return customer;
        }

        public void UpdateCustomerCompanyData(string id, ICompanyData companyData)
        {
            int transactionId = RegisterNewTransactionId();
            var uow = CustomerModule.GetUnitOfWork(transactionId);

            var customer = uow.GetCorporateCustomer(id);
            customer.Name = companyData.Name;
            customer.Regon = companyData.Regon;

            uow.FinalizeChanges();
            UnregisterTransactionId(transactionId);
        }

        public void UpdateCustomerPersonData(string id, IPersonData personData)
        {
            int transactionId = RegisterNewTransactionId();
            var uow = CustomerModule.GetUnitOfWork(transactionId);

            var customer = uow.GetIndividualCustomer(id);
            customer.UpdatePersonData(personData);

            uow.FinalizeChanges();
            UnregisterTransactionId(transactionId);
        }

        public ICustomerModule CustomerModule { get; set; }               
    }
}
