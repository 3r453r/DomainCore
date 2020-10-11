using AccountModule;
using AccountModule.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.AccountModule
{
    public class AccountUnitOfWork : IAccountUnitOfWork
    {
        private AccountContext context;

        private IAccountApplicationRepository _accountApplicationRepository;
        public IAccountApplicationRepository AccountApplicationRepository { set { _accountApplicationRepository = value; } }

        private IAccountRepository _accountRepository;
        public IAccountRepository AccountRepository { set { _accountRepository = value; } }
        public AccountUnitOfWork(AccountContext context)
        {
            this.context = context;
            _accountApplicationRepository = new AccountApplicationRepository(context);
            _accountRepository = new AccountRepository(context);

        }
        public IAccountApplicationRepository GetAccountApplicationRepository()
        {
            return _accountApplicationRepository;
        }

        public IAccountRepository GetAccountRepository()
        {
            return _accountRepository;
        }

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
