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

        public Calc(IEnumerable<IOperation> opers)     // Конструктор
        {
            operations = opers;                     // Указываем список операций
        }

        public IEnumerable<string> GetOperationNames()
        {
            return operations.Select(o => o.Name);
        }

        // Список всех операций
        private IEnumerable<IOperation> operations { get; set; }

        public object Execute(string name, object[] args)
        {
            // Выбирем первую операцию с именем Name
            var oper = operations.FirstOrDefault(oe => string.Compare(oe.Name, name, true) == 0);
            if (oper == null)
                return $"Operation \" {name}\"not found";
            return oper.Execute(args);  // Выполняется метод с именем Name
        }

    }

    public interface IOperation
    {
        string Name { get; }
        object Execute(object[] args);
        object Execute(object arg);     // Пока нигде не вызывается
    }

    //public interface ISingleOperation : IOperation
    //{
    //    object Execute(object arg);
    //}


    // Реализация метода "Сложение"
    public class SumOperation : IOperation
    {
        public string Name { get { return "Sum"; } }

        public object Execute(object[] args)
        {
            if (Convert.ToString(args[0]) == "" || Convert.ToString(args[1]) == "")
            {
                return "Some parameters not assigned";
            }
            return Convert.ToInt32(args[0]) + Convert.ToInt32(args[1]);
        }
        public object Execute(object arg)
        {
            return "Can't used";
        }
    }

    // Реализация метода "Возведение в степень"
    public class PowerOperation : IOperation
    {
        public string Name { get { return "Power"; } }
        public object Execute(object[] args)
        {
            if (Convert.ToString(args[0]) == "" || Convert.ToString(args[1]) == "")
            {
                return "Some parameters not assigned";
            }
            return System.Math.Pow(Convert.ToDouble(args[0]), Convert.ToDouble(args[1]));
        }
        public object Execute(object arg)
        {
            return "Can't used";
        }
    }

    // Реализация метода "Инкрементация"
    public class IncOperation : IOperation
    {
        public string Name { get { return "Inc"; } }
        public object Execute(object[] args)
        {
            if (Convert.ToString(args[0]) == "")
            {
                return "Parameter not assigned";
            }
            return Convert.ToInt32(args[0]) + 1;
        }
        public object Execute(object arg)
        {
            return "Can't used";
        }
    }

    // Реализация метода "Короткое число Пи"
    public class ShotPiOperation : IOperation
    {
        public string Name { get { return "ShotPi"; } }
        public object Execute(object[] args)
        {
            return 3.14;
        }
        public object Execute(object arg)
        {
            return "Can't used";
        }
    }
}
