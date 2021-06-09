using System;
using System.Collections.Generic;
using System.Linq;
using Kash.Domain.Flow.Model;
using Kash.Domain.Flow.Model.Filters;
using Kash.Domain.Flow.Repository;
using Microsoft.EntityFrameworkCore;

namespace Kash.Repository.Flow.EF
{
    public class RepositoryFlow : RepositoryFlowContext, IRepositoryFlow
    {
        public RepositoryFlow(DbContextOptions<RepositoryFlowContext> options) : base(options) { }

        public IEnumerable<Invoice> List(InvoiceFilters invoiceFilters)
        {
            throw new NotImplementedException();
        }

        public Account Remove(Account account)
        {
            throw new NotImplementedException();
        }

        public Invoice Remove(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public Account Update(Account account)
        {
            var accountFound = Accounts.FirstOrDefault(a => a.ID == account.ID);
            if (accountFound == null)
            {
                Accounts.Add(account);
                SaveChanges();
                return account;
            }
            accountFound.Type = account.Type;
            accountFound.Status = account.Status;
            accountFound.Name = account.Name;
            accountFound.ModifyUserID = account.ModifyUserID;
            accountFound.ModifyDate = DateTime.Now; ;
            SaveChanges();
            return accountFound;
        }

        public Invoice Update(Invoice invoice)
        {
            var invoiceFound = Invoices.FirstOrDefault(a => a.ID == invoice.ID);
            if (invoiceFound == null)
            {
                Invoices.Add(invoice);
                SaveChanges();
                return invoice;
            }
            invoiceFound.AccountID = invoice.AccountID;
            invoiceFound.Type = invoice.Type;
            invoiceFound.Status = invoice.Status;
            invoiceFound.EntryDate = invoice.EntryDate;
            invoiceFound.ReferenceDate = invoice.ReferenceDate;
            invoiceFound.PaymentDate = invoice.PaymentDate;
            invoiceFound.Value = invoice.Value;
            invoiceFound.Fees = invoice.Fees;
            invoiceFound.Fines = invoice.Fines;
            invoiceFound.Amount = invoice.Amount;
            invoiceFound.Description = invoice.Description;
            invoiceFound.ModifyUserID = invoice.ModifyUserID;
            invoiceFound.ModifyDate = DateTime.Now; ;
            SaveChanges();
            return invoiceFound;
        }
    }
}
