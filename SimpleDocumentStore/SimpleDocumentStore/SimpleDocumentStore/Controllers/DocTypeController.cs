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
    public class DocTypeController : Controller
    {
        private SimpleDocumentStoreEntities db = new SimpleDocumentStoreEntities();

        //
        // GET: /DocType/

        public ActionResult Index()
        {
            return View(db.DocTypes.ToList());
        }

        //
        // GET: /DocType/Details/5

        public ActionResult Details(int id = 0)
        {
            DocType doctype = db.DocTypes.Find(id);
            if (doctype == null)
            {
                return HttpNotFound();
            }
            return View(doctype);
        }

        //
        // GET: /DocType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DocType/Create

        [HttpPost]
        public ActionResult Create(DocType doctype)
        {
            if (ModelState.IsValid)
            {
                db.DocTypes.Add(doctype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctype);
        }

        //
        // GET: /DocType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DocType doctype = db.DocTypes.Find(id);
            if (doctype == null)
            {
                return HttpNotFound();
            }
            return View(doctype);
        }

        //
        // POST: /DocType/Edit/5

        [HttpPost]
        public ActionResult Edit(DocType doctype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctype);
        }

        //
        // GET: /DocType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DocType doctype = db.DocTypes.Find(id);
            if (doctype == null)
            {
                return HttpNotFound();
            }
            return View(doctype);
        }

        //
        // POST: /DocType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocType doctype = db.DocTypes.Find(id);
            db.DocTypes.Remove(doctype);
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