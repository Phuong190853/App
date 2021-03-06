using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asm.Models.Entity;

namespace Asm.Areas.Admin.Controllers
{
    public class AccountsController : Controller
    {
        private Manage_Context db = new Manage_Context();

        public ActionResult Welcome()
        {
            return View();
        }
        // GET: Admin/Accounts
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.Role1);
            return View(accounts.ToList());
        }

        // GET: Admin/Accounts/Details/5

        // GET: Admin/Accounts/Create
        [HttpPost]
        public ActionResult Search(string searchKey)
        {
            var a = db.Accounts.Where(t => t.Name.Contains(searchKey) || searchKey == null).ToList();
            return View(a);
        }

        [HttpPost]
        public ActionResult SearchRole(string role)
        {
            var a = db.Accounts.Where(t => t.Role.Contains(role) || role == null).ToList();
            return View(a);
        }
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles, "RoleName", "RoleName");
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccID,Name,Email,Password,Role,DoB,PhoneNo,ToeicScore")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role = new SelectList(db.Roles, "RoleName", "RoleName", account.Role);
            return View(account);
        }

        // GET: Admin/Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role = new SelectList(db.Roles, "RoleName", "RoleName", account.Role);
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccID,Name,Email,Password,Role,DoB,PhoneNo,ToeicScore")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role = new SelectList(db.Roles, "RoleName", "RoleName", account.Role);
            return View(account);
        }

        // GET: Admin/Accounts/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var model = db.Accounts.Where(x => x.AccID == id).FirstOrDefault();
            var model1 = db.Courses.Where(x => x.TrainerID == id).FirstOrDefault();
            model1.TrainerID = null;
            if (model != null)
            {
                db.Accounts.Remove(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return new HttpNotFoundResult();
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
