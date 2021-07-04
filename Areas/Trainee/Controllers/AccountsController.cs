using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Asm.Models.Entity;

namespace Asm.Areas.Trainee.Controllers
{
    public class AccountsController : Controller
    {
        private Manages_Context db = new Manages_Context();
        //private Model1 db = new Model1();

        public ActionResult Welcome()
        {
            return View();
        }

        // GET: Trainee/Accounts
        public ActionResult Index()
        {
            //var accounts = db.Accounts.Include(a => a.Role1);
            //return View(accounts.ToList());

            int sessionID = Convert.ToInt32(Session["AccountID"].ToString());
            var trainers = db.Accounts.Where(t => t.AccID.Equals(sessionID));
            return View(trainers.ToList());
        }

        // GET: Trainee/Accounts/Details/5
        public ActionResult Details(int? id)
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
            return View(account);
        }
        
        // GET: Trainee/Accounts/Edit/5
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

        // POST: Trainee/Accounts/Edit/5
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

        public ActionResult ChangePassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account acc = db.Accounts.Find(id);
            if (acc == null)
            {
                return HttpNotFound();
            }
            //ViewBag.AccountID = new SelectList(db.Accounts, "AccountID", "Email", trainer.AccountID);
            return View(acc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string newPassword, string confirmedPassword)
        {
            int sessionID = Convert.ToInt32(Session["AccountID"].ToString());
            Account acc = db.Accounts.Single(a => a.AccID == sessionID);
            if (newPassword != confirmedPassword)
            {
                TempData["error"] = "Confirmed password does not matches with new password!";
                return RedirectToAction("ChangePassword");
            }
            else
            {
                acc.Password = confirmedPassword;
                TempData["Message"] = "Password updated succesfully!";
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // return RedirectToAction("ViewProfile");
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
