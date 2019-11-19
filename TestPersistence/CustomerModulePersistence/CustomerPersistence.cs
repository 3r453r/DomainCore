using CustomerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestPersistence;

namespace TestPersistence.CustomerModulePersistence
{
    public class CustomerPersistence : ICustomerPersistence
    {
        List<CustomerUnitOfWork> UnitsOfWork { get; } = new List<CustomerUnitOfWork>();
        public ICustomerWork GetUnitOfWork(int transactionId)
        {
            var uow = UnitsOfWork.SingleOrDefault(uow => uow.TransactionId == transactionId);
            if (uow is ICustomerWork)
            {
                return uow;
            }
            else
            {
                var newUnit = new CustomerUnitOfWork(new TestContext(), transactionId);
                UnitsOfWork.Add(newUnit);
                return newUnit;
            }
        }
    }
}
