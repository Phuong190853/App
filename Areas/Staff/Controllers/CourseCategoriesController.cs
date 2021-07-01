using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asm.Models.Entities;

namespace Asm.Areas.Staff.Controllers
{
    public class CourseCategoriesController : Controller
    {
        private Manage_Context db = new Manage_Context();

        // GET: Staff/CourseCategories
        public ActionResult Index()
        {
            return View(db.CourseCategories.ToList());
        }

        // GET: Staff/CourseCategories/Details/5

        // GET: Staff/CourseCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staff/CourseCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseCatID,Name,Description")] CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                db.CourseCategories.Add(courseCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseCategory);
        }

        // GET: Staff/CourseCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCategory courseCategory = db.CourseCategories.Find(id);
            if (courseCategory == null)
            {
                return HttpNotFound();
            }
            return View(courseCategory);
        }

        // POST: Staff/CourseCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseCatID,Name,Description")] CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseCategory);
        }

        // GET: Staff/CourseCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            var model = db.CourseCategories.Where(x => x.CourseCatID == id).FirstOrDefault();
            if (model != null)
            {
                db.CourseCategories.Remove(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return new HttpNotFoundResult();
        }

        // POST: Staff/CourseCategories/Delete/5

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
