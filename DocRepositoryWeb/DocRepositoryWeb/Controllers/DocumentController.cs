using ModelDomainDoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocRepositoryWeb.Controllers
{
    public class DocumentController : Controller
    {
        private static IDocumentRepository repository { get; set; }
        public DocumentController()
        {
            if (repository == null)
            {
                repository = new DocumentRepository();
            }
        }

        // GET: Document
        public ActionResult Index()
        {
            var documents = repository.GetAll()
                .OrderBy(d => d.Name)
                .ThenBy(d => d.Date)
                .ThenBy(d => d.Autor.LastName);
            return View(documents);
        }

        public ActionResult OpenFile(string filePath)
        {
            Response.WriteFile(Server.MapPath("~/Files/MyFile.pdf"));
            //Response.ContentType = "application/pdf";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");
            //Response.TransmitFile(Server.MapPath("~/Files/MyFile.pdf"));
            //Response.End();
            return View("Index");
        }
    }
}