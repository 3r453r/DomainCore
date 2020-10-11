using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Modules
{
    public interface IAccountModule : IModule
    {
        ICustomerModule CustomerModule { set; }
        AccountApplication CreateAccountApplication(IEmployee employee);
        Account CreateAccount(AccountInfo accountInfo, long customerId);
        AccountApplication GetAccountApplication(long accountApplicationId);
        Account GetAccount(string nrb);
    }

    public enum CustomerProductRole
    {
        Owner,
        Proxy
    }
}
