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
    public class ModuleTraining_Create_UI_Tests : Base_UI_Tests
    {
       

        public ModuleTraining_Create_UI_Tests()
        {
            this.Entity_Path = "/ModuleTrainings";
        }
       
        [TestMethod]
        public void ModuleTraining_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void ModuleTraining_Create_Test()
        {
            this.GoTo_Index();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            ModuleTraining ModuleTraining = new ModuleTrainingsControllerTests_Service().CreateValideModuleTrainingInstance(null,GAppContext);
            Default_Form_ModuleTraining_Model Default_Form_ModuleTraining_Model = new Default_Form_ModuleTraining_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_ModuleTraining_Model(ModuleTraining);



			string xpath_SpecialtyId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SpecialtyId", Default_Form_ModuleTraining_Model.SpecialtyId.ToString());
            b.FindElement(By.XPath(xpath_SpecialtyId)).Click(); 

 
			var Name = b.FindElement(By.Id(nameof(Default_Form_ModuleTraining_Model.Name)));
            Name.SendKeys(Default_Form_ModuleTraining_Model.Name.ToString());

 
			var Code = b.FindElement(By.Id(nameof(Default_Form_ModuleTraining_Model.Code)));
            Code.SendKeys(Default_Form_ModuleTraining_Model.Code.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_ModuleTraining_Model.Description)));
            Description.SendKeys(Default_Form_ModuleTraining_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
