using AccountModule.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.AccountModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.AccountModule
{
    public class AccountRepository : IAccountRepository
    {
        private AccountContext context;

        public AccountRepository(AccountContext context)
        {
            this.context = context;
        }
        public IEnumerable<Account> AccountsByCustomer(decimal customerId)
        {
            return context.Accounts.Where(a => a.CustomerId == (long)customerId);
        }

        public IEnumerable<Account> AccountsByDebit(decimal debit)
        {
            return context.Accounts.Where(a => a.Debit == debit);
        }

        public void Delete(Account account)
        {
            context.Accounts.Remove(context.Accounts.Find(account.Nrb));
        }

        public Account Get(string nrb)
        {
            return context.Accounts.Find(nrb);
        }

        public Account GetReadonly(string nrb)
        {
            return context.Accounts.AsNoTracking().FirstOrDefault(a => a.Nrb == nrb);
        }

        public Account New(AccountInfo accountInfo)
        {
            var newAccount = new AccountModel { Debit = accountInfo.Debit, Nrb = accountInfo.Nrb };
            context.Accounts.Add(newAccount);
            return newAccount;
        }
    }
}
