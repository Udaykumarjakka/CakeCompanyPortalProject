using CakeCompanyPortal.Helper;
using CakeCompanyPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeCompanyPortal.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            try
            {
                return View(CakeShopOrderService.GetAllOrders());
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            

        }

        public ActionResult Details(string orderNumber)
        {
            IEnumerable<Order> orders = null;
            try
            {
                if (string.IsNullOrEmpty(orderNumber))
                    return View();

                orders = CakeShopOrderService.GetAllOrders().Where(x => x.OrderNumber.Equals(orderNumber)).ToList();
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }

            return View(orders);
        }
    }
}
