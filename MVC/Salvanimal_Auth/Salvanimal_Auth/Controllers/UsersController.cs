using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Salvanimal_Auth.Models;
using System.Web.Security;

namespace Salvanimal_Auth.Controllers
{
    public class UsersController : Controller
    {
        private UserEntities db = new UserEntities();
        private QuotasEntities dbq = new QuotasEntities();
        // GET: Users
        public ActionResult ManageUsers()
        {
            var users = dbq.AspNetUsers.Where(u => u.UserName.Contains("Admin").Equals(false));
            return View(users.ToList());
        }
        [HttpPost]
        public ActionResult ManageUsers(string searchName)
        {
            var users = dbq.AspNetUsers.Where(u => u.UserName.Contains("Admin").Equals(false)).Where(u => u.UserName.ToLower().Contains(searchName.ToLower()));
            return View(users.ToList());
        }


        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,PasswordHash,PhoneNumber,UserName")] AspNetUsers aspNetUsers)
        {
            aspNetUsers.EmailConfirmed = false;
            aspNetUsers.LockoutEnabled = false;
            aspNetUsers.LockoutEndDateUtc = null;
            aspNetUsers.PhoneNumberConfirmed = false;
            aspNetUsers.SecurityStamp = null;
            aspNetUsers.TwoFactorEnabled = false;

            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUsers);
                db.SaveChanges();
                Roles.AddUserToRole(aspNetUsers.UserName, "Socio");
                return RedirectToAction("Index");
            }

            return View(aspNetUsers);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUsers);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
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
