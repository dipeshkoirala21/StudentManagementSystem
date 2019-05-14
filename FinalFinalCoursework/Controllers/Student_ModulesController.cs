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
    public class Student_ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Student_Modules
        public ActionResult Index()
        {
            var student_Modules = db.Student_Modules.Include(s => s.Mod).Include(s => s.Std);
            return View(student_Modules.ToList());
        }

        // GET: Student_Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Modules student_Modules = db.Student_Modules.Find(id);
            if (student_Modules == null)
            {
                return HttpNotFound();
            }
            return View(student_Modules);
        }

        // GET: Student_Modules/Create
        public ActionResult Create()
        {
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            return View();
        }

        // POST: Student_Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,ModuleID")] Student_Modules student_Modules)
        {
            if (ModelState.IsValid)
            {
                db.Student_Modules.Add(student_Modules);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", student_Modules.ModuleID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", student_Modules.StudentID);
            return View(student_Modules);
        }

        // GET: Student_Modules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Modules student_Modules = db.Student_Modules.Find(id);
            if (student_Modules == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", student_Modules.ModuleID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", student_Modules.StudentID);
            return View(student_Modules);
        }

        // POST: Student_Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,ModuleID")] Student_Modules student_Modules)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Modules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", student_Modules.ModuleID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", student_Modules.StudentID);
            return View(student_Modules);
        }

        // GET: Student_Modules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Modules student_Modules = db.Student_Modules.Find(id);
            if (student_Modules == null)
            {
                return HttpNotFound();
            }
            return View(student_Modules);
        }

        // POST: Student_Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student_Modules student_Modules = db.Student_Modules.Find(id);
            db.Student_Modules.Remove(student_Modules);
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
