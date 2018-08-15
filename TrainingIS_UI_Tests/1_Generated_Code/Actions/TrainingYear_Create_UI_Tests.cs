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
    public class TrainingYear_Create_UI_Tests : Base_UI_Tests
    {
       

        public TrainingYear_Create_UI_Tests()
        {
            this.Entity_Path = "/TrainingYears";
        }
       
        [TestMethod]
        public void TrainingYear_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void TrainingYear_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            TrainingYear TrainingYear = new TrainingYearsControllerTests_Service().CreateValideTrainingYearInstance();
            Default_Form_TrainingYear_Model Default_Form_TrainingYear_Model = new Default_Form_TrainingYear_ModelBLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_TrainingYear_Model(TrainingYear);



 
			var Code = b.FindElement(By.Id(nameof(Default_Form_TrainingYear_Model.Code)));
            Code.SendKeys(Default_Form_TrainingYear_Model.Code.ToString());

 
			var StartDate = b.FindElement(By.Id(nameof(Default_Form_TrainingYear_Model.StartDate)));
            StartDate.SendKeys(Default_Form_TrainingYear_Model.StartDate.ToString());

 
			var EndtDate = b.FindElement(By.Id(nameof(Default_Form_TrainingYear_Model.EndtDate)));
            EndtDate.SendKeys(Default_Form_TrainingYear_Model.EndtDate.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
