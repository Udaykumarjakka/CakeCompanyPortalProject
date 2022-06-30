using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeCompanyPortal.DataAccess;
using CakeCompanyPortal.Helper;

namespace CakeCompanyPortal.Provider
{
    public static class ProductProvider
    {
        private static UnitOfWork _db = new UnitOfWork();

        public static void AddProduct(Product product)
        {
            IEnumerable<Product> _products = null;
            try
            {
                _products = _db.ProductRepository.GetAll();
                if (_products != null && _products.Count() > 0)
                {
                    var prodID = _products.Max(x => x.ProductID);
                    product.ProductID = Convert.ToInt16(prodID) + 1;
                }
                else
                    product.ProductID = 100;
                _db.ProductRepository.Insert(product);
                _db.Save();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static Product GetProduct(int? productId)
        {
            try
            {
                return _db.ProductRepository.GetById(productId);
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static IEnumerable<Product> GetAllProduct()
        {
            try
            {
                return _db.ProductRepository.GetAll();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void DeleteProduct(int productId)
        {
            try
            {
                _db.ProductRepository.Delete(productId);
                _db.Save();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void DeleteProduct(Product product)
        {
            try
            {
                _db.ProductRepository.Delete(product);
                _db.Save();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void UpdateProduct(Product Product)
        {
            try
            {
                _db.ProductRepository.Update(Product);
                _db.Save();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void BulkInsert(List<Product> productLst)
        {
            IEnumerable<Product> _products = null;
            int pID = 99;
            try
            {
                _products = _db.ProductRepository.GetAll();
                if (_products != null && _products.Count() > 0)
                {
                    var prodID = _products.Max(x => x.ProductID);
                    pID = Convert.ToInt32(prodID);
                }

                foreach (Product Prd in productLst)
                {
                    Prd.ProductID = ++pID;
                    _db.ProductRepository.Insert(Prd);
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
                _db.ProductRepository.DeleteAll();
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
