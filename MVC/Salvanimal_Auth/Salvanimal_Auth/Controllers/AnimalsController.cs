using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Salvanimal_Auth.Models;
using System.IO;
using System.Diagnostics;

namespace Salvanimal_Auth.Controllers
{
    public class AnimalsController : Controller
    {
        private AnimalsEntities db = new AnimalsEntities();

        // GET: Animals
        public ActionResult ManageAnimals()
        {
            var animals = db.Animals.Include(a => a.Especies);
            return View(animals.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ManageAnimals(string searchName)
        {
            return View(db.Animals.Where(n => n.Nome.Contains(searchName)).Include(a => a.Especies).ToList());
        }

        // GET: Animals/Details/5
        public ActionResult Animals()
        {
            var animals = db.Animals.Include(a => a.Especies);
            return View(animals.ToList());
        }

        
        
        public ActionResult CheckAnimals(string Especie)
        {
            //Debug.WriteLine(Especie);
            var animals = db.Animals.Where(a => a.Especies.NomeEspecie == Especie).Include(a => a.Especies);
            return View("Animals", animals.ToList());
        }

        // GET: Animals/Create
        public ActionResult Create()
        {
            ViewBag.Especie = new SelectList(db.Especies, "Id", "NomeEspecie");
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Especie,Raca,Nome,Idade,Observacoes,Imagem")] Animals animals, HttpPostedFileBase imageUpload)
        {
            //Debug.WriteLine(imageUpload.FileName);
            if (imageUpload != null)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(imageUpload.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/Resources/Images"), fileName);
                animals.Imagem = "/Resources/Images/" + imageUpload.FileName;
                imageUpload.SaveAs(path);
            }
            else
            {
                animals.Imagem = "";
            }

            if (ModelState.IsValid)
            {
                db.Animals.Add(animals);
                db.SaveChanges();
                return RedirectToAction("ManageAnimals");
            }

            ViewBag.Especie = new SelectList(db.Especies, "Id", "NomeEspecie", animals.Especie);
            return RedirectToAction("ManageAnimals");
        }

        // GET: Animals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animals animals = db.Animals.Find(id);
            if (animals == null)
            {
                return HttpNotFound();
            }
            ViewBag.Especie = new SelectList(db.Especies, "Id", "NomeEspecie", animals.Especie);
            return View(animals);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Especie,Raca,Nome,Idade,Observacoes,Imagem")] Animals animals)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animals).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Especie = new SelectList(db.Especies, "Id", "NomeEspecie", animals.Especie);
            return View(animals);
        }

        // GET: Animals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animals animals = db.Animals.Find(id);
            if (animals == null)
            {
                return HttpNotFound();
            }
            return View(animals);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animals animals = db.Animals.Find(id);
            db.Animals.Remove(animals);
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
