using Calc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class CalcController : Controller
    {
        // GET: Calc
        public ActionResult Index()
        {
            ViewData.Model = new OperationModel();
            return View();
        }
        // ModelBilder - позволяет исользовать отличающиеся имена
        public ActionResult Execute(OperationModel model)
        {
            var calc = new Calc.Calc(new IOperation[] { new SumOperation() });
            var result = calc.Execute(model.Name, model.GetParameters());
            ViewData.Model = $"result = {result}";
            return View();
        }
    }
}