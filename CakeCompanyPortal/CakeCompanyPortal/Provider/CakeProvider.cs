using CakeCompanyPortal.DataAccess;
using CakeCompanyPortal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.Provider
{
    public static class CakeProvider
    {
        private static UnitOfWork _db = new UnitOfWork();

        public static DateTime GetPreparationTime(Order order)
        {
            DateTime _estimatedDeliverTime = new DateTime();
            double estimateTime;
            try
            {
                if (order != null && order.ProductID != null)
                {
                    var Product = ProductProvider.GetProduct(order.ProductID);

                    if (Product != null)
                    {
                        if (Product.PreparationTime != null && !string.IsNullOrEmpty(Product.PreparationTime.ToString()))
                        {
                            estimateTime = Product.PreparationTime.HasValue ? double.Parse(Product.PreparationTime.ToString()) : 0;
                            return DateTime.Now.Add(TimeSpan.FromMinutes(estimateTime));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return _estimatedDeliverTime;
        }

        public static IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> _Products = null;
            try
            {
                _Products = _db.ProductRepository.GetAll();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return _Products;
        }
    }
}
