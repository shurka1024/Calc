using Calc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperOperations
{
    public class Therma : IOperation
    {
        public string Name { get { return "Th"; } }
        public object Execute(object[] args)
        {
            return "Therma";
        }
        public object Execute(object arg)
        {
            return "Can't used";
        }
    }
}
