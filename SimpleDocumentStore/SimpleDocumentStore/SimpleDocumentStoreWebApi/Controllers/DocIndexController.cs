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
    public class DocIndexController : ApiController
    {
        private SimpleDocumentStoreEntities db = new SimpleDocumentStoreEntities();

        // GET api/DocIndex
        public IEnumerable<DocIndex> GetDocIndexes()
        {
            return db.DocIndexes.AsEnumerable();
        }

        // GET api/DocIndex/5
        public DocIndex GetDocIndex(int id)
        {
            DocIndex docindex = db.DocIndexes.Find(id);
            if (docindex == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return docindex;
        }

        // PUT api/DocIndex/5
        public HttpResponseMessage PutDocIndex(int id, DocIndex docindex)
        {
            if (ModelState.IsValid && id == docindex.Id)
            {
                db.Entry(docindex).State = EntityState.Modified;

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

        // POST api/DocIndex
        public HttpResponseMessage PostDocIndex(DocIndex docindex)
        {
            if (ModelState.IsValid)
            {
                db.DocIndexes.Add(docindex);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, docindex);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = docindex.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/DocIndex/5
        public HttpResponseMessage DeleteDocIndex(int id)
        {
            DocIndex docindex = db.DocIndexes.Find(id);
            if (docindex == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DocIndexes.Remove(docindex);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, docindex);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}