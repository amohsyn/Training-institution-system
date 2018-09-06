using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.Context.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.UnitTest.DataAnnotations;

namespace TrainingIS.Context.Services.Tests
{
    [TestClass()]
    [CleanTestDB]
    public class GApp_Dev_Controllers_ContextTests
    {
        [TestMethod()]
        public void Get_Controllers_TypesTest()
        {
            GApp_Dev_Controllers_Context GApp_Dev_Controllers_Context = new GApp_Dev_Controllers_Context();
            var controllers_types = GApp_Dev_Controllers_Context.Get_Controllers_Types();

            // Assert 
            foreach (Type item in controllers_types)
            {
                    // The base controller is not a controller
                    Assert.IsTrue(!item.Name.StartsWith("Base"));
            }

        }
    }
}