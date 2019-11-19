using System;
using System.Collections.Generic;
using System.Text;

namespace DomainObjects.UnitOfWork
{
    public interface IUnitOfWork
    {
        int TransactionId { get; }
        void StageChanges();
        void UnstageChanges();
        void FinalizeChanges();
    }
}
