using CakeCompanyPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CakeCompanyPortal.Helper
{
    public static class PreLoadCakeDetails
    {
        public static void LoadCakeTables()
        {
            try
            {
                if (HttpContext.Current.Session["OnlyOnce"] == null)
                {
                    RefreshDatabase();
                    LoadTransport();
                    LoadClients();
                    LoadProducts();
                    LoadOrders();
                    HttpContext.Current.Session["OnlyOnce"] = "Data loaded, we can stop loading this menthod on every Invoice page load..!!";
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        private static void RefreshDatabase()
        {
            try
            {
                CakeShopOrderService.RefreshDatabase();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        private static void LoadTransport()
        {
            try
            {
                CakeShopOrderService.BulkInsertTransport(
                    new List<Transport>
                    {
                        new Transport{
                            TransportType = "Van",
                            QuantityAllowed = 1000,
                        },
                        new Transport{
                            TransportType = "Truck",
                            QuantityAllowed = 5000
                        },
                        new Transport{
                            TransportType = "Ship",
                            QuantityAllowed = 10000
                        }
                     }
                );
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }

        }

        private static void LoadClients()
        {
            try
            {
                CakeShopOrderService.BulkInsertClient(
                  new List<Client> {
                        new Client{
                            FirstName = "Steve Jobs Important",
                            Phone = "7449321907",
                            Email = "Steve.Jobs@mail.com",
                            Address = "Ipswich, UK",
                            CreatedDate = DateTime.Now
                        },
                        new Client{
                            FirstName = "Robert Albert",
                            Phone = "7449321123",
                            Email = "Robert.Albert@mail.com",
                            Address = "Manchester, UK",
                            CreatedDate = DateTime.Now
                        },
                        new Client{
                            FirstName = "Mc Donalds",
                            Phone = "929200685",
                            Email = "Mc.Don@mail.com",
                            Address = "oxford, UK",
                            CreatedDate = DateTime.Now
                        }
                  }
                  );
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        private static void LoadProducts()
        {
            try
            {


                CakeShopOrderService.BulkInsertProduct
                    (
                        new List<Product>{

                    new Product{
                        ProductName = "Cake",
                        ProductType = "Vanilla",
                        ProductPrice = 100,
                        PreparationTime = 10   ,
                        CreatedDate = DateTime.Now
                    },
                    new Product{
                        ProductName = "Cake",
                        ProductType = "Chocalate",
                        ProductPrice = 200,
                        PreparationTime = 20,
                        CreatedDate = DateTime.Now
                    },
                    new Product{
                        ProductName = "Cake",
                        ProductType = "Red Velvet",
                        ProductPrice = 300,
                        PreparationTime = 30,
                        CreatedDate = DateTime.Now
                    }
                        }
                    );
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        private static void LoadOrders()
        {
            string orderGuid1 = Guid.NewGuid().ToString();
            string orderGuid2 = Guid.NewGuid().ToString();
            string orderGuid3 = Guid.NewGuid().ToString();
            try
            {
                CakeShopOrderService.BulkInsertOrders(
                    new List<Order>
                    {
                        new Order{
                            OrderNumber = orderGuid1,
                            //OrderStatus = OrderStatus.Created,
                            ProductID = 100,
                            ClientId = 100,
                            TransportID = 100,
                            ProductQuantity = 1000,
                            RequestedDeliveryTime = DateTime.Now.AddDays(1),
                            CreatedDate = DateTime.Now
                        },
                        new Order{
                            OrderNumber = orderGuid1,
                            //OrderStatus = OrderStatus.Created,
                            ProductID = 101,
                            ClientId = 100,
                            TransportID = 100,
                            ProductQuantity = 2100,
                            RequestedDeliveryTime = DateTime.Now.AddDays(2),
                            CreatedDate = DateTime.Now
                        },
                        new Order{
                            OrderNumber = orderGuid1,
                            //OrderStatus = OrderStatus.Created,
                            ProductID = 102,
                            ClientId = 100,
                            TransportID = 100,
                            ProductQuantity = 8000,
                            RequestedDeliveryTime = DateTime.Now.AddDays(2),
                            CreatedDate = DateTime.Now
                        }
                    });
                CakeShopOrderService.BulkInsertOrders(
                    new List<Order>
                    {
                         new Order{
                            OrderNumber = orderGuid2,
                            //OrderStatus = OrderStatus.Created,
                            ProductID = 100,
                            ClientId = 101,
                            TransportID = 101,
                            ProductQuantity = 30,
                            RequestedDeliveryTime = DateTime.Now.AddDays(1),
                            CreatedDate = DateTime.Now
                        },
                         new Order{
                            OrderNumber = orderGuid2,
                            //OrderStatus = OrderStatus.Created,
                            ProductID = 102,
                            ClientId = 101,
                            TransportID = 101,
                            ProductQuantity = 30,
                            RequestedDeliveryTime = DateTime.Now.AddDays(4),
                            CreatedDate = DateTime.Now
                        }
                    });
                CakeShopOrderService.BulkInsertOrders(
                    new List<Order>
                    {

                        new Order{
                            OrderNumber = orderGuid3,
                            //OrderStatus = OrderStatus.Created,
                            ProductID = 101,
                            ClientId = 102,
                            TransportID = 102,
                            ProductQuantity = 1000,
                            RequestedDeliveryTime = DateTime.Now.AddDays(1),
                            CreatedDate = DateTime.Now
                        },
                        new Order{
                            OrderNumber = orderGuid3,
                            //OrderStatus = OrderStatus.Created,
                            ProductID = 102,
                            ClientId = 102,
                            TransportID = 102,
                            ProductQuantity = 5000,
                            RequestedDeliveryTime = DateTime.Now.AddDays(6),
                            CreatedDate = DateTime.Now
                        }
                    });

              
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }

        }
    }


}