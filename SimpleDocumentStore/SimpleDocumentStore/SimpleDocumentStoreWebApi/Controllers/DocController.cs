using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SimpleDocumentStoreWebApi.Models;

namespace SimpleDocumentStoreWebApi.Controllers
{
    public class DocController : ApiController
    {
        private SimpleDocumentStoreEntities db = new SimpleDocumentStoreEntities();

        // GET api/Doc
        public IEnumerable<Doc> GetDocs()
        {
            var docs = db.Docs.Include(d => d.DocType);
            return docs.AsEnumerable();
        }

        // GET api/Doc/5
        public Doc GetDoc(int id)
        {
            Doc doc = db.Docs.Find(id);
            if (doc == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return doc;
        }

        // PUT api/Doc/5
        public HttpResponseMessage PutDoc(int id, Doc doc)
        {
            if (ModelState.IsValid && id == doc.Id)
            {
                db.Entry(doc).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Doc
        public HttpResponseMessage PostDoc(Doc doc)
        {
            if (ModelState.IsValid)
            {
                db.Docs.Add(doc);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, doc);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = doc.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Doc/5
        public HttpResponseMessage DeleteDoc(int id)
        {
            Doc doc = db.Docs.Find(id);
            if (doc == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Docs.Remove(doc);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, doc);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}