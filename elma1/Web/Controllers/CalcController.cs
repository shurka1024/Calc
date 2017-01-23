using Calc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class CalcController : Controller
    {
        private IOperationResultRepository repository {get; set;}

        public CalcController()
        {
            repository = new OperationResultRepository();
        }

        // GET: Calc
        public ActionResult Index()
        {
            ViewData.Model = new OperationModel();
            var operations = new List<IOperation>();
            GetOperations(ref operations);

            var listoper = new List<SelectListItem>();

            foreach (var oper in operations)
            {
                listoper.Add(new SelectListItem() { Text = oper.Name, Value = oper.Name });
            }


            ViewData["Operation"] = listoper;
            return View();
        }
        // ModelBilder - позволяет исользовать отличающиеся имена
        public ActionResult Execute(OperationModel model)
        {

            
            var operations = new List<IOperation>();

            GetOperations(ref operations);

            var calc = new Calc.Calc(operations);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = calc.Execute(Convert.ToString(model.Name), model.GetParameters());

            stopWatch.Stop();

            var operResult = repository.Create();
            operResult.ArgumentCount = model.GetParameters().Count();
            operResult.Arguments = string.Join(",", model.GetParameters());
            operResult.OperationId = repository.FindOperByName(model.Name).Id;
            operResult.Result = result.ToString();
            operResult.ExecTime_ms = stopWatch.ElapsedMilliseconds;

            repository.Update(operResult);

            ViewData.Model = $"result = {result}";
            return View();
        }
        public void GetOperations(ref List<IOperation> operations)
        {
            var folderName = Path.GetDirectoryName(Server.MapPath("~/App_Data/Calc.dll"));
            var files = Directory.GetFiles(folderName, "*.dll");
            foreach (var file in files)
            {
                var types = Assembly.LoadFile(file).GetTypes(); // Получили сборку и ее типы
                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Contains(typeof(IOperation)))
                        {
                            var oper = Activator.CreateInstance(type) as IOperation;
                            if (oper != null)
                            {
                                operations.Add(oper);
                            }
                        }
                }
            }
        }
    }
}