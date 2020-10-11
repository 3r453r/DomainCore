using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentAdapter.UIAdapter
{
    public class ConsoleAdapter : IUserInterfaceAdapter
    {
        public async Task<string> GetApplicationAdditionalInfo(IAccountApplication accountApplicaton, string infoName)
        {
            Console.WriteLine($"podaj {infoName}");
            await Task.Delay(100);
            return Console.ReadLine();
        }
    }
}
