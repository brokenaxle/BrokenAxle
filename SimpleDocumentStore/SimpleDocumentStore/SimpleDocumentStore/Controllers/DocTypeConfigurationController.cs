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
    public class DocTypeConfigurationController : Controller
    {
        private SimpleDocumentStoreEntities db = new SimpleDocumentStoreEntities();

        //
        // GET: /DocTypeConfiguration/

        public ActionResult Index()
        {
            var doctypeconfigurations = db.DocTypeConfigurations.Include(d => d.DocIndex).Include(d => d.DocType);
            return View(doctypeconfigurations.ToList());
        }

        //
        // GET: /DocTypeConfiguration/Details/5

        public ActionResult Details(int id = 0)
        {
            DocTypeConfiguration doctypeconfiguration = db.DocTypeConfigurations.Find(id);
            if (doctypeconfiguration == null)
            {
                return HttpNotFound();
            }
            return View(doctypeconfiguration);
        }

        //
        // GET: /DocTypeConfiguration/Create

        public ActionResult Create()
        {
            ViewBag.DocIndexId = new SelectList(db.DocIndexes, "Id", "DocIndexFieldName");
            ViewBag.DocTypeId = new SelectList(db.DocTypes, "ID", "DocTypeDescription");
            return View();
        }

        //
        // POST: /DocTypeConfiguration/Create

        [HttpPost]
        public ActionResult Create(DocTypeConfiguration doctypeconfiguration)
        {
            if (ModelState.IsValid)
            {
                db.DocTypeConfigurations.Add(doctypeconfiguration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocIndexId = new SelectList(db.DocIndexes, "Id", "DocIndexFieldName", doctypeconfiguration.DocIndexId);
            ViewBag.DocTypeId = new SelectList(db.DocTypes, "ID", "DocTypeDescription", doctypeconfiguration.DocTypeId);
            return View(doctypeconfiguration);
        }

        //
        // GET: /DocTypeConfiguration/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DocTypeConfiguration doctypeconfiguration = db.DocTypeConfigurations.Find(id);
            if (doctypeconfiguration == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocIndexId = new SelectList(db.DocIndexes, "Id", "DocIndexFieldName", doctypeconfiguration.DocIndexId);
            ViewBag.DocTypeId = new SelectList(db.DocTypes, "ID", "DocTypeDescription", doctypeconfiguration.DocTypeId);
            return View(doctypeconfiguration);
        }

        //
        // POST: /DocTypeConfiguration/Edit/5

        [HttpPost]
        public ActionResult Edit(DocTypeConfiguration doctypeconfiguration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctypeconfiguration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocIndexId = new SelectList(db.DocIndexes, "Id", "DocIndexFieldName", doctypeconfiguration.DocIndexId);
            ViewBag.DocTypeId = new SelectList(db.DocTypes, "ID", "DocTypeDescription", doctypeconfiguration.DocTypeId);
            return View(doctypeconfiguration);
        }

        //
        // GET: /DocTypeConfiguration/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DocTypeConfiguration doctypeconfiguration = db.DocTypeConfigurations.Find(id);
            if (doctypeconfiguration == null)
            {
                return HttpNotFound();
            }
            return View(doctypeconfiguration);
        }

        //
        // POST: /DocTypeConfiguration/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocTypeConfiguration doctypeconfiguration = db.DocTypeConfigurations.Find(id);
            db.DocTypeConfigurations.Remove(doctypeconfiguration);
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