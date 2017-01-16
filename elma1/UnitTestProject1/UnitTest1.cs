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
            var calc = new C.Calc();          // Объявление экземпляра класса
            var result = calc.Sum(1, 2);
            Assert.AreEqual(result, 3);
        }
    }
}
