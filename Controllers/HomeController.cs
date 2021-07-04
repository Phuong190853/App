using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asm.Models.Entity;


namespace Asm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        Manages_Context db = new Manages_Context();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginProcess(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var data = db.Accounts.Where(acc => acc.Email.Equals(email) && acc.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    Session["AccountID"] = data.FirstOrDefault().AccID;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["Role"] = data.FirstOrDefault().Role;
                    if (Session["Role"].ToString().Contains("Admin"))
                    {
                        return RedirectToAction("Welcome", "Accounts", new { area = "Admin" });
                    }
                    else if(Session["Role"].ToString().Contains("Staff"))
                    {
                        return RedirectToAction("Welcome", "Accounts", new { area = "Staff" });
                    }
                    else if (Session["Role"].ToString().Contains("Trainer"))
                    {
                        return RedirectToAction("Welcome", "Accounts", new { area = "Trainer" });
                    }
                    else if (Session["Role"].ToString().Contains("Trainee"))
                    {
                        return RedirectToAction("Welcome", "Accounts", new { area = "Trainee" });
                    }
                    return View("Index");
                }
                else
                {
                    ViewBag.error = "Email or password is incorrect!";
                    return View("Login");
                }
            }

            return View();
        }

        public ActionResult Logout()
        {

            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();

            return View("Login");
        }
    }
}
