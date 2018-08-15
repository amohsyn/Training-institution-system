using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Training_Create_UI_Tests : Base_UI_Tests
    {
       

        public Training_Create_UI_Tests()
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
            Default_Form_Training_Model Default_Form_Training_Model = new Default_Form_Training_ModelBLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_Training_Model(Training);



 
			var Code = b.FindElement(By.Id(nameof(Default_Form_Training_Model.Code)));
            Code.SendKeys(Default_Form_Training_Model.Code.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_Training_Model.Description)));
            Description.SendKeys(Default_Form_Training_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
