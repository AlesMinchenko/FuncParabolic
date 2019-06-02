using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.Stubs;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private StubChartRepository stubChartRepository;
        public UnitTest1()
        {

        }
        [TestMethod]
        public void Get_ChartFromRepository_IsNotNull()
        {
            stubChartRepository = new StubChartRepository();
            var chart = stubChartRepository.Get(1);

            Assert.IsNotNull(chart);
            Assert.AreEqual("ChartName1", chart.Name);
        }
    }
}
