using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        Stubs.StubUserDataRepository1 stubUserDataRepository;
        [TestMethod]
        public void TestMethod1()
        {
            stubUserDataRepository = new Stubs.StubUserDataRepository1();

            var userData = stubUserDataRepository.Get(3);

            Assert.IsNotNull(userData);
            Assert.AreEqual(3, userData.Id);
        }
    }
}
