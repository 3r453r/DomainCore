using AccountModule.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountModule
{
    public interface IAccountUnitOfWork
    {
        void SaveChanges();
        void RejectChanges();
        void StageChanges();

        IAccountApplicationRepository GetAccountApplicationRepository();
        IAccountRepository GetAccountRepository();
    }
}
