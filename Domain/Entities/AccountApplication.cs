using Domain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public interface IAccountApplication
    {
        long Id { get; }
        string Nrb { get; }
        AccountApplicationStep Step { get; }
        IEmployee Operator { get; set; }
        IEnumerable<ICustomer> Applicants { get; }
        IAccount Account { get; }

        void AddApplicant(ICustomer customer, CustomerProductRole role);
        CustomerProductRole GetCustomerProductRole(ICustomer customer);
        void ApplicantsAdded();
        void AdditionalDataCollected(IAccount account);

        void AccountCreated();
    }

    public class AccountApplication : IAccountApplication
    {
        public long Id { get; set; }
        public AccountApplicationStep Step { get; private set; } = AccountApplicationStep.AddParticipants;

        private List<Applicant> applicants = new List<Applicant>();
        private bool applicantsClosed = false;
        public IEnumerable<ICustomer> Applicants => applicants.Select(a => a.Customer);

        public IEmployee Operator { get; set; }

        public IAccount Account { get; private set; }

        public string Nrb { get; set; }

        public void AddApplicant(ICustomer customer, CustomerProductRole role) {
            if (!applicantsClosed)
                applicants.Add(new Applicant { Customer = customer, Role = role });
            else
                throw new BussinessRuleViolation("Cannot add new participants");
        } 
        public CustomerProductRole GetCustomerProductRole(ICustomer customer)
        {
            try
            {
                return applicants.First(a => a.Customer == customer).Role;
            }
            catch(InvalidOperationException)
            {
                throw new DataIntegrityViolation($"Application {Id} has no applicant with id {customer.Id}");
            }
        }

        public void ApplicantsAdded()
        {
            applicantsClosed = true;
            Step = AccountApplicationStep.AdditionalDataCollection;
        }

        public void AdditionalDataCollected(IAccount account)
        {
            Step = AccountApplicationStep.DefAccountCreation;
            Account = account;
        }

        public void AccountCreated()
        {
            Step = AccountApplicationStep.Closed;
        }

        private class Applicant
        {
            public ICustomer Customer { get; set; }
            public CustomerProductRole Role { get; set; }


        }
    }

    public enum AccountApplicationStep
    {
        AddParticipants,
        AdditionalDataCollection,
        DefAccountCreation,
        Closed
    }
}
