using CakeCompanyPortal.DataAccess;
using CakeCompanyPortal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {

        private CakeDbContext _CakeDbContext = new CakeDbContext();

        public IGenericRepository<Product> _ProductRepository;
        public IGenericRepository<Order> _OrderRepository;
        public IGenericRepository<Client> _ClientRepository;
        public IGenericRepository<Invoice> _InvoiceRepository;
        public IGenericRepository<Transport> _TransportRepository;


        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._ProductRepository == null)
                {
                    this._ProductRepository = new GenericRepository<Product>(_CakeDbContext);
                }
                return this._ProductRepository;
            }
        }

        public IGenericRepository<Order> OrderRepository
        {
            get
            {
                if (this._OrderRepository == null)
                {
                    this._OrderRepository = new GenericRepository<Order>(_CakeDbContext);
                }
                return this._OrderRepository;
            }
        }

        public IGenericRepository<Client> ClientRepository
        {
            get
            {
                if (this._ClientRepository == null)
                {
                    this._ClientRepository = new GenericRepository<Client>(_CakeDbContext);
                }
                return this._ClientRepository;
            }
        }

        public IGenericRepository<Invoice> InvoiceRepository
        {
            get
            {
                if (this._InvoiceRepository == null)
                {
                    this._InvoiceRepository = new GenericRepository<Invoice>(_CakeDbContext);
                }
                return this._InvoiceRepository;
            }
        }

        public IGenericRepository<Transport> TransportRepository
        {
            get
            {
                if (this._TransportRepository == null)
                {
                    this._TransportRepository = new GenericRepository<Transport>(_CakeDbContext);
                }
                return this._TransportRepository;
            }
        }

        public void Save()
        {
            try
            {
                _CakeDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }


        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        _CakeDbContext.Dispose();
                    }
                }
                this.disposed = true;
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }
    }
}
