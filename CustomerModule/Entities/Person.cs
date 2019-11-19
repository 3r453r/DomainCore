using DomainObjects.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule
{
    public class Person : IPerson
    {
        public Person()
        { }

        public Person(IPersonData data)
        {
            FirstName = data.FirstName;
            OtherNames = data.OtherNames;
            LastName = data.LastName;
            BirthDate = data.BirthDate;
            Pesel = data.Pesel;
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public IList<string> OtherNames { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Pesel { get; set; }

        public int GetAge()
        {
            return BirthDate is object ? DateTime.Now.Year - BirthDate.Value.Year : -1;
        }
    }
}
