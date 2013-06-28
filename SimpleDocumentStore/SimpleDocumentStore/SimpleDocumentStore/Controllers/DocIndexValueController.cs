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
    public class DocIndexValueController : Controller
    {
        private SimpleDocumentStoreEntities db = new SimpleDocumentStoreEntities();

        //
        // GET: /DocIndexValue/

        public ActionResult Index()
        {
            var docindexvalues = db.DocIndexValues.Include(d => d.Doc).Include(d => d.DocIndex);
            return View(docindexvalues.ToList());
        }

        //
        // GET: /DocIndexValue/Details/5

        public ActionResult Details(int id = 0)
        {
            DocIndexValue docindexvalue = db.DocIndexValues.Find(id);
            if (docindexvalue == null)
            {
                return HttpNotFound();
            }
            return View(docindexvalue);
        }

        //
        // GET: /DocIndexValue/Create

        public ActionResult Create()
        {
            ViewBag.DocId = new SelectList(db.Docs, "Id", "SystemDocId");
            ViewBag.DocIndexId = new SelectList(db.DocIndexes, "Id", "DocIndexFieldName");
            return View();
        }

        //
        // POST: /DocIndexValue/Create

        [HttpPost]
        public ActionResult Create(DocIndexValue docindexvalue)
        {
            if (ModelState.IsValid)
            {
                db.DocIndexValues.Add(docindexvalue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocId = new SelectList(db.Docs, "Id", "SystemDocId", docindexvalue.DocId);
            ViewBag.DocIndexId = new SelectList(db.DocIndexes, "Id", "DocIndexFieldName", docindexvalue.DocIndexId);
            return View(docindexvalue);
        }

        //
        // GET: /DocIndexValue/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DocIndexValue docindexvalue = db.DocIndexValues.Find(id);
            if (docindexvalue == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocId = new SelectList(db.Docs, "Id", "SystemDocId", docindexvalue.DocId);
            ViewBag.DocIndexId = new SelectList(db.DocIndexes, "Id", "DocIndexFieldName", docindexvalue.DocIndexId);
            return View(docindexvalue);
        }

        //
        // POST: /DocIndexValue/Edit/5

        [HttpPost]
        public ActionResult Edit(DocIndexValue docindexvalue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docindexvalue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocId = new SelectList(db.Docs, "Id", "SystemDocId", docindexvalue.DocId);
            ViewBag.DocIndexId = new SelectList(db.DocIndexes, "Id", "DocIndexFieldName", docindexvalue.DocIndexId);
            return View(docindexvalue);
        }

        //
        // GET: /DocIndexValue/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DocIndexValue docindexvalue = db.DocIndexValues.Find(id);
            if (docindexvalue == null)
            {
                return HttpNotFound();
            }
            return View(docindexvalue);
        }

        //
        // POST: /DocIndexValue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocIndexValue docindexvalue = db.DocIndexValues.Find(id);
            db.DocIndexValues.Remove(docindexvalue);
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