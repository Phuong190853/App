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
    public class Assigned_CourseController : Controller
    {
        private Manages_Context db = new Manages_Context();

        // GET: Trainee/Assigned_Course
        public ActionResult Index()
        {
            int sessionID = Convert.ToInt32(Session["AccountID"].ToString());
            var assigned_Course = db.Assigned_Course.Include(a => a.Account).Include(a => a.Course).Where(a => a.TraineeID == sessionID);

            Course trainerName = db.Courses.Include(c => c.Account).Where(x => x.CourseID == sessionID).First();
            ViewBag.name = trainerName.Account.Name;
            return View(assigned_Course.ToList());
        }

        // GET: Trainee/Assigned_Course/Details/5
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

        // GET: Trainee/Assigned_Course/Create

        public ActionResult IndexCourse()
        {
            //var courses = db.Courses.Include(c => c.Account).Include(c => c.CourseCategory);
            var course = db.Courses.Include(a => a.Account);
            return View(course.ToList());
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
