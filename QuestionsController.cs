using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParnianTv.Models;

namespace ParnianTv.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "3")]
    public class QuestionsController : Controller
    {
        private ParnianDBEntities db = new ParnianDBEntities();

        // GET: Questions
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.Question_Status).Include(q => q.Videos);
            return View(questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.Qu_Status = new SelectList(db.Question_Status, "St_ID", "St_Name");
            ViewBag.Qu_Vi_ID = new SelectList(db.Videos, "Vi_ID", "Vi_Name");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Qu_ID,Qu_Time,Qu_Answer_Time,Qu_Answer_Time_Min,Qu_U_Fname,Qu_U_Lname,Qu_U_Email,Qu_U_Mobile,Qu_U_IP,Qu_Vi_ID,Qu_Status,Qu_Description")] Questions questions)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(questions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Qu_Status = new SelectList(db.Question_Status, "St_ID", "St_Name", questions.Qu_Status);
            ViewBag.Qu_Vi_ID = new SelectList(db.Videos, "Vi_ID", "Vi_Name", questions.Qu_Vi_ID);
            return View(questions);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            ViewBag.Qu_Status = new SelectList(db.Question_Status, "St_ID", "St_Name", questions.Qu_Status);
            ViewBag.Qu_Vi_ID = new SelectList(db.Videos, "Vi_ID", "Vi_Name", questions.Qu_Vi_ID);
            return View(questions);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Qu_ID,Qu_Time,Qu_Answer_Time,Qu_Answer_Time_Min,Qu_U_Fname,Qu_U_Lname,Qu_U_Email,Qu_U_Mobile,Qu_U_IP,Qu_Vi_ID,Qu_Status,Qu_Description")] Questions questions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Qu_Status = new SelectList(db.Question_Status, "St_ID", "St_Name", questions.Qu_Status);
            ViewBag.Qu_Vi_ID = new SelectList(db.Videos, "Vi_ID", "Vi_Name", questions.Qu_Vi_ID);
            return View(questions);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Questions questions = db.Questions.Find(id);
            db.Questions.Remove(questions);
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
