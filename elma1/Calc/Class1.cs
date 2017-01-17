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

        public Calc(IOperationEmpty[] opers)     // Конструктор
        {
            operations_empty = opers;                     // Указываем список операций
        }
        public Calc(IEnumerable<IOperation> opers)     // Конструктор
        {
            operations = opers;                     // Указываем список операций
        }

        // Список всех операций
        private IEnumerable<IOperation> operations { get; set; }
        private IOperationEmpty[] operations_empty { get; set; }

        public object Execute(string name, object[] args)
        {
            // Выбирем первую операцию с именем Name
            var oper = operations.FirstOrDefault(oe => oe.Name == name);
            if (oper == null)
                return $"Operation \" {name}\"not found";
            return oper.Execute(args);  // Выполняется метод с именем Name
        }

        public object ExecuteEmpty(string name)
        {
            // Выбирем первую операцию с именем Name
            var oper = operations_empty.FirstOrDefault(oe => oe.Name == name);
            return oper.Execute();  // Выполняется метод с именем Name
        }

    }

    public interface IOperation
    {
        string Name { get; }
        object Execute(object[] args);
    }

    public interface IOperationEmpty
    {
        string Name { get; }
        object Execute();
    }

    // Реализация метода "Сложение"
    public class SumOperation : IOperation
    {
        public string Name { get { return "Sum"; } }
        public object Execute(object[] args)
        {
            return (int)args[0] + (int)args[1];
        }
    }

    // Реализация метода "Возведение в степень"
    public class PowerOperation : IOperation
    {
        public string Name { get { return "Power"; } }
        public object Execute(object[] args)
        {
            return System.Math.Pow(Convert.ToDouble(args[0]), Convert.ToDouble(args[1]));
        }
    }

    // Реализация метода "Инкрементация"
    public class IncOperation : IOperation
    {
        public string Name { get { return "Inc"; } }
        public object Execute(object[] args)
        {
            return (int)args[0] + 1;
        }
    }

    // Реализация метода "Короткое число Пи"
    public class ShotPiOperation : IOperationEmpty
    {
        public string Name { get { return "ShotPi"; } }
        public object Execute()
        {
            return 3.14;
        }
    }
}
