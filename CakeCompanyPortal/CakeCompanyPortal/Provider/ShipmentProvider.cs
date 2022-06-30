using CakeCompanyPortal.DataAccess;
using CakeCompanyPortal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.Provider
{
    public static class ShipmentProvider
    {
        private static UnitOfWork _db = new UnitOfWork();

        public static IEnumerable<Invoice> GetAllShipmentDetails()
        {
            IEnumerable<Invoice> _Invoices = null;
            try
            {
                _Invoices = _db.InvoiceRepository.GetAll();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return _Invoices;
        }

        public static void CreateInvoice(Invoice invoice)
        {
            IEnumerable<Invoice> _invoices = null;
            try
            {
                _invoices = _db.InvoiceRepository.GetAll();
                if (_invoices != null && _invoices.Count() > 0)
                {
                    var invID = _invoices.Max(x => x.InvoiceID);
                    invoice.InvoiceID = Convert.ToInt16(invID) + 1;
                }
                else
                    invoice.InvoiceID = 100;
                _db.InvoiceRepository.Insert(invoice);
                _db.Save();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void BulkInsert(List<Invoice> invoiceLst)
        {
            IEnumerable<Invoice> _invoices = null;
            int vID = 99;
            try
            {
                _invoices = _db.InvoiceRepository.GetAll();
                if (_invoices != null && _invoices.Count() > 0)
                {
                    var invoiceID = _invoices.Max(x => x.InvoiceID);
                    vID = Convert.ToInt32(invoiceID);
                }

                foreach (Invoice inv in invoiceLst)
                {
                    inv.InvoiceID = ++vID;

                    inv.TransportName = _db.TransportRepository.GetById(inv.TransportID).TransportType;
                    inv.ClientName = _db.ClientRepository.GetById(inv.ClientID).FirstName;

                    _db.InvoiceRepository.Insert(inv);
                    _db.Save();
                }

            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }
        public static void DeleteAll()
        {
            try
            {
                _db.InvoiceRepository.DeleteAll();
                _db.Save();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }
    }
}
