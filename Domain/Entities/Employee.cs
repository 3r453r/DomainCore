using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public interface IEmployee
    {
        long Id { get; }
        string Pesel { get; }
    }

    public class Employee : IEmployee
    {
        public long Id { get; set; }

        public string Pesel { get; set; }
    }
}
