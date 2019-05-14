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
    public class GroupIdName
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
    public class AttendancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShowStudentAttendance
        public ActionResult ShowDivisions(int id)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Student")))
            {
                return RedirectToAction("Index", "Login");
            }
            //List<String> divisionName;
            Dictionary<string, int> TotalCount = new Dictionary<string, int>();
            Dictionary<string, int> TotalLectures = new Dictionary<string, int>();
            int UserId = Int32.Parse(Session["UserId"].ToString());
            //int DivId = db.Students.Where(s => s.StudentId == UserId).FirstOrDefault().DivisionId;
            var ModuleIds = db.GroupModules.Where(s => s.GroupId == id).Select(s => s.ModuleId);
            
            foreach (var Cid in ModuleIds)
            {
                int t = db.Attendances.Where(s => s.StudentID == UserId && s.ModuleID == Cid && s.status == Attendance.Status.P  && s.GroupID == id).Count();
                string c = db.Modules.Where(s => s.ModuleID == Cid).FirstOrDefault().Name;
                int t1 = db.Attendances.Where(s => s.StudentID == UserId && s.ModuleID == Cid && s.GroupID == id).Count();
                TotalLectures.Add(c, t1);
                TotalCount.Add(c, t);
            }
            ViewBag.TotalCount = TotalCount;
            ViewBag.TotalLectures = TotalLectures;
            ViewBag.Sname = db.Students.Where(s => s.StudentID == UserId).FirstOrDefault().Name;
            return View();
        }

        // GET: Attendances
        public ActionResult Index()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Student")))
            {
                return RedirectToAction("Index", "Login");
            }
            var StudentId = Int32.Parse(Session["userId"].ToString());
            IEnumerable<GroupIdName> groups = db.Attendances.Where(s => s.StudentID == StudentId).Select(s => new GroupIdName { GroupId = s.GroupID, GroupName = db.Groups.Where(div => div.GroupID == s.GroupID).FirstOrDefault().Name.ToString() }).Distinct();
            ViewBag.Groups = groups;
            return View();

            //var attendances = db.Attendances.Include(a => a.Grp).Include(a => a.Module).Include(a => a.Std).Include(a => a.Timtab);
            //return View(attendances.ToList());
        }

        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name");
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendanceID,Date,StudentID,ModuleID,GroupID,status")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", attendance.GroupID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", attendance.ModuleID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", attendance.StudentID);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", attendance.GroupID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", attendance.ModuleID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", attendance.StudentID);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendanceID,Date,StudentID,ModuleID,GroupID,status")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", attendance.GroupID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name", attendance.ModuleID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Name", attendance.StudentID);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
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
