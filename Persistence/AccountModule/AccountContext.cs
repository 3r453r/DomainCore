using Microsoft.EntityFrameworkCore;
using Persistence.AccountModule.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.AccountModule
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AccountDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>().HasKey(a => a.Nrb);
            modelBuilder.Entity<AccountApplicationModel>().Ignore(aa => aa.Operator).HasOne(aa => (AccountModel)aa.Account);
        }

        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<AccountApplicationModel> AccountApplications { get; set; }
    }
}
