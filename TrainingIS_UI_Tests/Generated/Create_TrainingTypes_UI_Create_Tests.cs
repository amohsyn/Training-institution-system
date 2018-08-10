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
    public class TrainingType_UI_Index_Tests : Base_UI_Tests
    {
       

        public TrainingType_UI_Index_Tests()
        {
            this.Entity_Path = "/TrainingTypes";
        }
       
        [TestMethod]
        public void TrainingType_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void TrainingType_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            TrainingType TrainingType = new TrainingTypesControllerTests_Service().CreateValideTrainingTypeInstance();
            Default_TrainingTypeFormView Default_TrainingTypeFormView = new Default_TrainingTypeFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_TrainingTypeFormView(TrainingType);



 
			var Code = b.FindElement(By.Id(nameof(Default_TrainingTypeFormView.Code)));
            Code.SendKeys(Default_TrainingTypeFormView.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_TrainingTypeFormView.Name)));
            Name.SendKeys(Default_TrainingTypeFormView.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_TrainingTypeFormView.Description)));
            Description.SendKeys(Default_TrainingTypeFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
