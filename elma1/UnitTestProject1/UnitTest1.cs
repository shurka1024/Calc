using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C = Calc;

namespace UnitTestProject1
{
    /// <summary>
    /// Тестирование Calc
    /// </summary>
    [TestClass]             // Атрибут. Помечает классы для тестирования
    public class CalcUnitTest
    {
        [TestMethod]        // Метод тестер
        public void SumTest()
        {
            var calc = new C.Calc(new C.IOperation[] { new C.SumOperation() });          // Объявление экземпляра класса
            var result_sum = calc.Execute("Sum", new object[] { 1, 2 });
            Assert.AreEqual(result_sum, 3);
        }

        [TestMethod]
        public void PowerTest()
        {
            var calc = new C.Calc(new C.IOperation[] { new C.PowerOperation() });          // Объявление экземпляра класса
            var result = calc.Execute("Power", new object[] { 2, 3 });
            Assert.AreEqual(result, (double)8);
        }

        [TestMethod]
        public void IncTest()
        {
            var calc = new C.Calc(new C.IOperation[] { new C.IncOperation() });
            var result = calc.Execute("Inc", new object[] { 4 });
            Assert.AreEqual(result, 5);
        }

        [TestMethod]
        public void ShotPiTest()
        {
            var calc = new C.Calc(new C.IOperation[] { new C.ShotPiOperation() });
            var result = calc.Execute("ShotPi", new object[] { 4 });
            Assert.AreEqual(result, 3.14);
        }
    }
}
