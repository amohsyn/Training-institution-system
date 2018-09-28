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
namespace TrainingIS_UI_Tests.LogWorks
{
    public class Base_LogWork_UI_Tests : Base_UI_Tests
    {
       

        public Base_LogWork_UI_Tests()
        {
            this.Entity_Path = "/LogWorks";
        }
       
        [TestMethod]
        public virtual void LogWork_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void LogWork_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert LogWork
            LogWork LogWork = new LogWorksControllerTests_Service().CreateValideLogWorkInstance(null,GAppContext);
            Default_Form_LogWork_Model Default_Form_LogWork_Model = new Default_Form_LogWork_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_LogWork_Model(LogWork);



	 


 
			var UserId = b.FindElement(By.Id(nameof(Default_Form_LogWork_Model.UserId)));
            UserId.SendKeys(Default_Form_LogWork_Model.UserId.ToString());

			this.Select.SelectValue("OperationWorkType", Convert.ToInt32(Default_Form_LogWork_Model.OperationWorkType).ToString());

	 


 
			var OperationReference = b.FindElement(By.Id(nameof(Default_Form_LogWork_Model.OperationReference)));
            OperationReference.SendKeys(Default_Form_LogWork_Model.OperationReference.ToString());

	 


 
			var EntityType = b.FindElement(By.Id(nameof(Default_Form_LogWork_Model.EntityType)));
            EntityType.SendKeys(Default_Form_LogWork_Model.EntityType.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_LogWork_Model.Description)));
            Description.SendKeys(Default_Form_LogWork_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class LogWork_UI_Tests : Base_LogWork_UI_Tests
    {

    }
}
