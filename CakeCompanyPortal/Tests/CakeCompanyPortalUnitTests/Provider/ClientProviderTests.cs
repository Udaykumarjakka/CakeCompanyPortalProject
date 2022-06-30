using CakeCompanyPortal;
using CakeCompanyPortal.DataAccess;
using CakeCompanyPortal.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeMock.ArrangeActAssert;

namespace CurrentUnitTestProject.Provider
{
    [Isolated]
    [TestClass()]
    public class ClientProviderTests
    {
        #region UnitTests for AddClient

        [TestMethod]
        public void AddClient_Test_ThrowsException()
        {
            //Arrange
            var client = new Client();
            UnitOfWork _db = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => _db.ClientRepository.GetAll()).WillReturn(null);

            //Act
            var result = ClientProvider.AddClient(client);

            //assert
            Assert.AreEqual(100, result);
        }

        #endregion
    }
}
