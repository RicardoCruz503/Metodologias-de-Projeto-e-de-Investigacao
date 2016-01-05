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
    public class EspeciesController : Controller
    {
        private EspeciesContext db = new EspeciesContext();

        // GET: Especies
        public ActionResult Index()
        {
            return View(db.NoticiasProfiles.ToList());
        }

        // GET: Especies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspeciesEntity especiesEntity = db.NoticiasProfiles.Find(id);
            if (especiesEntity == null)
            {
                return HttpNotFound();
            }
            return View(especiesEntity);
        }

        // GET: Especies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Especies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EspecieId,NomeEspecie")] EspeciesEntity especiesEntity)
        {
            if (ModelState.IsValid)
            {
                db.NoticiasProfiles.Add(especiesEntity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(especiesEntity);
        }

        // GET: Especies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspeciesEntity especiesEntity = db.NoticiasProfiles.Find(id);
            if (especiesEntity == null)
            {
                return HttpNotFound();
            }
            return View(especiesEntity);
        }

        // POST: Especies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EspecieId,NomeEspecie")] EspeciesEntity especiesEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especiesEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especiesEntity);
        }

        // GET: Especies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspeciesEntity especiesEntity = db.NoticiasProfiles.Find(id);
            if (especiesEntity == null)
            {
                return HttpNotFound();
            }
            return View(especiesEntity);
        }

        // POST: Especies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EspeciesEntity especiesEntity = db.NoticiasProfiles.Find(id);
            db.NoticiasProfiles.Remove(especiesEntity);
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
