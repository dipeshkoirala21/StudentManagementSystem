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
    public class TeacherGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeacherGroups
        public ActionResult Index()
        {
            var teacherGroups = db.TeacherGroups.Include(t => t.Group).Include(t => t.Teacher);
            return View(teacherGroups.ToList());
        }

        // GET: TeacherGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherGroup teacherGroup = db.TeacherGroups.Find(id);
            if (teacherGroup == null)
            {
                return HttpNotFound();
            }
            return View(teacherGroup);
        }

        // GET: TeacherGroups/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name");
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name");
            return View();
        }

        // POST: TeacherGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherID,GroupID")] TeacherGroup teacherGroup)
        {
            if (ModelState.IsValid)
            {
                db.TeacherGroups.Add(teacherGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", teacherGroup.GroupID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", teacherGroup.TeacherID);
            return View(teacherGroup);
        }

        // GET: TeacherGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherGroup teacherGroup = db.TeacherGroups.Find(id);
            if (teacherGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", teacherGroup.GroupID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", teacherGroup.TeacherID);
            return View(teacherGroup);
        }

        // POST: TeacherGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherID,GroupID")] TeacherGroup teacherGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", teacherGroup.GroupID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", teacherGroup.TeacherID);
            return View(teacherGroup);
        }

        // GET: TeacherGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherGroup teacherGroup = db.TeacherGroups.Find(id);
            if (teacherGroup == null)
            {
                return HttpNotFound();
            }
            return View(teacherGroup);
        }

        // POST: TeacherGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherGroup teacherGroup = db.TeacherGroups.Find(id);
            db.TeacherGroups.Remove(teacherGroup);
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
