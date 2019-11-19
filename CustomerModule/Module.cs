using DomainCore;
using DomainObjects.Company;
using DomainObjects.Customer;
using DomainObjects.Person;
using System;
using System.Collections.Generic;

namespace CustomerModule
{
    public class Module : ICustomerModule
    {
        public Module(ICustomerPersistence persistence)
        {
            Persistence = persistence;
        }
        private Dictionary<int, CustomerUnitOfWork> ActiveWorkUnits { get; } = new Dictionary<int, CustomerUnitOfWork>();

        public ICustomerPersistence Persistence { private get; set; }

        public ICustomerUnitOfWork GetUnitOfWork(int transactionId)
        {
            if (ActiveWorkUnits.ContainsKey(transactionId))
            {
                return ActiveWorkUnits[transactionId];
            }
            else 
            {
                var cuow = new CustomerUnitOfWork(Persistence, transactionId);
                cuow.UnitOfWorkDisposing += RemoveWorkUnit;
                ActiveWorkUnits.Add(transactionId, cuow);
                return cuow;
            }            
        }

        private void RemoveWorkUnit(int transactionId)
        {
            ActiveWorkUnits.Remove(transactionId);
        }

    }
}
