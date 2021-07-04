using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Asm.Models.Entity;

namespace Asm.Areas.Staff.Controllers
{
    public class CoursesController : Controller
    {
        private Manages_Context db = new Manages_Context();
        //private Model1 db = new Model1();

        // GET: Staff/Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Account).Include(c => c.CourseCategory);
            return View(courses.ToList());
        }

        [HttpPost]
        public ActionResult Search(string searchKey)
        {
            var a = db.Courses.Where(t => t.CourseName.Contains(searchKey) || searchKey == null).ToList();
            return View(a);
        }

        // GET: Staff/Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Staff/Courses/Create
        public ActionResult Create()
        {
            ViewBag.TrainerID = new SelectList(db.Accounts, "AccID", "Name");
            ViewBag.CatID = new SelectList(db.CourseCategories, "CourseCatID", "Name");
            return View();
        }

        // POST: Staff/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,CatID,CourseName,Description,TrainerID")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrainerID = new SelectList(db.Accounts, "AccID", "Name", course.TrainerID);
            ViewBag.CatID = new SelectList(db.CourseCategories, "CourseCatID", "Name", course.CatID);
            return View(course);
        }

        // GET: Staff/Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainerID = new SelectList(db.Accounts, "AccID", "Name", course.TrainerID);
            ViewBag.CatID = new SelectList(db.CourseCategories, "CourseCatID", "Name", course.CatID);
            return View(course);
        }

        // POST: Staff/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,CatID,CourseName,Description,TrainerID")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainerID = new SelectList(db.Accounts, "AccID", "Name", course.TrainerID);
            ViewBag.CatID = new SelectList(db.CourseCategories, "CourseCatID", "Name", course.CatID);
            return View(course);
        }

        public ActionResult Assigned(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            var findTrainer = db.Accounts.Where(f => f.Role.Equals("Trainer"));
            ViewBag.TrainerID = new SelectList(findTrainer, "AccID", "Name");
            ViewBag.CatID = new SelectList(db.CourseCategories, "CourseCatID", "Name", course.CatID);
            return View(course);
        }

        // POST: Staff/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assigned([Bind(Include = "CourseID,CatID,CourseName,Description,TrainerID")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainerID = new SelectList(db.Accounts, "AccID", "Name", course.TrainerID);
            ViewBag.CatID = new SelectList(db.CourseCategories, "CourseCatID", "Name", course.CatID);
            return View(course);
        }

        // GET: Staff/Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            var model = db.Courses.Where(x => x.CourseID == id).FirstOrDefault();
            var model1 = db.Assigned_Course.Where(x => x.CourseID == id).FirstOrDefault();
            if (model != null)
            {
                db.Courses.Remove(model);
                if(model1 != null)
                {
                    db.Assigned_Course.Remove(model1);
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return new HttpNotFoundResult();
        }

        // POST: Staff/Courses/Delete/5

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
