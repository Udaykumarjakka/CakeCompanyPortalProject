using CakeCompanyPortal.Helper;
using CakeCompanyPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeCompanyPortal.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            try
            {
                PreLoadCakeDetails.LoadCakeTables();

                return View(CakeShopOrderService.GetAllInvoices());
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            return View();
        }
    }
}