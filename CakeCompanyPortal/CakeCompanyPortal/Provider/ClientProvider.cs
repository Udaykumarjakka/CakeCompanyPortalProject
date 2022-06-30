using CakeCompanyPortal.DataAccess;
using CakeCompanyPortal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompanyPortal.Provider
{
    public static class ClientProvider
    {
        private static UnitOfWork _db = new UnitOfWork();

        public static int AddClient(Client client)
        {

            IEnumerable<Client> _Clients = null;
            try
            {
                _Clients = _db.ClientRepository.GetAll();
                if (_Clients != null && _Clients.Count() > 0)
                {
                    var cliID = _Clients.Max(x => x.ClientId);
                    client.ClientId = Convert.ToInt16(cliID) + 1;
                }
                else
                    client.ClientId = 100;

                _db.ClientRepository.Insert(client);
                _db.Save();

            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return client.ClientId;
        }

        public static void DeleteClient(int clientId)
        {
            try
            {
                _db.ClientRepository.Delete(clientId);
                _db.Save();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void DeleteClient(Client client)
        {
            try
            {
                _db.ClientRepository.Delete(client);
                _db.Save();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }


        public static void UpdateClient(Client client)
        {
            try
            {
                _db.ClientRepository.Update(client);
                _db.Save();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static void BulkInsert(List<Client> ClinetLst)
        {
            IEnumerable<Client> _clients = null;
            int cID = 99;
            try
            {
                _clients = _db.ClientRepository.GetAll();
                if (_clients != null && _clients.Count() > 0)
                {

                    var clientID = _clients.Max(x => x.ClientId);
                    cID = Convert.ToInt32(clientID);
                }

                foreach (Client clnt in ClinetLst)
                {
                    clnt.HasCreditLimit = UpdateCreditLimitStatus(clnt.FirstName);
                    clnt.IsSuccessful = "Yes";
                    clnt.ClientId = ++cID;
                    _db.ClientRepository.Insert(clnt);
                    _db.Save();
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
        }

        public static IEnumerable<Client> GetAllClients()
        {
            IEnumerable<Client> _Clients = null;
            try
            {
                _Clients = _db.ClientRepository.GetAll();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return _Clients;
        }

        public static string UpdateCreditLimitStatus(string _FirstName)
        {
            try
            {
                if (!string.IsNullOrEmpty(_FirstName))
                {
                    if (_FirstName.ToUpper().Contains("IMPORTANT"))
                    {
                        return "No";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return "Yes";
        }

        public static void DeleteAll()
        {
            try
            {
                _db.ClientRepository.DeleteAll();
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
