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
    
    public class GoodsDonationstblsController : Controller
    {
        private DisasterAlleviationFoundationDBEntities db = new DisasterAlleviationFoundationDBEntities();

        // GET: GoodsDonationstbls
        public ActionResult Index()
        {
            return View(db.GoodsDonationstbls.ToList());
        }

        // GET: GoodsDonationstbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoodsDonationstbl goodsDonationstbl = db.GoodsDonationstbls.Find(id);
            if (goodsDonationstbl == null)
            {
                return HttpNotFound();
            }
            return View(goodsDonationstbl);
        }

        // GET: GoodsDonationstbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GoodsDonationstbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GoodsID,Date,NumberOfItems,Category,Donar")] GoodsDonationstbl goodsDonationstbl)
        {
            if (ModelState.IsValid)
            {
                db.GoodsDonationstbls.Add(goodsDonationstbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goodsDonationstbl);
        }

        // GET: GoodsDonationstbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoodsDonationstbl goodsDonationstbl = db.GoodsDonationstbls.Find(id);
            if (goodsDonationstbl == null)
            {
                return HttpNotFound();
            }
            return View(goodsDonationstbl);
        }

        // POST: GoodsDonationstbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GoodsID,Date,NumberOfItems,Category,Donar")] GoodsDonationstbl goodsDonationstbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goodsDonationstbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goodsDonationstbl);
        }

        // GET: GoodsDonationstbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoodsDonationstbl goodsDonationstbl = db.GoodsDonationstbls.Find(id);
            if (goodsDonationstbl == null)
            {
                return HttpNotFound();
            }
            return View(goodsDonationstbl);
        }

        // POST: GoodsDonationstbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GoodsDonationstbl goodsDonationstbl = db.GoodsDonationstbls.Find(id);
            db.GoodsDonationstbls.Remove(goodsDonationstbl);
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
