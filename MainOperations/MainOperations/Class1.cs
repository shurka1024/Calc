using Calc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOperations
{
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
    }

    // Реализация метода "Короткое число Пи"
    public class ShotPiOperation : IOperation
    {
        public string Name { get { return "ShotPi"; } }
        public object Execute(object[] args)
        {
            return 3.14;
        }
    }

    public class DivOperation : IOperationCount
    {
        public int Count { get { return 1; } }
        public string Name { get { return "ShotPi"; } }
        public object Execute(object[] args)
        {
            return "DivResult";
        }
    }
}
