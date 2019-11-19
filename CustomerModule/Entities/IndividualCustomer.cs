using DomainObjects.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerModule
{
    public class IndividualCustomer : IIndividualCustomer
    {
        public IndividualCustomer()
        { }
        public IndividualCustomer(Person person)
        {
            Person = person;
        }
        Person Person { get; }
        public string Id { get; set; }

        public string FirstName => Person.FirstName;

        public IList<string> OtherNames => Person.OtherNames;

        public string LastName => Person.LastName;

        public DateTime? BirthDate => Person.BirthDate;

        public string Pesel => Person.Pesel;

        public CustomerType CustomerType => CustomerType.Individual;

        public int GetAge()
        {
            return Person.GetAge();
        }
    }
}
