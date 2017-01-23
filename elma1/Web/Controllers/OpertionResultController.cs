using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class OpertionResultController : Controller
    {
        private IOperationResultRepository repository { get; set; }

        public OpertionResultController()
        {
            repository = new OperationResultRepository();
        }


        // GET: OpertionResult
        public ActionResult Index()
        {
            // нужно фильтровать операции - выводить только те, которые выполнялись дольше 1 секунды
            var operations = repository.GetByFilter(1000);
            return View(operations);
        }
    }
}