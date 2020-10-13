using Domain.Entities;
using Domain.Modules;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface ISgbDomain
    {
        IEnvironmentAdapter EnvironmentAdapter { set; }
        ICustomerModule CustomerModule { set; }
        IAccountModule AccountModule { set; }

        Task<ICreateAccountResponse> CreateAccount(ICreateAccountRequest createAccountRequest, IEmployee employee);
        Task<ICreateAccountResponse> ContinueCreateAccount(long accountApplicationId, IEmployee employee, string additionalInfo);
    }

    public class SgbDomain : ISgbDomain
    {
        public SgbDomain(IEnvironmentAdapter environmentAdapter, ICustomerModule customerModule, IAccountModule accountModule)
        {
            _environmentAdapter = environmentAdapter;
            _customerModule = customerModule;
            _accountModule = accountModule;
        }

        private IEnvironmentAdapter _environmentAdapter;
        public IEnvironmentAdapter EnvironmentAdapter { set { _environmentAdapter = value; } }

        private ICustomerModule _customerModule;
        public ICustomerModule CustomerModule { set { _customerModule = value; } }

        private IAccountModule _accountModule;
        public IAccountModule AccountModule { set { _accountModule = value; } }

        public async Task<ICreateAccountResponse> CreateAccount(ICreateAccountRequest createAccountRequest, IEmployee employee)
        {
            ICustomer customer;
            if(createAccountRequest.CustomerId != 0)
            {
                customer = _customerModule.GetCustomer(createAccountRequest.CustomerId);
                if (customer == null) throw new DataIntegrityViolation($"Customer {createAccountRequest.CustomerId} doesn't exist");
            }
            else if(createAccountRequest.Customer != null)
            {
                customer = _customerModule.CreateCustomer(new CustomerInfo { Name = createAccountRequest.Customer .Name});
                _customerModule.StageChanges();
            }
            else
                throw new DataIntegrityViolation($"No customer specified");

            var accountApp = _accountModule.CreateAccountApplication(employee);
            accountApp.Nrb = createAccountRequest.Nrb;

            try
            {
                accountApp.AddApplicant(customer, CustomerProductRole.Owner);               
            }
            catch(DomainException)
            {
                _customerModule.RejectChanges();
                _accountModule.RejectChanges();
                throw;
            }

            accountApp.ApplicantsAdded();
            _customerModule.SaveChanges();
            _accountModule.SaveChanges();

            string additionalInfo;
            try
            {
                additionalInfo = await _environmentAdapter.GetApplicationAdditionalInfo(accountApp, "Set debit");
            }
            catch (Exception)
            {
                _accountModule.SaveChanges();
                _customerModule.SaveChanges();
                return new CreateAccountResponse { Application = accountApp };
            }
            
            var account = _accountModule.CreateAccount(new AccountInfo { Debit = decimal.Parse(additionalInfo), Nrb = createAccountRequest.Nrb }, accountApp.Applicants.First().Id);
            accountApp.AdditionalDataCollected(account);
            _accountModule.StageChanges();

            try
            {
                await _environmentAdapter.CreateAccountInTransactionSystem(account);
                if(string.IsNullOrEmpty(account.Nrb))
                {
                    _accountModule.RejectChanges();
                    return new CreateAccountResponse { Application = accountApp };
                }
                else
                {
                    accountApp.AccountCreated();
                    _accountModule.SaveChanges();
                    return new CreateAccountResponse { Account = account, Application = accountApp };
                }
            }
            catch(Exception)
            {
                _accountModule.RejectChanges();
                return new CreateAccountResponse { Application = accountApp };
            }
        }

        public async Task<ICreateAccountResponse> ContinueCreateAccount(long accountApplicationId, IEmployee employee, string additionalInfo)
        {
            var accountApp = _accountModule.GetAccountApplication(accountApplicationId);
            if (accountApp == null)
                throw new DataIntegrityViolation($"No application with id {accountApplicationId}");
            else if (accountApp.Operator.Id == employee.Id)
                throw new BussinessRuleViolation("Must have different operator");

            switch(accountApp.Step)
            {
                case AccountApplicationStep.AddParticipants:
                    {
                        long customerId;
                        try
                        {
                             customerId = long.Parse(additionalInfo);
                        }
                        catch (Exception)
                        {
                            return new CreateAccountResponse { Application = accountApp };
                        }
                        accountApp.AddApplicant(_customerModule.GetCustomer(customerId), CustomerProductRole.Owner);
                        accountApp.ApplicantsAdded();
                        _accountModule.SaveChanges();

                        var response = await GetAdditionalInfo(accountApp, additionalInfo);
                        if (response.Application != null)
                            return response;
                        else
                        {
                            return await CreateAccountInDef(response);
                        }
                    }
                case AccountApplicationStep.AdditionalDataCollection:
                    {
                        var response = await GetAdditionalInfo(accountApp, additionalInfo);
                        if (response.Application != null)
                            return response;
                        else
                        {
                            return await CreateAccountInDef(response);
                        }
                    }
                case AccountApplicationStep.DefAccountCreation:
                    {
                        return await CreateAccountInDef(new CreateAccountResponse { Account = accountApp.Account, Application = accountApp });
                    }
                default:
                    throw new DataIntegrityViolation($"Invalid accountApplication state: {accountApp.Step}");
            }
        }

        private async Task<CreateAccountResponse> GetAdditionalInfo(IAccountApplication accountApp, string additionalInfo = null)
        {
            if(additionalInfo == null)
            {
                try
                {
                    additionalInfo = await _environmentAdapter.GetApplicationAdditionalInfo(accountApp, "Set debit");
                }
                catch (Exception)
                {
                    _accountModule.SaveChanges();
                    _customerModule.SaveChanges();
                    return new CreateAccountResponse { Application = accountApp };
                }
            }
            
            var account = _accountModule.CreateAccount(new AccountInfo { Debit = decimal.Parse(additionalInfo), Nrb = accountApp.Nrb }, accountApp.Applicants.First().Id);
            accountApp.AdditionalDataCollected(account);
            _accountModule.StageChanges();
            return new CreateAccountResponse { Account = account };
        }

        private async Task<CreateAccountResponse> CreateAccountInDef(CreateAccountResponse response)
        {
            try
            {
                await _environmentAdapter.CreateAccountInTransactionSystem(response.Account);
                if (string.IsNullOrEmpty(response.Account.Nrb))
                {
                    _accountModule.RejectChanges();
                    return new CreateAccountResponse { Application = response.Application };
                }
                else
                {
                    response.Application.AccountCreated();
                    _accountModule.SaveChanges();
                    return new CreateAccountResponse { Account = response.Account };
                }
            }
            catch (Exception)
            {
                _accountModule.RejectChanges();
                return new CreateAccountResponse { Application = response.Application };
            }
        }
    }

    public interface ICreateAccountRequest
    {
        long CustomerId { get; }
        ICustomer Customer { get; }
        decimal Debit { get; }

        string Nrb { get; }
    }

    public interface ICreateAccountResponse
    {
        IAccount Account { get; }
        IAccountApplication Application { get; }
    }

    public class CreateAccountResponse : ICreateAccountResponse
    {
        public IAccount Account { get; set; }

        public IAccountApplication Application { get; set; }
    }

    #region DomainExceptions
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    public class BussinessRuleViolation : DomainException
    {
        public BussinessRuleViolation()
        {
        }

        public BussinessRuleViolation(string message)
            : base(message)
        {
        }

        public BussinessRuleViolation(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class DataIntegrityViolation : DomainException
    {
        public DataIntegrityViolation()
        {
        }

        public DataIntegrityViolation(string message)
            : base(message)
        {
        }

        public DataIntegrityViolation(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class EnvironmentFailure : DomainException
    {
        public EnvironmentFailure()
        {
        }

        public EnvironmentFailure(string message)
            : base(message)
        {
        }

        public EnvironmentFailure(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    #endregion
}
