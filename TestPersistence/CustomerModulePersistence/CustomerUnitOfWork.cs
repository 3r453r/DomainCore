using CustomerModule;
using CustomerModule.Persistence.Repositories;
using DomainObjects.Customer;
using DomainObjects.Person;
using DomainObjects.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestPersistence;
using TestPersistence.CustomerModulePersistence.Repositories;
using TestPersistence.Entities;

namespace TestPersistence.CustomerModulePersistence
{
    public class CustomerUnitOfWork : ICustomerWork
    {
        public CustomerUnitOfWork(TestContext context, int transactionId)
        {
            TestContext = context;
            TransactionId = transactionId;

            IndividualCustomerRepository = new IndividualCustomerRepository(context.Customers);
            ChangesFinalized += IndividualCustomerRepository.ChangesFinalized;
            ChangesFinalizing += IndividualCustomerRepository.ChangesFinalizing;

            CorporateCustomerRepository = new CorporateCustomerRepository(context.Customers);
            ChangesFinalized += CorporateCustomerRepository.ChangesFinalized;
        }

        private TestContext TestContext { get; }

        private IndividualCustomerRepository IndividualCustomerRepository { get; }
        public ICorporateCustomerRepository CorporateCustomers => CorporateCustomerRepository;

        private CorporateCustomerRepository CorporateCustomerRepository { get; }
        public IIndividualCustomerRepository IndividualCustomers => IndividualCustomerRepository;

        public int TransactionId { get; set; }

        public event Action ChangesFinalized;

        public event Action ChangesFinalizing;
        public void FinalizeChanges()
        {
            ChangesFinalizing();
            TestContext.SaveChanges();
            ChangesFinalized();
        }

        public event Action ChangesStaged;
        public void StageChanges()
        {
            ChangesStaged();
        }

        public event Action ChangesUnstaged;
        public void UnstageChanges()
        {
            ChangesUnstaged();
        }
    }
}
