using System;
using System.Collections.Generic;
using System.Text;

namespace DomainCore
{
    interface IDomain
    {
        ICustomerModule CustomerModule { get; set; }
    }
}
