using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.BLL.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Training_UI_Index_Tests : Base_UI_Tests
    {
       

        public Training_UI_Index_Tests()
        {
            this.Entity_Path = "/Trainings";
        }
       
        [TestMethod]
        public void Training_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void Training_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Training Training = new TrainingsControllerTests_Service().CreateValideTrainingInstance();
            TrainingFormView TrainingFormView = new TrainingFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_TrainingFormView(Training);



			string xpath_TrainingYearId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TrainingYearId", TrainingFormView.TrainingYearId.ToString());
            b.FindElement(By.XPath(xpath_TrainingYearId)).Click(); 

 
			var SpecialtyId = b.FindElement(By.Id(nameof(TrainingFormView.SpecialtyId)));
            SpecialtyId.SendKeys(TrainingFormView.SpecialtyId.ToString());

			string xpath_ModuleTrainingId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ModuleTrainingId", TrainingFormView.ModuleTrainingId.ToString());
            b.FindElement(By.XPath(xpath_ModuleTrainingId)).Click(); 

			string xpath_FormerId = string.Format("//select[@id='{0}']/option[@value='{1}']", "FormerId", TrainingFormView.FormerId.ToString());
            b.FindElement(By.XPath(xpath_FormerId)).Click(); 

			string xpath_GroupId = string.Format("//select[@id='{0}']/option[@value='{1}']", "GroupId", TrainingFormView.GroupId.ToString());
            b.FindElement(By.XPath(xpath_GroupId)).Click(); 

 
			var Code = b.FindElement(By.Id(nameof(TrainingFormView.Code)));
            Code.SendKeys(TrainingFormView.Code.ToString());

 
			var Description = b.FindElement(By.Id(nameof(TrainingFormView.Description)));
            Description.SendKeys(TrainingFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
