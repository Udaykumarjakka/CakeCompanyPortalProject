using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CakeCompanyPortal;
using CakeCompanyPortal.DataAccess;
using CakeCompanyPortal.Helper;
using CakeCompanyPortal.Service;

namespace CakeCompanyPortal.Controllers
{
    public class TransportsController : Controller
    {
        private CakeDbContext db = new CakeDbContext();

        // GET: Transports
        public ActionResult Index()
        {
            try
            {
                return View(CakeShopOrderService.GetAllTransports());
            }
            catch (Exception ex)
            {
                Log.Create(ex);
                throw;
            }
            
        }

        // GET: Transports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transport transport = db.Transports.Find(id);
            if (transport == null)
            {
                return HttpNotFound();
            }
            return View(transport);
        }

        // GET: Transports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransportID,TransportType,QuantityAllowed")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                db.Transports.Add(transport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transport);
        }

        // GET: Transports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transport transport = db.Transports.Find(id);
            if (transport == null)
            {
                return HttpNotFound();
            }
            return View(transport);
        }

        // POST: Transports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransportID,TransportType,QuantityAllowed")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transport);
        }

        // GET: Transports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transport transport = db.Transports.Find(id);
            if (transport == null)
            {
                return HttpNotFound();
            }
            return View(transport);
        }

        // POST: Transports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transport transport = db.Transports.Find(id);
            db.Transports.Remove(transport);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
