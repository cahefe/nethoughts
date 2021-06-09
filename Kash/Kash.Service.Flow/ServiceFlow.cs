using System;
using System.Collections.Generic;
using Kash.Domain.Flow.Model;
using Kash.Domain.Flow.Model.Filters;
using Kash.Domain.Flow.Repository;
using Kash.Domain.Flow.Service;

namespace Kash.Service.Flow
{
    public class ServiceFlow : IServiceFlow
    {
        readonly IRepositoryFlow _repositoryFlow;

        public ServiceFlow(IRepositoryFlow repositoryFlow)
        {
            _repositoryFlow = repositoryFlow ?? throw new NullReferenceException(nameof(repositoryFlow));
        }
        public IEnumerable<Invoice> List(InvoiceFilters invoiceFilters) => _repositoryFlow.List(invoiceFilters);

        public Account Remove(Account account)
        {
            return _repositoryFlow.Remove(account);
        }

        public Invoice Remove(Invoice invoice)
        {
            return _repositoryFlow.Remove(invoice);
        }

        public Account Update(Account account)
        {
            return _repositoryFlow.Update(account);
        }

        public Invoice Update(Invoice invoice)
        {
            return _repositoryFlow.Update(invoice);
        }
    }
}
