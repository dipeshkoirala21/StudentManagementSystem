using FinalFinalCoursework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalFinalCoursework.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(login l)
        {
            String usertype = l.usertype.ToString();
            string userName = l.username;
            String password = l.password.ToString();
            if (usertype.Equals("Admin"))
            {
                Admin s = db.Admins.Where(x => x.username == userName && x.Password == password).FirstOrDefault();
                var userId = db.Admins.Where(x => x.username == userName).Select(x => x.AdminId).FirstOrDefault();
                if (s == null)
                {
                    ViewBag.ErrorMessage = "Please provide correct credentials!!";
                    return RedirectToAction("Index","Login"); // change karvanu chhe
                }
                else
                {
                    Session["userId"] = userId;
                    Session["userName"] = userName;
                    Session["userType"] = usertype;
                    //return View("Index");
                }
                return RedirectToAction("Index", "Admin");
            } 
            else if (usertype.Equals("Student"))
            {
                
                Student s=db.Students.Where(x => x.Username == userName && x.Password == password).FirstOrDefault();
                var userId = db.Students.Where(x => x.Username == userName).Select(x => x.StudentID).FirstOrDefault();
                if (s == null)
                {
                    ViewBag.ErrorMessage = "Please provide correct credentials!!";
                    return View("Index");
                }
                else
                {
                    Session["userId"] = userId;
                    Session["userType"] = usertype;
                    Session["userName"] = userName;
                    return RedirectToAction("Index", "Attendances");
                }
            }
            else if(usertype.Equals("Teacher"))
            {
               
                Teacher f = db.Teachers.Where(x => x.Username == userName && x.Password == password).FirstOrDefault();
                var userId = db.Teachers.Where(x => x.Username == userName).Select(x => x.TeacherID).FirstOrDefault();
                if (f == null)
                {
                    ViewBag.ErrorMessage = "Please provide correct credentials!!";
                    return View("Index");
                }
                else
                {
                    Session["userId"] = userId;
                    Session["userType"] = usertype;
                    Session["userName"] = userName;
                    return RedirectToAction("Index", "Teachers");
                }
            }
            else if (usertype.Equals("StudentService"))
            {

                StudentService f = db.StudentServices.Where(x => x.Username == userName && x.Password == password).FirstOrDefault();
                var userId = db.StudentServices.Where(x => x.Username == userName).Select(x => x.StudentServiceID).FirstOrDefault();
                if (f == null)
                {
                    ViewBag.ErrorMessage = "Please provide correct credentials!!";
                    return View("Index");
                }
                else
                {
                    Session["userId"] = userId;
                    Session["userType"] = usertype;
                    Session["userName"] = userName;
                    return RedirectToAction("Index", "StudentServices");
                }
            }
            return View();
          
        }
        public ActionResult InvalidLogin()
        {
            return View();
        }
       

    
    }
}