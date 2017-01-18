using Calc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplicationOperation
{
    public class Multiplication : IOperation
    {
        public string Name { get { return "Mul"; } }
        public object Execute(object[] args)
        {
            //var firstel = Convert.ToInt32(args[0]);
            //var secondel = Convert.ToInt32(args[1]);
            return Convert.ToInt32(args[0]) * Convert.ToInt32(args[1]);
        }
        public object Execute(object arg)
        {
            var element = Convert.ToInt32(arg);
            return element * element;
        }
    }
}
