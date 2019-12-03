using DomainObjects.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainObjects.Customer
{
    public interface IIndividualCustomer : ICustomer, IPerson
    {
        void UpdatePersonData(IPersonData personData);
    }
}
