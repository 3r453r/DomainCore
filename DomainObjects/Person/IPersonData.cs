using System;
using System.Collections.Generic;
using System.Text;

namespace DomainObjects.Person
{
    public interface IPersonData
    {
        string FirstName { get; }
        IList<string> OtherNames { get; }
        string LastName { get; }
        DateTime? BirthDate { get; }
        string Pesel { get; }
    }
}
