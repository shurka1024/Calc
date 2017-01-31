using DomainModel.Helpers;
using Microsoft.AspNet.Identity;
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
    [Authorize]
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
        public ActionResult Registr(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                ViewData.Model = file;
                return Execute(file);
            }
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
                using (var transaction = NHibernateHelper.OpenSession().BeginTransaction())
                {
                    try
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = "~/Files/" + fileName;

                        var user = userrepository.GetUserByLogin(User.Identity.GetUserName());

                        // Создадим запись в таблице БД
                        var doc = docrepository.Create();
                        doc.Name = fileName;
                        doc.AutorId = user.Id;
                        doc.Autor = user;
                        doc.Date = System.DateTime.Now;
                        doc.BinaryFile = filePath;
                        message = docrepository.Update(doc);

                        file.SaveAs(Server.MapPath(filePath));
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        message = ex.Message;
                    }
                }
            }
            ViewData.Model = message;
            return View("Execute");
        }
    }
}