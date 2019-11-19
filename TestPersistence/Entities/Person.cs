using DomainObjects.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestPersistence.Entities
{
    public class Person : IPersonData
    {
        public Person(IPersonData data)
        {
            FirstName = data.FirstName;
            OtherNames = string.Join(',', data.OtherNames);
            LastName = data.LastName;
            BirthDate = data.BirthDate;
            Pesel = data.Pesel;
        }
        public Person()
        {
        }

        public long Id { get; set; }

        public string FirstName { get; set; }

        IList<string> IPersonData.OtherNames => OtherNames.Split(',');
        public string OtherNames { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Pesel { get; set; }
    }
}
