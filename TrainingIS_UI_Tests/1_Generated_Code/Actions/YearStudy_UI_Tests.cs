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
namespace TrainingIS_UI_Tests.YearStudies
{
    public class Base_YearStudy_UI_Tests : Base_UI_Tests
    {
       

        public Base_YearStudy_UI_Tests()
        {
            this.Entity_Path = "/YearStudies";
        }
       
        [TestMethod]
        public virtual void YearStudy_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void YearStudy_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            YearStudy YearStudy = new YearStudiesControllerTests_Service().CreateValideYearStudyInstance(null,GAppContext);
            Default_Form_YearStudy_Model Default_Form_YearStudy_Model = new Default_Form_YearStudy_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_YearStudy_Model(YearStudy);



 
			var Code = b.FindElement(By.Id(nameof(Default_Form_YearStudy_Model.Code)));
            Code.SendKeys(Default_Form_YearStudy_Model.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_Form_YearStudy_Model.Name)));
            Name.SendKeys(Default_Form_YearStudy_Model.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_YearStudy_Model.Description)));
            Description.SendKeys(Default_Form_YearStudy_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class YearStudy_UI_Tests : Base_YearStudy_UI_Tests
    {

    }
}
