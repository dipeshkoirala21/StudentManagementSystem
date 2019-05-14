using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalFinalCoursework.Models;
using PagedList;

namespace FinalFinalCoursework.Controllers
{
    public class StudentServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentServices
        public ActionResult Index()
        {
            return View(db.StudentServices.ToList());
        }

        // GET: StudentServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentService studentService = db.StudentServices.Find(id);
            if (studentService == null)
            {
                return HttpNotFound();
            }
            return View(studentService);
        }

        // GET: StudentServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentServiceID,Name,Username,Password")] StudentService studentService)
        {
            if (ModelState.IsValid)
            {
                db.StudentServices.Add(studentService);
                db.SaveChanges();
                return RedirectToAction("StudentService","Admin");
            }

            return View(studentService);
        }

        // GET: StudentServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentService studentService = db.StudentServices.Find(id);
            if (studentService == null)
            {
                return HttpNotFound();
            }
            return View(studentService);
        }

        // POST: StudentServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentServiceID,Name,Username,Password")] StudentService studentService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("StudentService", "Admin");
            }
            return View(studentService);
        }

        // GET: StudentServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentService studentService = db.StudentServices.Find(id);
            if (studentService == null)
            {
                return HttpNotFound();
            }
            return View(studentService);
        }

        // POST: StudentServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentService studentService = db.StudentServices.Find(id);
            db.StudentServices.Remove(studentService);
            db.SaveChanges();
            return RedirectToAction("StudentService", "Admin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Teachers()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("StudentService")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Teachers.ToList());
        }
        public ActionResult TeacherDetails(int? id)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("StudentService")))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher faculty = db.Teachers.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }
        public ActionResult CreateTeacher()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("StudentService")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeacher(Teacher faculty)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(faculty);
                db.SaveChanges();
                return RedirectToAction("Teachers");
            }

            return View(faculty);
        }
        public ActionResult Students(string SearchCourseName, int? page)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("StudentService")))
            {
                return RedirectToAction("Index", "Login");
            }
            var sa = from c in db.Students
                     orderby c.StudentID
                     select c;
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            if (!string.IsNullOrEmpty(SearchCourseName))
            {
                sa = sa.Where(x => x.Crs.Name.Contains(SearchCourseName)).OrderBy(x => x.StudentID);
            }

            return View(sa.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CreateStudents()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudents([Bind(Include = "StudentID,Name,Username,Email,Password,Gender,Contact,DateOfBirth,EnrollDate,CourseID,GroupID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", student.CourseID);
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", student.GroupID);
            return View(student);
        }
        public ActionResult Courses()
        {
            return View(db.Courses.ToList());
        }
        public ActionResult Groups()
        {
            return View(db.Groups.ToList());
        }
        public ActionResult Modules()
        {
            return View(db.Modules.ToList());
        }
        public ActionResult TeacherModules()
        {
            var teacher_Modules = db.Teacher_Modules.Include(t => t.Mod).Include(t => t.Tch);
            return View(teacher_Modules.ToList());
        }
        public ActionResult CreateTeacherModule()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("StudentService")))
            {
                return RedirectToAction("Index", "Login");
            }
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name");
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name");
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name");
            //ViewBag.Teachers = db.Teachers;
            return View();
            //ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name");
            //ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name");
            //return View();
        }

        // POST: Teacher_Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeacherModule([Bind(Include = "TeacherID,ModuleID,GroupID")] Teacher_Module teacher_Module)
        {
            if (ModelState.IsValid)
            {
                db.Teacher_Modules.Add(teacher_Module);
                db.SaveChanges();
                return RedirectToAction("TeacherModules");
            }

            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", teacher_Module.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", teacher_Module.TeacherID);
            return View(teacher_Module);
        }
        public ActionResult GroupModules()
        {
            var groupModules = db.GroupModules.Include(g => g.Group).Include(g => g.Module);
            return View(groupModules.ToList());
        }
        public ActionResult CreateGroupModules()
        {
            ViewBag.GroupId = new SelectList(db.Groups, "GroupID", "Name");
            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleID", "Name");
            return View();
        }

        // POST: GroupModules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroupModules([Bind(Include = "GroupId,ModuleId")] GroupModule groupModule)
        {
            if (ModelState.IsValid)
            {
                db.GroupModules.Add(groupModule);
                db.SaveChanges();
                return RedirectToAction("GroupModules");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "GroupID", "Name", groupModule.GroupId);
            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleID", "Name", groupModule.ModuleId);
            return View(groupModule);
        }
    }
}
