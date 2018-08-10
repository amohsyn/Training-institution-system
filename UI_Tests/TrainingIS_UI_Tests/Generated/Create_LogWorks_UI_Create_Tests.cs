using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class LogWork_UI_Index_Tests : Base_UI_Tests
    {
       

        public LogWork_UI_Index_Tests()
        {
            this.Entity_Path = "/LogWorks";
        }
       
        [TestMethod]
        public void LogWork_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void LogWork_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            LogWork LogWork = new LogWorksControllerTests_Service().CreateValideLogWorkInstance();
            Default_LogWorkFormView Default_LogWorkFormView = new Default_LogWorkFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_LogWorkFormView(LogWork);



 
			var UserId = b.FindElement(By.Id(nameof(Default_LogWorkFormView.UserId)));
            UserId.SendKeys(Default_LogWorkFormView.UserId.ToString());

  			string xpath_OperationWorkType = string.Format("//select[@id='{0}']/option[@value='{1}']", "OperationWorkType", Default_LogWorkFormView.OperationWorkType.ToString());
            b.FindElement(By.XPath(xpath_OperationWorkType)).Click();

 
			var OperationReference = b.FindElement(By.Id(nameof(Default_LogWorkFormView.OperationReference)));
            OperationReference.SendKeys(Default_LogWorkFormView.OperationReference.ToString());

 
			var EntityType = b.FindElement(By.Id(nameof(Default_LogWorkFormView.EntityType)));
            EntityType.SendKeys(Default_LogWorkFormView.EntityType.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_LogWorkFormView.Description)));
            Description.SendKeys(Default_LogWorkFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
