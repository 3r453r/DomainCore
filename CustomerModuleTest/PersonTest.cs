using CustomerModule;
using System;
using System.Collections.Generic;
using Xunit;

namespace CustomerModuleTest
{
    public class PersonTest
    {
        public static IEnumerable<object[]> GetBirthDays()
        {
            yield return new object[] { DateTime.Parse("1900-01-01") };
            yield return new object[] { (null as DateTime?)};
            yield return new object[] { DateTime.Today.AddDays(365 * -12 - 1) };
        }

        [Theory]
        [MemberData(nameof(GetBirthDays))]
        public void Test1(DateTime? birthDate)
        {
            var person = new Person { BirthDate = birthDate };
            Assert.Equal(CalculateAge(birthDate), person.GetAge());
        }

        private int CalculateAge(DateTime? birthDate)
        {
            return -1;
        }
    }
}
