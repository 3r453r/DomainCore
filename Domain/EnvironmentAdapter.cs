using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IEnvironmentAdapter
    {
        Task<string> GetApplicationAdditionalInfo(IAccountApplication accountApplicaton, string infoName);
        Task CreateAccountInTransactionSystem(IAccount account);
    }   
}
