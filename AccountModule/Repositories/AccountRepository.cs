using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountModule.Repositories
{
    public interface IAccountRepository
    {
        Account New(AccountInfo accountInfo);
        void Delete(Account account);
        Account Get(string nrb);
        Account GetReadonly(string nrb);

        IEnumerable<Account> AccountsByDebit(decimal debit);
        IEnumerable<Account> AccountsByCustomer(decimal customerId);
    }
}
