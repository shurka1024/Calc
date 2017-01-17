using Calc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class PiOperation : IOperation
    {
        public string Name { get { return "Pi"; } }
        public object Execute(object[] args)
        {
            return Math.PI;
        }
        public object Execute(object arg)
        {
            return "Can't used";
        }
    }
}
