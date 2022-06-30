using CakeCompanyPortal.DataAccess;
using CakeCompanyPortal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.Provider
{
    public static class TransportProvider
    {
        private static UnitOfWork _db = new UnitOfWork();

        public static IEnumerable<Transport> getAllTransport()
        {
            IEnumerable<Transport> trans = null;
            try
            {
                trans = _db.TransportRepository.GetAll();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return trans;
        }

        public static void BulkInsert(IList<Transport> transportsLst)
        {
            IEnumerable<Transport> _Transport = null;
            int tID = 99;
            try
            {

                _Transport = _db.TransportRepository.GetAll();
                if (_Transport != null && _Transport.Count() > 0)
                {
                    var transID = _Transport.Max(x => x.TransportID);
                    tID = Convert.ToInt32(transID);
                }

                foreach (Transport trns in transportsLst)
                {

                    trns.TransportID = ++tID;
                    _db.TransportRepository.Insert(trns);
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
                _db.TransportRepository.DeleteAll();
                _db.Save();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static Transport GetTransportByQuantity(int? quantity)
        {
            Transport minTrans = null;
            try
            {
                var allTrans = _db.TransportRepository.GetAll().OrderByDescending(X => X.QuantityAllowed);
                if (allTrans != null && allTrans.Count() > 0)
                {
                    foreach (Transport trns in allTrans)
                    {
                        if (trns.QuantityAllowed < quantity)
                        {
                            return trns;
                        }
                        minTrans = trns;
                    }
                    return minTrans;
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return null;
        }
    }
}
