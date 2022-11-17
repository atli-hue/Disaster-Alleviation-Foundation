using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DisasterAllevitionFoundationproject.Models;

namespace DisasterAllevitionFoundationproject.Controllers
{
    
    public class MonetaryDonationsTblsController : Controller
    {
        private DisasterAlleviationFoundationDBEntities db = new DisasterAlleviationFoundationDBEntities();

        // GET: MonetaryDonationsTbls
        public ActionResult Index()
        {
            return View(db.MonetaryDonationsTbls.ToList());
        }

        // GET: MonetaryDonationsTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonetaryDonationsTbl monetaryDonationsTbl = db.MonetaryDonationsTbls.Find(id);
            if (monetaryDonationsTbl == null)
            {
                return HttpNotFound();
            }
            return View(monetaryDonationsTbl);
        }

        // GET: MonetaryDonationsTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MonetaryDonationsTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MonetaryID,date,Amount,Donar")] MonetaryDonationsTbl monetaryDonationsTbl)
        {
            if (ModelState.IsValid)
            {
                db.MonetaryDonationsTbls.Add(monetaryDonationsTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(monetaryDonationsTbl);
        }

        // GET: MonetaryDonationsTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonetaryDonationsTbl monetaryDonationsTbl = db.MonetaryDonationsTbls.Find(id);
            if (monetaryDonationsTbl == null)
            {
                return HttpNotFound();
            }
            return View(monetaryDonationsTbl);
        }

        // POST: MonetaryDonationsTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MonetaryID,date,Amount,Donar")] MonetaryDonationsTbl monetaryDonationsTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monetaryDonationsTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(monetaryDonationsTbl);
        }

        // GET: MonetaryDonationsTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonetaryDonationsTbl monetaryDonationsTbl = db.MonetaryDonationsTbls.Find(id);
            if (monetaryDonationsTbl == null)
            {
                return HttpNotFound();
            }
            return View(monetaryDonationsTbl);
        }

        // POST: MonetaryDonationsTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonetaryDonationsTbl monetaryDonationsTbl = db.MonetaryDonationsTbls.Find(id);
            db.MonetaryDonationsTbls.Remove(monetaryDonationsTbl);
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
