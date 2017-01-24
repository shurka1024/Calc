using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;

namespace Controllers
{
    public class OpertionResultController : Controller
    {
        private IOperationResultRepository repository { get; set; }

        public OpertionResultController()
        {
            repository = new NHOperationResultRepository();
        }


        // GET: OpertionResult
        public ActionResult Index()
        {
            // нужно фильтровать операции - выводить только те, которые выполнялись дольше 1 секунды
            //var operations = repository.GetByFilter(1000);
            var operations = repository.GetAll();
            return View(operations);
        }
    }
}