using Domain;
using Domain.Entities;
using Domain.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountModule
{
    public class Module : IAccountModule
    {
        private IAccountUnitOfWork _accountUnitOfWork;
        public IAccountUnitOfWork AccountUnitOfWork { set { _accountUnitOfWork = value; } }

        public ICustomerModule CustomerModule { set; private get; }

        public Module(IAccountUnitOfWork accountUnitOfWork, ICustomerModule customerModule)
        {
            _accountUnitOfWork = accountUnitOfWork;
            CustomerModule = customerModule;
        }

        public Account CreateAccount(AccountInfo accountInfo, long customerId)
        {
            var account = _accountUnitOfWork.GetAccountRepository().New(accountInfo);
            account.Owner = CustomerModule.GetCustomer(customerId);
            return account;
        }

        public AccountApplication CreateAccountApplication(IEmployee employee)
        {
            var app = _accountUnitOfWork.GetAccountApplicationRepository().New();
            app.Operator = employee;
            return app;
        }

        public Account GetAccount(string nrb)
        {
            return _accountUnitOfWork.GetAccountRepository().Get(nrb);
        }

        public AccountApplication GetAccountApplication(long accountApplicationId)
        {
            return _accountUnitOfWork.GetAccountApplicationRepository().Get(accountApplicationId);
        }

        public void RejectChanges() => _accountUnitOfWork.RejectChanges();

        public void SaveChanges() => _accountUnitOfWork.SaveChanges();

        public void StageChanges() => _accountUnitOfWork.StageChanges();
    }
}
