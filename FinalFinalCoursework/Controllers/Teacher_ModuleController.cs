using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalFinalCoursework.Models;

namespace FinalFinalCoursework.Controllers
{
    public class Teacher_ModuleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teacher_Module
        public ActionResult Index()
        {
            var teacher_Modules = db.Teacher_Modules.Include(t => t.Mod).Include(t => t.Tch);
            return View(teacher_Modules.ToList());
        }

        // GET: Teacher_Module/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Module teacher_Module = db.Teacher_Modules.Find(id);
            if (teacher_Module == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Module);
        }

        // GET: Teacher_Module/Create
        public ActionResult Create()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin") ) )
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
        public ActionResult Create([Bind(Include = "TeacherID,ModuleID,GroupID")] Teacher_Module teacher_Module)
        {
            if (ModelState.IsValid)
            {
                db.Teacher_Modules.Add(teacher_Module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", teacher_Module.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", teacher_Module.TeacherID);
            return View(teacher_Module);
        }

        // GET: Teacher_Module/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Module teacher_Module = db.Teacher_Modules.Find(id);
            if (teacher_Module == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", teacher_Module.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", teacher_Module.TeacherID);
            return View(teacher_Module);
        }

        // POST: Teacher_Module/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherID,ModuleID,GroupID")] Teacher_Module teacher_Module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher_Module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", teacher_Module.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", teacher_Module.TeacherID);
            return View(teacher_Module);
        }

        // GET: Teacher_Module/Delete/5
        public ActionResult Delete(int? id1, int? id2, int? id3)
        {

            if (id1 == null || id2 == null || id3 == null)
            {
                return RedirectToAction("Index");
            }
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
            {
                return RedirectToAction("Index", "Login");
            }

            Teacher_Module fd1 = db.Teacher_Modules.Where(fd => fd.ModuleID == id1 && fd.TeacherID == id2 && fd.GroupID == id3).FirstOrDefault();
            return View(fd1);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteFacultyCourse(int id1, int id2, int id3)
        {
            Teacher_Module fd1 = db.Teacher_Modules.Where(fd => fd.ModuleID == id1 && fd.TeacherID == id2 && fd.GroupID == id3).FirstOrDefault();

            db.Teacher_Modules.Remove(fd1);
            db.SaveChanges();
            Teacher_Module fd2 = db.Teacher_Modules.Where(fd => fd.TeacherID == id2 && fd.GroupID == id3).FirstOrDefault();
            db.Teacher_Modules.Remove(fd2);
            db.SaveChanges();
            var count = db.Teacher_Modules.Where(fd => fd.ModuleID == id1 && fd.GroupID == id3).Count();
            if (count == 0)
            {
                GroupModule fd = db.GroupModules.Where(fd3 => fd3.GroupId == id3 && fd3.ModuleId == id1).FirstOrDefault();
                db.GroupModules.Remove(fd);
                db.SaveChanges();

            }

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
