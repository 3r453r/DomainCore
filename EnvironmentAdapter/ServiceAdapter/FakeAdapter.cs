using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentAdapter.ServiceAdapter
{
    public class FakeAdapter : IServiceAdapter
    {
        public async Task CreateAccountInTransactionSystem(IAccount account)
        {
            Console.WriteLine($"Creating account {account.Nrb} in def");
            await Task.Delay(500);
            Console.WriteLine($"Account created");
            return;
        }
    }
}
