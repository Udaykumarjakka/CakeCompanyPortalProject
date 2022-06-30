using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> ProductRepository { get; }

        IGenericRepository<Order> OrderRepository { get; }

        IGenericRepository<Client> ClientRepository { get; }

        IGenericRepository<Invoice> InvoiceRepository { get; }

        IGenericRepository<Transport> TransportRepository { get; }

        void Save();
    }
}
