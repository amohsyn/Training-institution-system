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

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            SeancePlanning SeancePlanning = new SeancePlanningsControllerTests_Service().CreateValideSeancePlanningInstance(null,GAppContext);
            Default_Form_SeancePlanning_Model Default_Form_SeancePlanning_Model = new Default_Form_SeancePlanning_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_SeancePlanning_Model(SeancePlanning);



			string xpath_ScheduleId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ScheduleId", Default_Form_SeancePlanning_Model.ScheduleId.ToString());
            b.FindElement(By.XPath(xpath_ScheduleId)).Click(); 

			string xpath_TrainingId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TrainingId", Default_Form_SeancePlanning_Model.TrainingId.ToString());
            b.FindElement(By.XPath(xpath_TrainingId)).Click(); 

			string xpath_SeanceDayId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SeanceDayId", Default_Form_SeancePlanning_Model.SeanceDayId.ToString());
            b.FindElement(By.XPath(xpath_SeanceDayId)).Click(); 

			string xpath_SeanceNumberId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SeanceNumberId", Default_Form_SeancePlanning_Model.SeanceNumberId.ToString());
            b.FindElement(By.XPath(xpath_SeanceNumberId)).Click(); 

			string xpath_ClassroomId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ClassroomId", Default_Form_SeancePlanning_Model.ClassroomId.ToString());
            b.FindElement(By.XPath(xpath_ClassroomId)).Click(); 

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_SeancePlanning_Model.Description)));
            Description.SendKeys(Default_Form_SeancePlanning_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
