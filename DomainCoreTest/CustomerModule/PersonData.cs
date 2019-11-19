using DomainObjects.Person;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DomainCoreTest.CustomerModule
{
    public class PersonData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var mock = new Mock<IPersonData>();

            mock.Setup(x => x.BirthDate).Returns(DateTime.Parse("1988-03-26"));
            mock.Setup(x => x.FirstName).Returns("Adam");
            mock.Setup(x => x.LastName).Returns("Adamowy");
            mock.Setup(x => x.OtherNames).Returns(new string[] { "Andrzej", "Alojzy" });
            mock.Setup(x => x.Pesel).Returns("0123456789");
            yield return new object[] { mock.Object };

            mock.Setup(x => x.BirthDate).Returns(DateTime.Parse("1998-07-11"));
            mock.Setup(x => x.FirstName).Returns("Basia");
            mock.Setup(x => x.LastName).Returns("Basiowa");
            mock.Setup(x => x.OtherNames).Returns(new string[] { "Bogumiła", "Bianka" });
            mock.Setup(x => x.Pesel).Returns("9876543210");
            yield return new object[] { mock.Object };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
    }
}
