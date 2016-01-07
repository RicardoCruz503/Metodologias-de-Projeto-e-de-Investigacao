using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Salvanimal_Auth.Models;

namespace VerticalculoWebsite.Controllers
{
    public class NoticiasController : Controller
    {
        private NoticiasEntities db = new NoticiasEntities();

        //
        // GET: /Default1/

        public ActionResult ManageNoticias()
        {
            return View(db.NoticiasEntity.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ManageNoticias(string searchName)
        {
            return View(db.NoticiasEntity.Where(n => n.Titulo.Contains(searchName)).ToList());
        }

        public ActionResult Noticias(int index = 1)
        {
            ViewBag.Index = index;
            if (db.NoticiasEntity.ToArray().Reverse().ToArray().Length == 0)
            {
                return View();
            }
            return View(db.NoticiasEntity.ToArray().Reverse().ToArray()[index - 1]);
        }



        //
        // GET: /Default1/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoticiasEntity noticiasentity, DateTime data)
        {
            if (ModelState.IsValid)
            {
                noticiasentity.data = data;
                noticiasentity.CorpoNoticia = noticiasentity.CorpoNoticia.Replace("*b*", "<b>").Replace("*/b*", "</b>").Replace("*u*", "<u>").Replace("*/u*", "</u>").Replace("*p*", "<p>").Replace("*/p*", "</p>");
                db.NoticiasEntity.Add(noticiasentity);
                db.SaveChanges();
                return RedirectToAction("ManageNoticias");
            }

            return RedirectToAction("ManageNoticias");
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 1)
        {
            NoticiasEntity noticiasentity = db.NoticiasEntity.Find(id);
            if (noticiasentity == null)
            {
                return HttpNotFound();
            }
            return View(noticiasentity);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NoticiasEntity noticiasentity)
        {

            if (ModelState.IsValid)
            {
                db.Entry(noticiasentity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageNoticias");
            }
            return View(noticiasentity);
        }

        //
        // GET: /Default1/Delete/5
        public ActionResult Delete(int id = 0)
        {
            NoticiasEntity noticiasentity = db.NoticiasEntity.Find(id);
            if (noticiasentity == null)
            {
                return HttpNotFound();
            }
            return View(noticiasentity);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NoticiasEntity noticiasentity = db.NoticiasEntity.Find(id);
            db.NoticiasEntity.Remove(noticiasentity);
            db.SaveChanges();
            return RedirectToAction("ManageNoticias");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}