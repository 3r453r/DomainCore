using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public interface IAccount
    {
        decimal Debit { get; }
        string Nrb { get; }
        ICustomer Owner { get; }
    }

    public class Account : IAccount
    {        
        public decimal Debit { get; set; }

        public string Nrb { get; set; }

        public ICustomer Owner { get; set; }
    }

    public class AccountInfo
    {
        public string Nrb { get; set; }
        public decimal Debit { get; set; }
    }
}
