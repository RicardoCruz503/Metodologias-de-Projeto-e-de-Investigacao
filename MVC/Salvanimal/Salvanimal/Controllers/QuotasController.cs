using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Salvanimal.Models;

namespace Salvanimal.Controllers
{
    public class QuotasController : Controller
    {
        private QuotasContext db = new QuotasContext();

        // GET: Quotas
        public ActionResult Index()
        {
            return View(db.NoticiasProfiles.ToList());
        }

        // GET: Quotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotasEntity quotasEntity = db.NoticiasProfiles.Find(id);
            if (quotasEntity == null)
            {
                return HttpNotFound();
            }
            return View(quotasEntity);
        }

        // GET: Quotas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuotaId,Descrição,Valor,data")] QuotasEntity quotasEntity)
        {
            if (ModelState.IsValid)
            {
                db.NoticiasProfiles.Add(quotasEntity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quotasEntity);
        }

        // GET: Quotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotasEntity quotasEntity = db.NoticiasProfiles.Find(id);
            if (quotasEntity == null)
            {
                return HttpNotFound();
            }
            return View(quotasEntity);
        }

        // POST: Quotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuotaId,Descrição,Valor,data")] QuotasEntity quotasEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quotasEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quotasEntity);
        }

        // GET: Quotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotasEntity quotasEntity = db.NoticiasProfiles.Find(id);
            if (quotasEntity == null)
            {
                return HttpNotFound();
            }
            return View(quotasEntity);
        }

        // POST: Quotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuotasEntity quotasEntity = db.NoticiasProfiles.Find(id);
            db.NoticiasProfiles.Remove(quotasEntity);
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
