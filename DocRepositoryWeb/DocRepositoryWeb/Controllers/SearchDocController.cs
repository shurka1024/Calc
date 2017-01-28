using ModelDomainDoc.Models;
using ModelDomainDoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocRepositoryWeb.Controllers
{
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

        public ActionResult Execute(Document doc)
        {
            var documents = repository.SearchByName(doc.Name)
                .OrderBy(d => d.Name)
                .ThenBy(d => d.Date)
                .ThenBy(d => d.Autor.LastName);
            return View(documents);
        }
    }
}