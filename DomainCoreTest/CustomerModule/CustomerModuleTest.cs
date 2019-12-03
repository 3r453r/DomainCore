using CustomerModule;
using DomainCore;
using DomainObjects.Person;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DomainCoreTest.CustomerModule
{
    public class CustomerModuleTest
    {
        private Domain domain;
        Domain Domain { get {
                if (domain == null)
                {
                    domain = Domain.Instance;
                    domain.CustomerModule = new Module(new TestPersistence.CustomerModulePersistence.CustomerPersistence());
                    return domain;
                }
                else
                    return domain;
            } }
        [Theory]
        [ClassData(typeof(PersonData))]
        public void UpdatedCustomerPropertyPersists(IPersonData personData)
        {
            var mock = new Mock<IPersonData>();
            mock.SetupGet(x => x.OtherNames).Returns(new string[0]);
            string id = Domain.CreateCustomer(mock.Object);

            Domain.UpdateCustomerPersonData(id, personData);
            var storedPerson = Domain.GetCustomerPersonData(id);

            Assert.Equal(personData.FirstName, storedPerson.FirstName);
            Assert.Equal(personData.LastName, storedPerson.LastName);
            Assert.Collection(personData.OtherNames, GenerateOtherNamesAction(personData.OtherNames));
            Assert.Equal(personData.Pesel, storedPerson.Pesel);
            Assert.Equal(personData.BirthDate, storedPerson.BirthDate);
        }

        private Action<string>[] GenerateOtherNamesAction(IList<string> otherNames)
        {
            var actions = new List<Action<string>>();
            foreach (string name in otherNames)
            {
                actions.Add(s => Assert.Contains(s, otherNames));
            }
            return actions.ToArray();
        }
    }
}
