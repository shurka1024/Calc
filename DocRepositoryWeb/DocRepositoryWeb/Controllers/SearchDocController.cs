using DocRepositoryWeb.Models;
using ModelDomainDoc.Models;
using ModelDomainDoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocRepositoryWeb.Controllers
{
    [Authorize]
    public class SearchDocController : Controller
    {
        private static IDocumentRepository repository { get; set; }
        public SearchDocController()
        {
            if(repository == null)
            {
                repository = new DocumentRepository();
            }
        }

        // GET: SearchDoc
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Registr(SearchDocModel doc)
        {
            if (ModelState.IsValid)
            {
                ViewData.Model = doc;
                return Execute(doc);
            }
            return View();
        }

        public ActionResult Execute(SearchDocModel doc)
        {
            var documents = repository.FindByName(doc.Name)
                .OrderBy(d => d.Name)
                .ThenBy(d => d.Date)
                .ThenBy(d => d.Autor.LastName).ToList();
            return View("Execute", documents);
        }

        public ActionResult OpenFile(string filePath, string fileName)
        {
            Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
            Response.TransmitFile(Server.MapPath(filePath));
            Response.End();
            return Index();
        }
    }
}