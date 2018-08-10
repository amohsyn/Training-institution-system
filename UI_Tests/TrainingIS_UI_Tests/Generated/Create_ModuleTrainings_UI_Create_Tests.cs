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
    public class ModuleTraining_UI_Index_Tests : Base_UI_Tests
    {
       

        public ModuleTraining_UI_Index_Tests()
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

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            ModuleTraining ModuleTraining = new ModuleTrainingsControllerTests_Service().CreateValideModuleTrainingInstance();
            Default_ModuleTrainingFormView Default_ModuleTrainingFormView = new Default_ModuleTrainingFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_ModuleTrainingFormView(ModuleTraining);



			string xpath_SpecialtyId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SpecialtyId", Default_ModuleTrainingFormView.SpecialtyId.ToString());
            b.FindElement(By.XPath(xpath_SpecialtyId)).Click(); 

 
			var Name = b.FindElement(By.Id(nameof(Default_ModuleTrainingFormView.Name)));
            Name.SendKeys(Default_ModuleTrainingFormView.Name.ToString());

 
			var Code = b.FindElement(By.Id(nameof(Default_ModuleTrainingFormView.Code)));
            Code.SendKeys(Default_ModuleTrainingFormView.Code.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_ModuleTrainingFormView.Description)));
            Description.SendKeys(Default_ModuleTrainingFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
