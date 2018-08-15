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
    public class SeancePlanning_Create_UI_Tests : Base_UI_Tests
    {
       

        public SeancePlanning_Create_UI_Tests()
        {
            this.Entity_Path = "/SeancePlannings";
        }
       
        [TestMethod]
        public void SeancePlanning_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void SeancePlanning_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            SeancePlanning SeancePlanning = new SeancePlanningsControllerTests_Service().CreateValideSeancePlanningInstance();
            Default_Form_SeancePlanning_Model Default_Form_SeancePlanning_Model = new Default_Form_SeancePlanning_ModelBLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_SeancePlanning_Model(SeancePlanning);



 
			var Description = b.FindElement(By.Id(nameof(Default_Form_SeancePlanning_Model.Description)));
            Description.SendKeys(Default_Form_SeancePlanning_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
