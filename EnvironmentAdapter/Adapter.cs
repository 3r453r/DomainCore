using Domain;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace EnvironmentAdapter
{
    public class Adapter : IEnvironmentAdapter
    {
        private IUserInterfaceAdapter _userInterfaceAdapter;
        public IUserInterfaceAdapter UserInterfaceAdapter { set { _userInterfaceAdapter = value; } }

        private IServiceAdapter _serviceAdapter;
        public IServiceAdapter ServiceAdapter { set { _serviceAdapter = value; } }
        public Adapter(IUserInterfaceAdapter uiAdapter, IServiceAdapter serviceAdapter)
        {
            _userInterfaceAdapter = uiAdapter;
            _serviceAdapter = serviceAdapter;
        }
        public Task CreateAccountInTransactionSystem(IAccount account)
        {
            return _serviceAdapter.CreateAccountInTransactionSystem(account);
        }

        public Task<string> GetApplicationAdditionalInfo(IAccountApplication accountApplicaton, string infoName)
        {
            return _userInterfaceAdapter.GetApplicationAdditionalInfo(accountApplicaton, infoName);
        }
    }

    public interface IUserInterfaceAdapter
    {
        Task<string> GetApplicationAdditionalInfo(IAccountApplication accountApplicaton, string infoName);
    }

    public interface IServiceAdapter
    {
        Task CreateAccountInTransactionSystem(IAccount account);
    }
}
