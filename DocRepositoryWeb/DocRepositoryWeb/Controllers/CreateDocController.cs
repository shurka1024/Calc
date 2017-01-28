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
        private static IDocumentRepository docrepository { get; set; }
        private static IUserRepository userrepository { get; set; }

        public CreateDocController()
        {
            if (docrepository == null)
            {
                docrepository = new DocumentRepository();
            }
            if (userrepository == null)
            {
                userrepository = new UserRepository();
            }
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
            var message = "";
            if (file != null)
            {
                var dir = Server.MapPath("~/Files/");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                string fileName = Path.GetFileName(file.FileName);
                string filePath = "~/Files/" + fileName;

                // Создадим запись в таблице БД
                var doc = docrepository.Create();
                doc.Name = fileName;
                doc.AutorId = 1;
                doc.Autor = userrepository.GetUserById(1);
                doc.Date = System.DateTime.Now;
                doc.BinaryFile = filePath;
                message = docrepository.Update(doc);

                file.SaveAs(filePath);
            }
            ViewData.Model = message;
            return View();
        }
    }
}