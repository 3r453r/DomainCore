namespace DomainCore
{
    public interface ICustomerModule
    {
        ICustomerUnitOfWork GetUnitOfWork(int transactionId);       
    }
}