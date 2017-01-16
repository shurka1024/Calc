using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    public class Calc
    {
        public int Sum(int x, int y)        // Сигнатура
        {
            return (int)Execute("Sum", new object[] { x, y });
        }

        public Calc(IOperation[] opers)     // Конструктор
        {
            operations = opers;                     // Указываем список операций
        }

        // Список всех операций
        private IOperation[] operations { get; set; }

        public object Execute(string name, object[] args)
        {
            // Выбирем первую операцию с именем Name
            var oper = operations.FirstOrDefault(o => o.Name == name);
            return oper.Execute(args);  // Выполняется метод с именем Name
        }

    }

    public interface IOperation
    {
        string Name { get; }
        object Execute(object[] args);
    }

    public class SumOperation : IOperation
    {
        public string Name { get { return "Sum"; } }
        public object Execute(object[] args)
        {
            return (int)args[0] + (int)args[1];
        }
    }
}
