using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeCompanyPortal.Helper;
using CakeCompanyPortal.Provider;

namespace CakeCompanyPortal.Service
{
    public sealed class CakeShopOrderService
    {
        private CakeShopOrderService() { }

        private static CakeShopOrderService _orderInstance;

        public static CakeShopOrderService GetInstance()
        {
            if (_orderInstance == null)
            {
                _orderInstance = new CakeShopOrderService();
            }
            return _orderInstance;
        }


        public static IEnumerable<Transport> GetAllTransports()
        {
            IEnumerable<Transport> transport = null;
            try
            {
                transport = TransportProvider.getAllTransport();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return transport;
        }

        public static IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> products = null;
            try
            {
                products = ProductProvider.GetAllProduct();

            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return products;
        }

        public static IEnumerable<Order> GetAllOrders()
        {
            IEnumerable<Order> orders = null;
            try
            {
                orders = OrderProvider.GetAllOrders();

            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return orders;
        }

        public static IEnumerable<Client> GetAllClients()
        {
            IEnumerable<Client> clients = null;
            try
            {
                clients = ClientProvider.GetAllClients();

            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return clients;
        }

        public static IEnumerable<Invoice> GetAllInvoices()
        {
            IEnumerable<Invoice> _Invoices = null;
            try
            {
                _Invoices = ShipmentProvider.GetAllShipmentDetails();

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
            try
            {
                ShipmentProvider.CreateInvoice(invoice);
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static int CreateOrder(Order order)
        {
            try
            {

                return OrderProvider.AddOrder(order);
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static int CreateClient(Client client)
        {

            try
            {
                if (client != null)
                {
                    return ClientProvider.AddClient(client);
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return -1;
        }

        public static void CreateProduct(Product product)
        {
            try
            {
                if (product != null)
                {
                    ProductProvider.AddProduct(product);
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void BulkInsertProduct(List<Product> products)
        {
            try
            {
                if (products != null && products.Count > 0)
                    ProductProvider.BulkInsert(products);
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void BulkInsertTransport(List<Transport> transports)
        {
            try
            {
                if (transports != null && transports.Count() > 0)
                {
                    TransportProvider.BulkInsert(transports);
                }

            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void BulkInsertOrders(List<Order> orders)
        {
            try
            {
                if (orders != null && orders.Count() > 0)
                {
                    OrderProvider.BulkInsert(orders);
                }

            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void BulkInsertClient(List<Client> clients)
        {
            try
            {
                if (clients != null && clients.Count() > 0)
                {
                    ClientProvider.BulkInsert(clients);
                }

            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void BulkInsertInvoice(List<Invoice> invoices)
        {
            try
            {
                if (invoices != null && invoices.Count() > 0)
                {
                    ShipmentProvider.BulkInsert(invoices);
                }

            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void RefreshDatabase()
        {
            try
            {
                TransportProvider.DeleteAll();
                ClientProvider.DeleteAll();
                ProductProvider.DeleteAll();
                ShipmentProvider.DeleteAll();
                OrderProvider.DeleteAll();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

    }

}
