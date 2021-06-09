using System.Collections.Generic;
using Kash.Domain.Flow.Model;
using Kash.Domain.Flow.Model.Filters;

namespace Kash.Domain.Flow.Service
{
    public interface IServiceFlow
    {
        Account Update(Account account);
        Account Remove(Account account);
        Invoice Update(Invoice invoice);
        Invoice Remove(Invoice invoice);
        IEnumerable<Invoice> List(InvoiceFilters invoiceFilters);
    }
}
