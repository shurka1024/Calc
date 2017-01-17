using Calc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("calc.exe \"Sum\" \"1\" \"2\"");
                Console.ReadKey();
                return;
            }

            var operations = new List<IOperation>();

            #region Получение всех возможных операций
            // Найти файлы dll и exe в текущей директории
            var files = Directory.GetFiles(Environment.CurrentDirectory, "*.dll")   // Найдет все наши dll
            .Union( Directory.GetFiles(Environment.CurrentDirectory, "*.exe"));
            // Загрузить эти файлы
            foreach (var  file in files)
            {
                var assembly = Assembly.LoadFile(file); // Получили сборку
                var types = assembly.GetTypes();

                foreach (var type in types)
                {

                    var interfaces = type.GetInterfaces();
                    // Найти реализацию интерфейса IOperation
                    if (interfaces.Contains(typeof(IOperation)))
                    {
                        Console.WriteLine(type.Name);

                        // Создать экземпляр класса и приводим его к нужному интерфейсу
                        var oper = Activator.CreateInstance(type) as IOperation;    // Приведение типов. Более безопасное. При деудачном приведении null
                        if (oper != null)
                        {
                            operations.Add(oper);
                        }
                    }

                }
            }
            #endregion
            // calc.exe "Sum" "1" "2"

            var calc = new Calc.Calc(operations);

            var activeoper = args[0];
            //var parameters = args.Skip(1).Select(a => (object) a).ToArray();
            var parameters = args.Skip(1).ToArray();

            var result = calc.Execute(activeoper, parameters);
            Console.WriteLine($"result = {result}");

            Console.ReadKey();
        }
    }
}
