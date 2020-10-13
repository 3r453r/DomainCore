using AccountModule.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence.AccountModule
{
    public class AccountApplicationRepository : IAccountApplicationRepository
    {
        private AccountContext context;

        public AccountApplicationRepository(AccountContext context)
        {
            this.context = context;
        }
        public IEnumerable<AccountApplication> AccountApplicationsByStep(AccountApplicationStep step)
        {
            return context.AccountApplications.Where(aa => aa.Step == step);
        }

        public void Delete(AccountApplication accountApplication)
        {
            context.AccountApplications.Remove(context.AccountApplications.Find(accountApplication.Id));
        }

        public AccountApplication Get(decimal accountApplicationId)
        {
            return context.AccountApplications.Find((long)accountApplicationId);
        }

        public AccountApplication New()
        {
            var accountApplication = new Persistence.AccountModule.Entities.AccountApplicationModel();
            context.AccountApplications.Add(accountApplication);
            return accountApplication;
        }
    }
}
