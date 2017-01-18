using Calc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            //var calc = new Calc.Calc(GetOperations());
            var result = calc.Execute(Convert.ToString(model.Name), model.GetParameters());
            ViewData.Model = $"result = {result}";
            return View();
        }
        public void GetOperations(ref List<IOperation> operations)
        {
            //var operations = new List<SelectListItem>();
            var folderName = Path.GetDirectoryName(Server.MapPath("~/App_Data/Calc.dll"));
            var files = Directory.GetFiles(folderName, "*.dll");
            //var files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "*.dll");
            foreach (var file in files)
            {
                var types = Assembly.LoadFile(file).GetTypes(); // Получили сборку и ее типы
                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();
                    // Почему то условие не выполняется для файла Calc.dll!!!!!!!!!!!!!!!!
                    if (interfaces.Contains(typeof(IOperation)) && !type.IsInterface)
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