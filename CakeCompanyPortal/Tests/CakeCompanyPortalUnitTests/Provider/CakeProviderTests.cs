using Microsoft.VisualStudio.TestTools.UnitTesting;
using CakeCompanyPortal;
using CakeCompanyPortal.Provider;
using TypeMock.ArrangeActAssert.Suggest;
using TypeMock.ArrangeActAssert;
using System.Linq;
using System;
using CakeCompanyPortal.Helper;
using CakeCompanyPortal.DataAccess;

namespace UnitTestCakeCompanyPortal.Provider
{
    [Isolated()]
    [TestClass()]
    public class CakeProviderTests
    {
        #region Unit Tests for GetPreparationTime
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetPreparationTime_TestCatchBlock_NoAsserts()
        {
            // arrange
            var order = new Order();
            order.ProductID = 100;
            Isolate.WhenCalled(() => ProductProvider.GetProduct(null)).WillThrow(new NullReferenceException());
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();
             
            // act
            var result = CakeProvider.GetPreparationTime(order);

            //Assert
           
        }

        [TestMethod]
        public void GetPreparationTime_TestWithOrderNull_NoAsserts()
        {
            // arrange
            var date = new DateTime();
            var objProduct = new Product();
            objProduct.PreparationTime = 10;
            Isolate.WhenCalled(() => ProductProvider.GetProduct(null)).WillReturn(objProduct);
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            // act
            var result = CakeProvider.GetPreparationTime(null);

            //Assert
            Assert.AreEqual(date, result);
        }

        [TestMethod]
        public void GetPreparationTime_TestWithProductIdNull_NoAsserts()
        {
            // arrange
            var date = new DateTime();
            var order = new Order();
            order.ProductID = null;
            var objProduct = new Product();
            objProduct.PreparationTime = 10;
            Isolate.WhenCalled(() => ProductProvider.GetProduct(null)).WillReturn(objProduct);
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            // act
            var result = CakeProvider.GetPreparationTime(order);

            //Assert
            Assert.AreEqual(date, result);
        }

        [TestMethod]
        public void GetPreparationTime_TestWithValidOrderAndProductNull_NoAsserts()
        {
            // arrange
            var date = new DateTime();
            var order = new Order();
            order.ProductID = 100;
            Isolate.WhenCalled(() => ProductProvider.GetProduct(null)).WillReturn(null);
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            // act
            var result = CakeProvider.GetPreparationTime(order);

            //Assert
            Assert.AreEqual(date, result);
        }

        [TestMethod]
        public void GetPreparationTime_TestWithPreparationtimeNull_NoAsserts()
        {
            // arrange
            var date = new DateTime();
            var order = new Order();
            order.ProductID = 100;
            var objProduct = new Product();
            objProduct.PreparationTime = null;
            Isolate.WhenCalled(() => ProductProvider.GetProduct(null)).WillReturn(objProduct);
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            // act
            var result = CakeProvider.GetPreparationTime(order);

            //Assert
            Assert.AreEqual(date, result);
        }

        [TestMethod]
        public void GetPreparationTime_TestWithValidOrderAndProduct_NoAsserts()
        {
            // arrange
            var expret = DateTime.Now.Add(TimeSpan.FromMinutes(10));
            var order = new Order();
            order.ProductID = 100;
            var objProduct = new Product();
            objProduct.PreparationTime = 10;
            Isolate.WhenCalled(() => ProductProvider.GetProduct(null)).WillReturn(objProduct);
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            // act
            var result = CakeProvider.GetPreparationTime(order);

            //Assert
            Assert.AreEqual(expret.DayOfYear, result.DayOfYear);
        }

        #endregion

        #region Unit Tests for GetAllProducts


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetAllProducts_TestCatchBlock_ReturnsNull()
        {
            // arrange
            UnitOfWork _db = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => _db.ProductRepository.GetAll()).WillThrow(new NullReferenceException());
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            // act
            var result = CakeProvider.GetAllProducts();

            //Assert

        }

        [TestMethod]
        public void GetAllProducts_Test_ReturnsProducts()
        {
            // Arrange
            UnitOfWork _db = Isolate.Fake.AllInstances<UnitOfWork>();
            Isolate.WhenCalled(() => _db.ProductRepository.GetAll()).WillReturn(null);
            Isolate.WhenCalled(() => Log.Create("")).IgnoreCall();

            // Act
            var result = CakeProvider.GetAllProducts();

            //Assert
            Assert.AreEqual(null, result);
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