using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Salvanimal_Auth.Models;

namespace Salvanimal_Auth.Controllers
{
    public class QuotasController : Controller
    {
        private QuotasEntities db = new QuotasEntities();

        // GET: Quotas
        public ActionResult ManageQuotas()
        {
            var quotas = db.Quotas.Include(q => q.AspNetUsers);
            return View(quotas.ToList());
        }

        public ActionResult CheckQuotas(string Id)
        {
            
            var quotas = db.Quotas.Where(q => q.AspNetUsers.Id.Equals(Id)).Include(q => q.AspNetUsers);
            return View("ManageQuotas", quotas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageQuotas(string searchName)
        {
            var quotas = db.Quotas.Where(q => q.AspNetUsers.UserName.ToLower().Contains(searchName.ToLower())).Include(q => q.AspNetUsers);
            return View(quotas.ToList());
        }

        // GET: Quotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotas quotas = db.Quotas.Find(id);
            if (quotas == null)
            {
                return HttpNotFound();
            }
            return View(quotas);
        }

        // GET: Quotas/Create
        public ActionResult Create()
        {
            ViewBag.Socio = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Quotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Socio,Descricao,Valor,Data")] Quotas quotas)
        {
            if (ModelState.IsValid)
            {
                db.Quotas.Add(quotas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Socio = new SelectList(db.AspNetUsers, "Id", "Email", quotas.Socio);
            return View(quotas);
        }

        // GET: Quotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotas quotas = db.Quotas.Find(id);
            if (quotas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Socio = new SelectList(db.AspNetUsers, "Id", "Email", quotas.Socio);
            return View(quotas);
        }

        // POST: Quotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Socio,Descricao,Valor,Data")] Quotas quotas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quotas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Socio = new SelectList(db.AspNetUsers, "Id", "Email", quotas.Socio);
            return View(quotas);
        }

        // GET: Quotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotas quotas = db.Quotas.Find(id);
            if (quotas == null)
            {
                return HttpNotFound();
            }
            return View(quotas);
        }

        // POST: Quotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quotas quotas = db.Quotas.Find(id);
            db.Quotas.Remove(quotas);
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
