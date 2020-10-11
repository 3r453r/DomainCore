using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountModule.Repositories
{
    public interface IAccountApplicationRepository
    {
        AccountApplication New();
        void Delete(AccountApplication accountApplication);
        AccountApplication Get(decimal accountApplicationId);

        IEnumerable<AccountApplication> AccountApplicationsByStep(AccountApplicationStep step);
    }
}
