using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Func.Web.Controllers;
using Func.BLL.Interfaces;
using System.Web.Mvc;
namespace UnitTest.Controllers
{
    /// <summary>
    /// Сводное описание для UserDataContollerTest
    /// </summary>
    [TestClass]
    public class UserDataContollerTest
    {
        UserDataController userDataController = null;
        IFuncService func;

        public UserDataContollerTest()
        {
            userDataController = new UserDataController(func);
        }
        [TestMethod]
        public void ShowChartViewResultNotNull()
        {

            ViewResult result = userDataController.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void IndexViewResultNotNull()
        {

            ViewResult result = userDataController.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void IndexViewNameEqualIndexCshtmlName()
        {
            ViewResult result = userDataController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }
        [TestMethod]
        public void IndexStringInViewbag()
        {
            ViewResult result = userDataController.Index() as ViewResult;

            Assert.AreEqual("MessageForTest", result.ViewBag.MessageForTest);
        }
    }
}
