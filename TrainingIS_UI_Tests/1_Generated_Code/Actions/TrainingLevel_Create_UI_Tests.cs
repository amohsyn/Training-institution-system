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
using TrainingIS.WebApp.Tests.Services;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class TrainingLevel_Create_UI_Tests : Base_UI_Tests
    {
       

        public TrainingLevel_Create_UI_Tests()
        {
            this.Entity_Path = "/TrainingLevels";
        }
       
        [TestMethod]
        public void TrainingLevel_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void TrainingLevel_Create_Test()
        {
            this.GoTo_Index();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            TrainingLevel TrainingLevel = new TrainingLevelsControllerTests_Service().CreateValideTrainingLevelInstance(null,GAppContext);
            Default_Form_TrainingLevel_Model Default_Form_TrainingLevel_Model = new Default_Form_TrainingLevel_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_TrainingLevel_Model(TrainingLevel);



 
			var Code = b.FindElement(By.Id(nameof(Default_Form_TrainingLevel_Model.Code)));
            Code.SendKeys(Default_Form_TrainingLevel_Model.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_Form_TrainingLevel_Model.Name)));
            Name.SendKeys(Default_Form_TrainingLevel_Model.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_TrainingLevel_Model.Description)));
            Description.SendKeys(Default_Form_TrainingLevel_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
