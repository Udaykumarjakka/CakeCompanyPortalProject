using CakeCompanyPortal.Helper;
using CakeCompanyPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeCompanyPortal.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            try
            {
                return View(CakeShopOrderService.GetAllProducts());
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