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
    public class DocIndexController : Controller
    {
        private SimpleDocumentStoreEntities db = new SimpleDocumentStoreEntities();

        //
        // GET: /DocIndex/

        public ActionResult Index()
        {
            return View(db.DocIndexes.ToList());
        }

        //
        // GET: /DocIndex/Details/5

        public ActionResult Details(int id = 0)
        {
            DocIndex docindex = db.DocIndexes.Find(id);
            if (docindex == null)
            {
                return HttpNotFound();
            }
            return View(docindex);
        }

        //
        // GET: /DocIndex/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DocIndex/Create

        [HttpPost]
        public ActionResult Create(DocIndex docindex)
        {
            if (ModelState.IsValid)
            {
                db.DocIndexes.Add(docindex);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(docindex);
        }

        //
        // GET: /DocIndex/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DocIndex docindex = db.DocIndexes.Find(id);
            if (docindex == null)
            {
                return HttpNotFound();
            }
            return View(docindex);
        }

        //
        // POST: /DocIndex/Edit/5

        [HttpPost]
        public ActionResult Edit(DocIndex docindex)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docindex).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(docindex);
        }

        //
        // GET: /DocIndex/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DocIndex docindex = db.DocIndexes.Find(id);
            if (docindex == null)
            {
                return HttpNotFound();
            }
            return View(docindex);
        }

        //
        // POST: /DocIndex/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocIndex docindex = db.DocIndexes.Find(id);
            db.DocIndexes.Remove(docindex);
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