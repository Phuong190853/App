using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asm.Models.Entities;

namespace Asm.Areas.Trainer.Controllers
{
    public class CoursesController : Controller
    {
        private Manage_Context db = new Manage_Context();

        // GET: Trainer/Courses
        public ActionResult Index()
        {
            //var courses = db.Courses.Include(c => c.Account).Include(c => c.CourseCategory);

            int sessionID = Convert.ToInt32(Session["AccountID"].ToString());
            var trainers_course = db.Courses.Include(a => a.Account).Where(a => a.TrainerID == sessionID);
            return View(trainers_course.ToList());
        }

        // GET: Trainer/Courses/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Course course = db.Courses.Find(id);
        //    if (course == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(course);
        //}

        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Course courseName = db.Courses.Find(id);
            ViewBag.courseName = courseName.CourseName;
            Course courseCategory = db.Courses.Include(c => c.CourseCategory).Where(x => x.CourseID == id).First();
            ViewBag.courseCategory = courseName.CourseCategory.Name;
            Course trainerName = db.Courses.Include(c => c.Account).Where(x => x.CourseID == id).First();
            ViewBag.name = trainerName.Account.Name;
            //if (course == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(course);

            List<Assigned_Course> courses = db.Assigned_Course.Include(c => c.Account).Include(c => c.Course).Where(x => x.CourseID == id).ToList();
            return View(courses);
        }

        // GET: Trainer/Courses/Create
        public ActionResult Create()
        {
            ViewBag.TrainerID = new SelectList(db.Accounts, "AccID", "Name");
            ViewBag.CatID = new SelectList(db.CourseCategories, "CourseCatID", "Name");
            return View();
        }

        // POST: Trainer/Courses/Create
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

        // GET: Trainer/Courses/Edit/5
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

        // POST: Trainer/Courses/Edit/5
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

        // GET: Trainer/Courses/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Trainer/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
