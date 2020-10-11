using Domain;
using Domain.Entities;
using Persistence.AccountModule;
using Persistence.CustomerModule;
using System;
using System.Threading.Tasks;

namespace DomainApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var customerModule = new CustomerModule.Module(new CustomerUnitOfWork(new CustomerContext(new Microsoft.EntityFrameworkCore.DbContextOptions<CustomerContext> { })));

            var accountModule = new AccountModule.Module(new AccountUnitOfWork(new AccountContext(new Microsoft.EntityFrameworkCore.DbContextOptions<AccountContext> { })), customerModule);

            var environmentAdapter = new EnvironmentAdapter.Adapter(new EnvironmentAdapter.UIAdapter.ConsoleAdapter(), new EnvironmentAdapter.ServiceAdapter.FakeAdapter());

            var domain = new SgbDomain(environmentAdapter, customerModule, accountModule);

            var response = await domain.CreateAccount(new CreateAccountRequest { Debit = 1000, Customer = new Customer { Name = "Adam"}, Nrb = "1342151313" }, new Employee { Pesel = "8803265123" });
            Console.ReadKey();
        }

        public class Employee : IEmployee
        {
            public long Id { get; set; }

            public string Pesel { get; set; }
        }

        public class CreateAccountRequest : ICreateAccountRequest
        {
            public long CustomerId { get; set; }

            public ICustomer Customer { get; set; }

            public decimal Debit { get; set; }

            public string Nrb { get; set; }
        }

        public class Customer : ICustomer
        {
            public long Id { get; set; }

            public string Name { get; set; }
        }
    }
}
