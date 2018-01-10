﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sem1Exampaper.Models.AttendModels;

namespace Sem1Exampaper.Controllers
{
    public class AttendanceEntryController : Controller
    {
        private AttendDbContext db = new AttendDbContext();

        // GET: AttendanceEntry
        public ActionResult Index()
        {
            var attendances = db.Attendances.Include(a => a.assocStudent).Include(a => a.assocSubject);
            return View(attendances.ToList());
        }

        // GET: AttendanceEntry/Details/5
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

        // GET: AttendanceEntry/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName");
            return View();
        }

        // POST: AttendanceEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendanceID,AttendanceDate,SubjectID,StudentID,Present")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", attendance.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", attendance.SubjectID);
            return View(attendance);
        }

        // GET: AttendanceEntry/Edit/5
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
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", attendance.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", attendance.SubjectID);
            return View(attendance);
        }

        // POST: AttendanceEntry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendanceID,AttendanceDate,SubjectID,StudentID,Present")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", attendance.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", attendance.SubjectID);
            return View(attendance);
        }

        // GET: AttendanceEntry/Delete/5
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

        // POST: AttendanceEntry/Delete/5
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