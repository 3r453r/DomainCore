using CustomerModule.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule
{
    public interface ICustomerUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public void SaveChanges();
        public void RejectChanges();
        public void StageChanges();
    }
}
