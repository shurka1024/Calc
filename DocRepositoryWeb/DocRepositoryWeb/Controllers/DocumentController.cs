using DocRepositoryWeb.Models;
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
    public class DocumentController : Controller
    {
        private static IDocumentRepository docrepository { get; set; }
        private static IUserRepository userrepository { get; set; }

        public DocumentController()
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

        // GET: Document
        public ActionResult Index(List<Document> documents)                 // Отображает список документов
        {
            if (documents == null)
            {
                documents = docrepository.GetAll()
                    .OrderBy(d => d.Name)
                    .ThenBy(d => d.Date)
                    .ThenBy(d => d.Autor.LastName).ToList();
            }
            return View("Index", documents);
        }

        public void OpenFile(string filePath, string fileName)      // Ссылка для открытия файла
        {
            Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
            Response.TransmitFile(Server.MapPath(filePath));
            Response.End();
        }
        
        [HttpGet]
        public ActionResult CreateDoc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDoc(HttpPostedFileBase file)              // Занесение нового документа в хранилище
        {
            if (ModelState.IsValid)
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
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = "~/Files/" + fileName;

                        // Проверим, может документ с таким наименованием уже есть в хранилище
                        if (docrepository.GetDocByName(fileName) == null)
                        {
                            try
                            {
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
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                message = ex.Message;
                            }
                        }
                        else
                        {
                            message = "Документ с таким наименованием уже существует в хранилище";
                        }
                    }
                }
                ViewData.Model = message;
                return View("Info");
            }
            return View();
        }

        // GET: SearchDoc
        [HttpGet]
        public ActionResult SearchDoc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchDoc(SearchDocModel doc)                   // Поиск по названию
        {
            if (ModelState.IsValid)
            {
                var documents = docrepository.FindByName(doc.Name)
                .OrderBy(d => d.Name)
                .ThenBy(d => d.Date)
                .ThenBy(d => d.Autor.LastName).ToList();
                return Index(documents);
            }
            return View();
        }

        public ActionResult Info()                                          // Вывод информации о результате выполнения
        {
            return View();
        }
    }
}