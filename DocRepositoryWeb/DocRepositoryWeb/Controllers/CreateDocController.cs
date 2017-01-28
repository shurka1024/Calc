using DomainModel.Helpers;
using ModelDomainDoc.Models;
using ModelDomainDoc.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocRepositoryWeb.Controllers
{
    //[Authorize]
    public class CreateDocController : Controller
    {
        private IDocumentRepository docrepository { get; set; }
        private IUserRepository userrepository { get; set; }

        public CreateDocController()
        {
            docrepository = new DocumentRepository();
            userrepository = new UserRepository();
        }

        // GET: CreateDoc
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Execute(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var dir = Server.MapPath("~/Files/");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string fileName = Path.GetFileName(file.FileName);
                string filePath = dir + fileName;

                // Создадим запись в таблице БД
                var doc = docrepository.Create();
                doc.Name = fileName;
                doc.AutorId = 1;
                doc.Autor = userrepository.GetUserById(1);
                doc.Date = System.DateTime.Now;
                doc.BinaryFile = filePath;
                docrepository.Update(doc);

                file.SaveAs(filePath);
            }
            return View();
        }
    }
}