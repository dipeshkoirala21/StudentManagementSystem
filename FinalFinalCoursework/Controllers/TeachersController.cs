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
    public class TeachersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teachers
        public ActionResult Index()
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Teacher")))
            {
                return RedirectToAction("Index", "Login");
            }
            int fid = Int32.Parse(Session["userId"].ToString());
            String fname = db.Teachers.Where(f => f.TeacherID == fid).FirstOrDefault().Name;

            var result1 = from fd in db.TeacherGroups
                          join f in db.Teachers on fd.TeacherID equals f.TeacherID
                          select new { TeacherId = f.TeacherID, GroupId = fd.GroupID, groupName = db.Groups.Where(div => div.GroupID == fd.GroupID).FirstOrDefault().Name };

            ViewBag.GroupId = new SelectList(result1.Where(r => r.TeacherId == fid), "GroupId", "groupName");
            var result2 = from fc in db.Teacher_Modules
                          join f in db.Teachers on fc.TeacherID equals f.TeacherID
                          select new { TeacherId = f.TeacherID, moduleId = fc.ModuleID, moduleName = db.Modules.Where(c => c.ModuleID == fc.ModuleID).FirstOrDefault().Name };
            ViewBag.ModuleId = new SelectList(result2.Where(res => res.TeacherId == fid), "moduleId", "moduleName");
            //ViewBag.Semesters=new SelectList()
            ViewBag.Fname = fname;
            return View();
        }
        public ActionResult TakeAttendance(int? GroupId, int? ModuleId, DateTime? news_date)
        {
            if (GroupId == null || ModuleId == null || news_date == null)
            {
                return RedirectToAction("Index");
            }
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Teacher")))
            {
                return RedirectToAction("Index", "Login");
            }
            TempData["ModuleId"] = ModuleId;
            TempData["GroupId"] = GroupId;
            TempData["Date"] = news_date;
            return View(db.Students.Where(s => s.GroupID == GroupId));
        }
        public ActionResult SubmitAttendance(int[] attendance)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Teacher")))
            {
                return RedirectToAction("Index", "Login");
            }
            if (attendance != null)
            {
                foreach (var sid in attendance)
                {
                    Attendance s = new Attendance();
                    s.StudentID = sid;
                    s.ModuleID = Int32.Parse(TempData["ModuleId"].ToString());
                    s.status = Attendance.Status.P;
                    s.Date = DateTime.Parse(TempData["Date"].ToString());
                    s.GroupID = Int32.Parse(TempData["GroupId"].ToString());
                    // s.date = DateTime.Parse(DateTime.Now.Date.ToShortDateString());
                    db.Attendances.Add(s);
                    db.SaveChanges();


                }
            }
            int did = Int32.Parse(TempData["GroupId"].ToString());
            var result = db.Students.Where(s => s.GroupID == did).Select(s => s.StudentID);

            if (attendance != null)
            {
                result = result.Except(attendance);
            }

            foreach (var id in result.ToList())
            {
                Attendance s = new Attendance();
                s.StudentID = id;
                s.ModuleID = Int32.Parse(TempData["ModuleId"].ToString());
                s.status = Attendance.Status.A;
                s.Date = DateTime.Parse(TempData["Date"].ToString());
                s.GroupID = Int32.Parse(TempData["GroupId"].ToString());
                db.Attendances.Add(s);
                db.SaveChanges();
            }
            TempData.Keep();
            return RedirectToAction("ShowAttendances");
        }

        public ActionResult EditAttendance(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Teacher")))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Attendances.Where(sa => sa.AttendanceID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditAttendance(Attendance s)
        {
            Attendance sa = db.Attendances.Where(saa => saa.AttendanceID == s.AttendanceID).FirstOrDefault();
            sa.status = s.status;
            db.Entry(sa).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ShowAttendances");
        }
        public ActionResult ShowAttendances(string SearchModuleName, DateTime? SearchDate, int? page)
        {
            if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Teacher")))
            {
                return RedirectToAction("Index", "Login");
            }
            var sa = from c in db.Attendances
                     orderby c.AttendanceID
                     select c;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (!string.IsNullOrEmpty(SearchModuleName))
            {
                sa = sa.Where(x => x.Module.Name.Contains(SearchModuleName)).OrderBy(x => x.AttendanceID);
            }
            if (!string.IsNullOrEmpty(SearchDate.ToString()))
            {
                sa = sa.Where(x => x.Date == SearchDate).OrderBy(x => x.AttendanceID);
            }
            return View(sa.ToPagedList(pageNumber, pageSize));
        }
    }
}
