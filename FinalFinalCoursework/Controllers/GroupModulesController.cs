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
    public class GroupModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GroupModules
        public ActionResult Index()
        {
            var groupModules = db.GroupModules.Include(g => g.Group).Include(g => g.Module);
            return View(groupModules.ToList());
        }

        // GET: GroupModules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModule groupModule = db.GroupModules.Find(id);
            if (groupModule == null)
            {
                return HttpNotFound();
            }
            return View(groupModule);
        }

        // GET: GroupModules/Create
        public ActionResult Create()
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
        public ActionResult Create([Bind(Include = "GroupId,ModuleId")] GroupModule groupModule)
        {
            if (ModelState.IsValid)
            {
                db.GroupModules.Add(groupModule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "GroupID", "Name", groupModule.GroupId);
            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleID", "Name", groupModule.ModuleId);
            return View(groupModule);
        }

        // GET: GroupModules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModule groupModule = db.GroupModules.Find(id);
            if (groupModule == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "GroupID", "Name", groupModule.GroupId);
            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleID", "Name", groupModule.ModuleId);
            return View(groupModule);
        }

        // POST: GroupModules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,ModuleId")] GroupModule groupModule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupModule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "GroupID", "Name", groupModule.GroupId);
            ViewBag.ModuleId = new SelectList(db.Modules, "ModuleID", "Name", groupModule.ModuleId);
            return View(groupModule);
        }

        // GET: GroupModules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModule groupModule = db.GroupModules.Find(id);
            if (groupModule == null)
            {
                return HttpNotFound();
            }
            return View(groupModule);
        }

        // POST: GroupModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupModule groupModule = db.GroupModules.Find(id);
            db.GroupModules.Remove(groupModule);
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
