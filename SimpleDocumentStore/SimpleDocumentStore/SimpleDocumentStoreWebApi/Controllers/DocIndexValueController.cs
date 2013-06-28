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
    public class DocIndexValueController : ApiController
    {
        private SimpleDocumentStoreEntities db = new SimpleDocumentStoreEntities();

        // GET api/DocIndexValue
        public IEnumerable<DocIndexValue> GetDocIndexValues()
        {
            var docindexvalues = db.DocIndexValues.Include(d => d.Doc).Include(d => d.DocIndex);
            return docindexvalues.AsEnumerable();
        }

        // GET api/DocIndexValue/5
        public DocIndexValue GetDocIndexValue(int id)
        {
            DocIndexValue docindexvalue = db.DocIndexValues.Find(id);
            if (docindexvalue == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return docindexvalue;
        }

        // PUT api/DocIndexValue/5
        public HttpResponseMessage PutDocIndexValue(int id, DocIndexValue docindexvalue)
        {
            if (ModelState.IsValid && id == docindexvalue.Id)
            {
                db.Entry(docindexvalue).State = EntityState.Modified;

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

        // POST api/DocIndexValue
        public HttpResponseMessage PostDocIndexValue(DocIndexValue docindexvalue)
        {
            if (ModelState.IsValid)
            {
                db.DocIndexValues.Add(docindexvalue);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, docindexvalue);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = docindexvalue.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/DocIndexValue/5
        public HttpResponseMessage DeleteDocIndexValue(int id)
        {
            DocIndexValue docindexvalue = db.DocIndexValues.Find(id);
            if (docindexvalue == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DocIndexValues.Remove(docindexvalue);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, docindexvalue);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}