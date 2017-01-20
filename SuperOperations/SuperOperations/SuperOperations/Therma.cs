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

    public class Sum2 : IOperation
    {
        public string Name { get { return "Sum2"; } }
        public object Execute(object[] args)
        {
            return Convert.ToInt32(args[0]) + Convert.ToInt32(args[1]); ;
        }
        public object Execute(object arg)
        {
            return "Can't used";
        }
    }

    //public class DivOperation2 : IOperationCount
    //{
    //    public int Count { get { return 1; } }
    //    public string Name { get { return "ShotPi"; } }
    //    public object Execute(object[] args)
    //    {
    //        return "DivResult";
    //    }
    //}
}
