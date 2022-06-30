using CakeCompanyPortal.DataAccess;
using CakeCompanyPortal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.Provider
{
    public static class OrderProvider
    {

        private static UnitOfWork _db = new UnitOfWork();

        public static IEnumerable<Order> GetAllOrders()
        {
            IEnumerable<Order> _orders = null;
            try
            {
                _orders = _db.OrderRepository.GetAll();

            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return _orders;
        }

        public static int AddOrder(Order order)
        {
            try
            {
                if (order != null && order.ProductID != null)
                {
                    var orders = _db.OrderRepository.GetAll();
                    if (orders != null && orders.Count() > 0)
                    {
                        var orderID = orders.Max(x => x.OrderID);
                        order.OrderID = Convert.ToInt16(orderID) + 1;
                    }
                    else order.OrderID = 100;

                    var prepTime = _db.ProductRepository.GetById(order.ProductID).PreparationTime;
                    order.EstimatedDeliveryTime = DateTime.Now.Add(TimeSpan.FromMinutes(double.Parse(prepTime.ToString())));
                    _db.OrderRepository.Insert(order);
                    _db.Save();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return order.OrderID;
        }

        public static void BulkInsert(List<Order> ordersLst)
        {
            IEnumerable<Order> _orders = null;
            //Invoice invoice = null;
            int? billAmount = 0;
            //int prodPrice = 0;
            int? totalProdsQty = 0;
            int oID = 99;
            string orderStatus = string.Empty;
            try
            {
                _orders = _db.OrderRepository.GetAll();
                if (_orders != null && _orders.Count() > 0)
                {
                    var orderID = _orders.Max(x => x.OrderID);
                    oID = Convert.ToInt32(orderID);
                }

                foreach (Order ordr in ordersLst)
                {

                    ordr.OrderID = ++oID;

                    ordr.ProductName = _db.ProductRepository.GetById(ordr.ProductID).ProductName + " [ " + _db.ProductRepository.GetById(ordr.ProductID).ProductType + " ]";
                    ordr.ClientName = _db.ClientRepository.GetById(ordr.ClientId).FirstName;
                    ordr.EstimatedDeliveryTime = UpdateEstimateTime(ordr);
                    ordr.OrderStatus = UpdateOrderStatus(ordr);
                    orderStatus = ordr.OrderStatus;
                    totalProdsQty += ordr.ProductQuantity;
                    billAmount += ordr.ProductQuantity * _db.ProductRepository.GetById(ordr.ProductID).ProductPrice;

                    _db.OrderRepository.Insert(ordr);
                    _db.Save();
                }
                createInvoice(ordersLst.FirstOrDefault(), billAmount, totalProdsQty);
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }
        private static void createInvoice(Order order, int? billamount, int? totalProdsQty)
        {
            Invoice invoice = null;
            Transport transport = null;
            int vID = 99;
            try
            {
                if (order != null)
                {
                    invoice = new Invoice();
                    invoice.BillAmount = billamount;
                    invoice.BillStatus = order.OrderStatus;
                    invoice.ClientName = order.ClientName;
                    invoice.OrderNumber = order.OrderNumber;

                    transport = TransportProvider.GetTransportByQuantity(totalProdsQty);
                    if (transport != null)
                    {
                        invoice.TransportID = transport.TransportID;
                        invoice.TransportName = transport.TransportType;
                    }
                    invoice.CreatedDate = DateTime.Now;
                    var _invoices = _db.InvoiceRepository.GetAll();
                    if (_invoices != null && _invoices.Count() > 0)
                    {
                        var invoiceID = _invoices.Max(x => x.InvoiceID);
                        vID = Convert.ToInt32(invoiceID);
                    }
                    invoice.InvoiceID = ++vID;

                    _db.InvoiceRepository.Insert(invoice);
                    _db.Save();
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }

        }

        private static string UpdateOrderStatus(Order order)
        {
            Product product = null;
            Client client = null;
            string _OrderStatus = string.Empty;
            try
            {
                if (order != null && order.ProductID != null)
                {
                    product = _db.ProductRepository.GetById(order.ProductID);
                    client = _db.ClientRepository.GetById(order.ClientId);
                    if (product != null)
                    {
                        if (order.EstimatedDeliveryTime > DateTime.Now.Add(TimeSpan.FromMinutes(Convert.ToDouble(product.PreparationTime))))
                        {
                            _OrderStatus = OrderStatus.Rejected;
                        }
                        else
                        {
                            _OrderStatus = OrderStatus.Preparing;
                        }
                    }
                    if (client != null)
                    {
                        if (client.FirstName.Contains("Important"))
                        {
                            _OrderStatus = OrderStatus.Preparing;
                        }
                        else
                        {
                            _OrderStatus = OrderStatus.Rejected;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return _OrderStatus;
        }

        private static DateTime UpdateEstimateTime(Order order)
        {
            Product product = null;
            DateTime _estimateTime = DateTime.Now;
            try
            {
                if (order != null && order.ProductID != null)
                {
                    product = _db.ProductRepository.GetById(order.ProductID);
                    if (product != null)
                    {
                        _estimateTime = DateTime.Now.Add(TimeSpan.FromMinutes(Convert.ToDouble(product.PreparationTime)));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return _estimateTime;
        }
        public static void DeleteAll()
        {
            try
            {
                _db.OrderRepository.DeleteAll();
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
