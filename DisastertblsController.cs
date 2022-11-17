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
    public class DisastertblsController : Controller
    {
        private DisasterAlleviationFoundationDBEntities db = new DisasterAlleviationFoundationDBEntities();

        // GET: Disastertbls
        public ActionResult Index()
        {
            return View(db.Disastertbls.ToList());
        }

        // GET: Disastertbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disastertbl disastertbl = db.Disastertbls.Find(id);
            if (disastertbl == null)
            {
                return HttpNotFound();
            }
            return View(disastertbl);
        }

        // GET: Disastertbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Disastertbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DisasterID,StartDate,EndDate,Location,Description,AidType")] Disastertbl disastertbl)
        {
            if (ModelState.IsValid)
            {
                db.Disastertbls.Add(disastertbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(disastertbl);
        }

        // GET: Disastertbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disastertbl disastertbl = db.Disastertbls.Find(id);
            if (disastertbl == null)
            {
                return HttpNotFound();
            }
            return View(disastertbl);
        }

        // POST: Disastertbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisasterID,StartDate,EndDate,Location,Description,AidType")] Disastertbl disastertbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disastertbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disastertbl);
        }

        // GET: Disastertbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disastertbl disastertbl = db.Disastertbls.Find(id);
            if (disastertbl == null)
            {
                return HttpNotFound();
            }
            return View(disastertbl);
        }

        // POST: Disastertbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disastertbl disastertbl = db.Disastertbls.Find(id);
            db.Disastertbls.Remove(disastertbl);
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
