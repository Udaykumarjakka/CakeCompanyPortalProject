using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CakeCompanyPortal.Provider;
using CakeCompanyPortal;
using CakeCompanyPortal.DataAccess;
using System.Configuration;
using System.IO;
using System;
using System.Data.Entity;
using TypeMock.ArrangeActAssert.Suggest;
using TypeMock.ArrangeActAssert;
using System.Linq;
using CakeCompanyPortal.Helper;

namespace UnitTestCakeCompanyPortal.Provider
{
    [Isolated()]
    [TestClass()]
    public class ShipmentProviderTests
    {
        #region Unit Tests for BulkInsert

        [TestMethod]
        public void BulkInsert_TestWithInvoiceList()
        {
            //Arrange
            var invoice = new Invoice();
            invoice.ClientID = 0;
            invoice.TransportID = 0;
            invoice.InvoiceID = 78;
            var invoice1 = new Invoice();
            invoice1.ClientID = 99;
            invoice1.InvoiceID = 69;
            invoice1.TransportID = 0;
            var invoiceLst = new List<Invoice> { invoice, invoice1 };
            var fakeUnitOfWork = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => fakeUnitOfWork.InvoiceRepository.GetAll()).WillReturn(invoiceLst);

            //Act
            ShipmentProvider.BulkInsert(invoiceLst);

            //Assert
            Assert.AreEqual(79, invoiceLst[0].InvoiceID);
            Assert.AreEqual(80, invoiceLst[1].InvoiceID);
        }

        [TestMethod]
        public void BulkInsert_TestWithInvoiceListEmpty()
        {
            //Arrange
            var invoice = new Invoice();
            invoice.ClientID = 0;
            invoice.TransportID = 0;
            invoice.InvoiceID = 78;
            var invoice1 = new Invoice();
            invoice1.ClientID = 99;
            invoice1.InvoiceID = 69;
            invoice1.TransportID = 0;
            var invoiceLst = new List<Invoice> {};
            var fakeUnitOfWork = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => fakeUnitOfWork.InvoiceRepository.GetAll()).WillReturn(invoiceLst);

            //Act
            ShipmentProvider.BulkInsert(invoiceLst);

            //Assert
        }

        [TestMethod]
        public void BulkInsert_TestWithInvoiceListNull()
        {
            //Arrange
            var invoice = new Invoice();
            invoice.ClientID = 0;
            invoice.TransportID = 0;
            var invoice1 = new Invoice();
            invoice1.ClientID = 99;
            invoice1.InvoiceID = 0;
            invoice1.TransportID = 0;
            var invoiceLst = new List<Invoice> { invoice, invoice1 };
            var fakeUnitOfWork = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => fakeUnitOfWork.InvoiceRepository.GetAll()).WillReturn(null);

            //Act
            ShipmentProvider.BulkInsert(invoiceLst);

            //Assert
            Assert.AreEqual(100, invoiceLst[0].InvoiceID);
            Assert.AreEqual(101, invoiceLst[1].InvoiceID);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BulkInsert_TestCatchBlock()
        {
            //Arrange
            var invoice = new Invoice();
            invoice.ClientID = 0;
            invoice.TransportID = 0;
            var invoice1 = new Invoice();
            invoice1.ClientID = 99;
            invoice1.InvoiceID = 0;
            invoice1.TransportID = 0;
            var invoiceLst = new List<Invoice> { invoice, invoice1 };
            var fakeUnitOfWork = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => fakeUnitOfWork.InvoiceRepository.GetAll()).WillThrow(new NullReferenceException());
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            //Act
            ShipmentProvider.BulkInsert(invoiceLst);

            //Assert
        }

        #endregion

        #region Unit Tests for CreateInvoice

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreateInvoice_Test_ThrowsException()
        {
            // arrange
            var fakeUnitOfWork = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => fakeUnitOfWork.InvoiceRepository.GetAll()).WillThrow(new NullReferenceException());
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            // act
            ShipmentProvider.CreateInvoice(null);

            // assert
        }

        [TestMethod]
        public void CreateInvoice_Test_ReturnsInvoiceIDIs1()
        {
            // arrange
            var invoice = new Invoice();
            invoice.InvoiceID = 200;
            var handleGenericRepository_1 = Isolate.Fake.AllInstances<GenericRepository<Invoice>>();
            var invoiceList = new List<Invoice> {invoice};
            Isolate.WhenCalled(() => handleGenericRepository_1.GetAll()).WillReturn(invoiceList);
             
            // act
            ShipmentProvider.CreateInvoice(invoice);
             
            // assert
            // side affects on invoice
            Assert.AreEqual(201, invoice.InvoiceID);
        }

        [TestMethod]
        public void CreateInvoice_Test_ReturnsInvoiceIDIs100()
        {
            // arrange
            var invoice = new Invoice();
            var invoiceList = new List<Invoice> { };
            var handleGenericRepository_1 = Isolate.Fake.AllInstances<GenericRepository<Invoice>>();
            Isolate.WhenCalled(() => handleGenericRepository_1.GetAll()).WillReturn(invoiceList);
        
             
            // act
            ShipmentProvider.CreateInvoice(invoice);
             
            // assert
            // side affects on invoice
            Assert.AreEqual(100, invoice.InvoiceID);
        }

        #endregion

        #region Unit Tests for GetAllShipmentDetails

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]       
        public void GetAllShipmentDetails_Test_throwsException()
        {
            // arrange
            UnitOfWork _db = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => _db.InvoiceRepository.GetAll()).WillThrow(new NullReferenceException());
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            // act
            var result = ShipmentProvider.GetAllShipmentDetails();

            // assert
        }

        [TestMethod]
        public void GetAllShipmentDetails_Test_ReturnsFakeDbSet_1()
        {
            // Arrange
            UnitOfWork _db = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => _db.InvoiceRepository.GetAll()).WillThrow(null);
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            // act
            var result = ShipmentProvider.GetAllShipmentDetails();
             
            // assert
            Assert.AreSame(null, result);
        }

        #endregion

        #region UnitTests for DeleteAll

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteAll_TestsCatchBlock_NOAsserts()
        {
            //Arrange
            UnitOfWork _db = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => _db.InvoiceRepository.DeleteAll()).WillThrow(new NullReferenceException());
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            //Act
            ShipmentProvider.DeleteAll();
        }

        [TestMethod]
        public void DeleteAll_Test_NoAsserts()
        {
            //Arrange
            UnitOfWork _db = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => _db.InvoiceRepository.DeleteAll()).IgnoreCall();
            Isolate.WhenCalled(() => _db.Save()).IgnoreCall();
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            //Act
            ShipmentProvider.DeleteAll();
        }

        #endregion

        #region Setup
        [TestInitialize]
        public void Setup_RunBeforeEachTest()
        {
            TestUtil.ResetAllStatics();
            TestUtil.AssertRunningInSandbox();
        }
        #endregion

    }
}