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
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class SeanceTraining_Create_UI_Tests : Base_UI_Tests
    {
       

        public SeanceTraining_Create_UI_Tests()
        {
            this.Entity_Path = "/SeanceTrainings";
        }
       
        [TestMethod]
        public void SeanceTraining_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void SeanceTraining_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            SeanceTraining SeanceTraining = new SeanceTrainingsControllerTests_Service().CreateValideSeanceTrainingInstance();
            Default_Form_SeanceTraining_Model Default_Form_SeanceTraining_Model = new Default_Form_SeanceTraining_ModelBLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_SeanceTraining_Model(SeanceTraining);



 
			var SeanceDate = b.FindElement(By.Id(nameof(Default_Form_SeanceTraining_Model.SeanceDate)));
            SeanceDate.SendKeys(Default_Form_SeanceTraining_Model.SeanceDate.ToString());

			string xpath_SeancePlanningId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SeancePlanningId", Default_Form_SeanceTraining_Model.SeancePlanningId.ToString());
            b.FindElement(By.XPath(xpath_SeancePlanningId)).Click(); 
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
