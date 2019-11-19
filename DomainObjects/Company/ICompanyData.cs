using System;
using System.Collections.Generic;
using System.Text;

namespace DomainObjects.Company
{
    public interface ICompanyData
    {
        string Name { get; set; }
        string Regon { get; set; }
    }
}
