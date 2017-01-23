using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    public class CalcService
    {
        private static readonly Lazy<CalcService> lazy =
            new Lazy<CalcService>(() => new CalcService());

        public Calc Calculator { get; private set; }

        public string Name { get; private set; }

        private CalcService()
        {
            Name = System.Guid.NewGuid().ToString();
        }

        public void LoadOperations(string pathToDLL)
        {
            var operations = new List<IOperation>();
            //var folderName = Path.GetDirectoryName(Server.MapPath("~/App_Data/Calc.dll"));
            var files = Directory.GetFiles(pathToDLL, "*.dll");
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


            Calculator = new Calc(operations);
        }

        public static CalcService GetInstance()
        {
            return lazy.Value;
        }
    }
}
