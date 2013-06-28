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
    public class DocTypeConfigurationController : ApiController
    {
        private SimpleDocumentStoreEntities db = new SimpleDocumentStoreEntities();

        // GET api/DocTypeConfiguration
        public IEnumerable<DocTypeConfiguration> GetDocTypeConfigurations()
        {
            var doctypeconfigurations = db.DocTypeConfigurations.Include(d => d.DocIndex).Include(d => d.DocType);
            return doctypeconfigurations.AsEnumerable();
        }

        // GET api/DocTypeConfiguration/5
        public DocTypeConfiguration GetDocTypeConfiguration(int id)
        {
            DocTypeConfiguration doctypeconfiguration = db.DocTypeConfigurations.Find(id);
            if (doctypeconfiguration == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return doctypeconfiguration;
        }

        // PUT api/DocTypeConfiguration/5
        public HttpResponseMessage PutDocTypeConfiguration(int id, DocTypeConfiguration doctypeconfiguration)
        {
            if (ModelState.IsValid && id == doctypeconfiguration.Id)
            {
                db.Entry(doctypeconfiguration).State = EntityState.Modified;

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

        // POST api/DocTypeConfiguration
        public HttpResponseMessage PostDocTypeConfiguration(DocTypeConfiguration doctypeconfiguration)
        {
            if (ModelState.IsValid)
            {
                db.DocTypeConfigurations.Add(doctypeconfiguration);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, doctypeconfiguration);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = doctypeconfiguration.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/DocTypeConfiguration/5
        public HttpResponseMessage DeleteDocTypeConfiguration(int id)
        {
            DocTypeConfiguration doctypeconfiguration = db.DocTypeConfigurations.Find(id);
            if (doctypeconfiguration == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DocTypeConfigurations.Remove(doctypeconfiguration);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, doctypeconfiguration);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}