using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Asm.Models.Entity;

namespace Asm.Areas.Trainer.Controllers
{
    public class CoursesController : Controller
    {
        private Manages_Context db = new Manages_Context();

        // GET: Trainer/Courses
        public ActionResult Index()
        {

            int sessionID = Convert.ToInt32(Session["AccountID"].ToString());
            var trainers_course = db.Courses.Include(a => a.Account).Where(a => a.TrainerID == sessionID);
            return View(trainers_course.ToList());
        }

        public ActionResult Details(int? id)
        {
            Course courseName = db.Courses.Find(id);
            ViewBag.courseName = courseName.CourseName;
            Course courseCategory = db.Courses.Include(c => c.CourseCategory).Where(x => x.CourseID == id).First();
            ViewBag.courseCategory = courseName.CourseCategory.Name;
            Course trainerName = db.Courses.Include(c => c.Account).Where(x => x.CourseID == id).First();
            ViewBag.name = trainerName.Account.Name;

            List<Assigned_Course> courses = db.Assigned_Course.Include(c => c.Account).Include(c => c.Course).Where(x => x.CourseID == id).ToList();
            return View(courses);
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
