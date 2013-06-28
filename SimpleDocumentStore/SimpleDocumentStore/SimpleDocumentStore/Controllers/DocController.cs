using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleDocumentStore.Models;

namespace SimpleDocumentStore.Controllers
{
    public class DocController : Controller
    {
        private SimpleDocumentStoreEntities db = new SimpleDocumentStoreEntities();

        //
        // GET: /Doc/

        public ActionResult Index()
        {
            var docs = db.Docs.Include(d => d.DocType);
            return View(docs.ToList());
        }

        //
        // GET: /Doc/Details/5

        public ActionResult Details(int id = 0)
        {
            Doc doc = db.Docs.Find(id);
            if (doc == null)
            {
                return HttpNotFound();
            }
            return View(doc);
        }

        //
        // GET: /Doc/Create

        public ActionResult Create()
        {
            ViewBag.DocTypeId = new SelectList(db.DocTypes, "ID", "DocTypeDescription");
            return View();
        }

        //
        // POST: /Doc/Create

        [HttpPost]
        public ActionResult Create(Doc doc)
        {
            if (ModelState.IsValid)
            {
                db.Docs.Add(doc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocTypeId = new SelectList(db.DocTypes, "ID", "DocTypeDescription", doc.DocTypeId);
            return View(doc);
        }

        //
        // GET: /Doc/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Doc doc = db.Docs.Find(id);
            if (doc == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocTypeId = new SelectList(db.DocTypes, "ID", "DocTypeDescription", doc.DocTypeId);
            return View(doc);
        }

        //
        // POST: /Doc/Edit/5

        [HttpPost]
        public ActionResult Edit(Doc doc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocTypeId = new SelectList(db.DocTypes, "ID", "DocTypeDescription", doc.DocTypeId);
            return View(doc);
        }

        //
        // GET: /Doc/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Doc doc = db.Docs.Find(id);
            if (doc == null)
            {
                return HttpNotFound();
            }
            return View(doc);
        }

        //
        // POST: /Doc/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Doc doc = db.Docs.Find(id);
            db.Docs.Remove(doc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}