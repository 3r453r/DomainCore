using System;
using System.Collections.Generic;
using System.Text;

namespace DomainObjects.Person
{
    public interface IPerson : IPersonData
    {
        int GetAge();
    }
}
