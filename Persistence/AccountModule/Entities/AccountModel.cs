using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.AccountModule.Entities
{
    public class AccountModel : Account
    {
        ICustomer _owner;
        new ICustomer Owner { get { return _owner; } set { _owner = value; CustomerId = value.Id; } }
        public long CustomerId { get; set; }
    }
}
