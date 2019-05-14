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
    public class TimeTablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeTables
        public ActionResult Index(string SearchGroupName, int? page)
        {
            if (Session["userType"] == null )
            {
                return RedirectToAction("Index", "Login");
            }
            var sa = from c in db.TimeTables
                     orderby c.TimeTableID
                     select c;
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            if (!string.IsNullOrEmpty(SearchGroupName))
            {
                sa = sa.Where(x => x.Group.Name.Contains(SearchGroupName)).OrderBy(x => x.TimeTableID);
            }

            return View(sa.ToPagedList(pageNumber, pageSize));
        }

        // GET: TimeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeTable timeTable = db.TimeTables.Find(id);
            if (timeTable == null)
            {
                return HttpNotFound();
            }
            return View(timeTable);
        }

        // GET: TimeTables/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name");
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name");
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name");
            return View();
        }

        // POST: TimeTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeTableID,ClassName,ClassType,Time,Day,TeacherID,GroupID,ModuleID")] TimeTable timeTable)
        {
            if (ModelState.IsValid)
            {
                db.TimeTables.Add(timeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", timeTable.GroupID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", timeTable.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", timeTable.TeacherID);
            return View(timeTable);
        }

        // GET: TimeTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeTable timeTable = db.TimeTables.Find(id);
            if (timeTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", timeTable.GroupID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", timeTable.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", timeTable.TeacherID);
            return View(timeTable);
        }

        // POST: TimeTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeTableID,ClassName,ClassType,Time,Day,TeacherID,GroupID,ModuleID")] TimeTable timeTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", timeTable.GroupID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", timeTable.ModuleID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "Name", timeTable.TeacherID);
            return View(timeTable);
        }

        // GET: TimeTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeTable timeTable = db.TimeTables.Find(id);
            if (timeTable == null)
            {
                return HttpNotFound();
            }
            return View(timeTable);
        }

        // POST: TimeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeTable timeTable = db.TimeTables.Find(id);
            db.TimeTables.Remove(timeTable);
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
