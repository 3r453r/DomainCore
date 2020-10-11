using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Modules
{
    public interface IModule
    {
        void SaveChanges();
        void RejectChanges();
        void StageChanges();
    }
}
