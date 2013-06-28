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
    public class DocTypeController : ApiController
    {
        private SimpleDocumentStoreEntities db = new SimpleDocumentStoreEntities();

        // GET api/DocType
        public IEnumerable<DocType> GetDocTypes()
        {
            return db.DocTypes.AsEnumerable();
        }

        // GET api/DocType/5
        public DocType GetDocType(int id)
        {
            DocType doctype = db.DocTypes.Find(id);
            if (doctype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return doctype;
        }

        // PUT api/DocType/5
        public HttpResponseMessage PutDocType(int id, DocType doctype)
        {
            if (ModelState.IsValid && id == doctype.ID)
            {
                db.Entry(doctype).State = EntityState.Modified;

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

        // POST api/DocType
        public HttpResponseMessage PostDocType(DocType doctype)
        {
            if (ModelState.IsValid)
            {
                db.DocTypes.Add(doctype);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, doctype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = doctype.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/DocType/5
        public HttpResponseMessage DeleteDocType(int id)
        {
            DocType doctype = db.DocTypes.Find(id);
            if (doctype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DocTypes.Remove(doctype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, doctype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}