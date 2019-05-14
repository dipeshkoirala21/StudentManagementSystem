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
    
        public class AdminController : Controller
        {
            // GET: Admin
            ApplicationDbContext db = new ApplicationDbContext();
            public ActionResult Index()
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                return View();
            }
            public ActionResult AssignModulesToTeachers()
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                if (TempData.ContainsKey("Message"))
                {
                    ViewBag.Message = TempData["Message"];
                }
                ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "Name");
                ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name");
                ViewBag.Teachers = db.Teachers;
                return View();
            }
            [HttpPost]
            public ActionResult AssignModulesToTeachers(int ModuleID, int[] TeacherIDs, int GroupID)
            {
                bool flag = false;
                TempData["Message"] = "<ul>";
                foreach (int fId in TeacherIDs)
                {
                    if (db.Teacher_Modules.Where(fd => fd.ModuleID == ModuleID && fd.TeacherID == fId && fd.GroupID == GroupID).FirstOrDefault() != null)
                    {
                        TempData["Message"] += "<li>" + "Teacher " + db.Teachers.Where(f => f.TeacherID == fId).FirstOrDefault().Name + " Is Already Assigned To " + db.Modules.Where(d => d.ModuleID == ModuleID).FirstOrDefault().Name + ". " + "</li>";
                        flag = true;
                    }
                    if (!flag)
                    {
                        Teacher_Module mTeacher = new Teacher_Module();
                        mTeacher.ModuleID = ModuleID;
                        mTeacher.TeacherID = fId;
                        mTeacher.GroupID = GroupID;
                        db.Teacher_Modules.Add(mTeacher);
                        db.SaveChanges();
                        try
                        {
                            TeacherGroup tGroup = new TeacherGroup();
                            tGroup.TeacherID = fId;
                            tGroup.GroupID = GroupID;
                            db.TeacherGroups.Add(tGroup);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {

                        }

                        try
                        {

                            GroupModule dCourse = new GroupModule();
                            dCourse.ModuleId = ModuleID;
                            dCourse.GroupId = GroupID;
                            db.GroupModules.Add(dCourse);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {

                        }



                    }
                }
                if (flag)
                {
                    TempData["Message"] += "</ul>";
                    return RedirectToAction("AssignModulesToTeachers");
                }
                return RedirectToAction("Index", "Teacher_Modlue");
            }
            public ActionResult AssignTeacherToGroup()
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                if (TempData.ContainsKey("Message"))
                {
                    ViewBag.Message = TempData["Message"];
                }

                ViewBag.GroupId = new SelectList(db.Groups, "GroupId", "Name");
                ViewBag.ModuleId = new SelectList(db.Modules, "ModuleId", "Name");

                ViewBag.Teacher = db.Teacher_Modules;
                return View();
            }
            [HttpPost]
            public ActionResult AssignTeacherToGroup(int GroupId, int ModuleId, int[] TeacherIds)
            {
                bool flag = false;
                TempData["Message"] = "<ul>";
                foreach (int fId in TeacherIds)
                {
                    if (db.TeacherGroups.Where(fd => fd.GroupID == GroupId && fd.TeacherID == fId).FirstOrDefault() != null)
                    {
                        TempData["Message"] += "<li>" + "Teacher " + db.Teachers.Where(f => f.TeacherID == fId).FirstOrDefault().Name + " Is Already Assigned To " + db.Groups.Where(d => d.GroupID == GroupId).FirstOrDefault().Name + ". " + "for Module Name : " + db.Modules.Where(c => c.ModuleID == ModuleId).FirstOrDefault().Name + "</li>";
                        flag = true;
                    }
                    if (!flag)
                    {
                        TeacherGroup fDivision = new TeacherGroup();
                        fDivision.GroupID = GroupId;
                        fDivision.TeacherID = fId;
                        //fDivision.CourseId = CourseId;
                        db.TeacherGroups.Add(fDivision);
                        db.SaveChanges();
                    }

                }
                if (flag)
                {
                    TempData["Message"] += "</ul>";
                    return RedirectToAction("AssignTeacherToGroup");
                }
                return RedirectToAction("Index", "TeacherGroup");
            }
            /*public ActionResult ViewFacultyDivisionData()
            {
                return View(db.FacultyDivisions);
            }*/
            public ActionResult CreateTeacher()
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                return View();
            }
            [HttpPost]
            public ActionResult CreateTeacher(Teacher f)
            {
                Teacher fa = new Teacher();
                fa = f;
                db.Teachers.Add(f);
                db.SaveChanges();
                return RedirectToAction("ViewFaculties");
            }
            public ActionResult ViewTeacher()
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                return View(db.Teachers);
            }
            public ActionResult ViewGroups()
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                return View(db.Groups);
            }
            public ActionResult CreateGroups()
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                return View();
            }
            [HttpPost]
            public ActionResult CreateGroups(Models.GroupIdName d)
            {
            Models.GroupIdName d1 = new Models.GroupIdName();
                d1 = d;
                db.Groups.Add(d1);
                db.SaveChanges();
                return RedirectToAction("ViewGroups");
            }

            public ActionResult ViewModules()
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                return View(db.Modules);
            }
            public ActionResult CreateModule()
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                return View();
            }
            [HttpPost]
            public ActionResult CreateModule(Module c)
            {
                Module c1 = new Module();
                c1 = c;
                db.Modules.Add(c1);
                db.SaveChanges();
                return RedirectToAction("ViewModules");
            }
            public ActionResult EditModules(int id)
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                return View(db.Modules.Where(c => c.ModuleID == id).FirstOrDefault());
            }
            [HttpPost]
            public ActionResult EditModule(Module c)
            {
                Module c1 = db.Modules.Where(co => co.ModuleID == c.ModuleID).FirstOrDefault();
                c1.Sem = c.Sem;
                c1.Name = c.Name;
                db.Entry(c1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewModules");
            }
            public ActionResult DeleteModule(int id)
            {
                if (Session["userType"] == null || (Session["userType"] != null && !Session["userType"].Equals("Admin")))
                {
                    return RedirectToAction("Index", "Login");
                }
                return View(db.Modules.Where(c => c.ModuleID == id).FirstOrDefault());
            }
            [HttpPost]
            [ActionName("DeleteModules")]
            public ActionResult DeleteCourse1(int id)
            {
                Module c1 = db.Modules.Where(c => c.ModuleID == id).FirstOrDefault();
                db.Modules.Remove(c1);
                db.SaveChanges();
                return RedirectToAction("ViewModules");

            }
            //public ActionResult EditFaculty(int )

            public ActionResult StudentService()
        {
            return View(db.StudentServices.ToList());
        }
        public ActionResult EditSS(int? id)
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
        public ActionResult EditSS([Bind(Include = "StudentServiceID,Name,Username,Password")] StudentService studentService)
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
        public ActionResult DeleteSS(int? id)
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
        [HttpPost, ActionName("DeleteSS")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentService studentService = db.StudentServices.Find(id);
            db.StudentServices.Remove(studentService);
            db.SaveChanges();
            return RedirectToAction("StudentService", "Admin");
        }
        public ActionResult DetailsSS(int? id)
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
    }
    
}
