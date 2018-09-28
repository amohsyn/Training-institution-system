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
namespace TrainingIS_UI_Tests.SeancePlannings
{
    public class Base_SeancePlanning_UI_Tests : Base_UI_Tests
    {
       

        public Base_SeancePlanning_UI_Tests()
        {
            this.Entity_Path = "/SeancePlannings";
        }
       
        [TestMethod]
        public virtual void SeancePlanning_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void SeancePlanning_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert SeancePlanning
            SeancePlanning SeancePlanning = new SeancePlanningsControllerTests_Service().CreateValideSeancePlanningInstance(null,GAppContext);
            Default_Form_SeancePlanning_Model Default_Form_SeancePlanning_Model = new Default_Form_SeancePlanning_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_SeancePlanning_Model(SeancePlanning);



			this.Select.SelectValue("ScheduleId", Default_Form_SeancePlanning_Model.ScheduleId.ToString());

			this.Select.SelectValue("TrainingId", Default_Form_SeancePlanning_Model.TrainingId.ToString());

			this.Select.SelectValue("SeanceDayId", Default_Form_SeancePlanning_Model.SeanceDayId.ToString());

			this.Select.SelectValue("SeanceNumberId", Default_Form_SeancePlanning_Model.SeanceNumberId.ToString());

			this.Select.SelectValue("ClassroomId", Default_Form_SeancePlanning_Model.ClassroomId.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_SeancePlanning_Model.Description)));
            Description.SendKeys(Default_Form_SeancePlanning_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class SeancePlanning_UI_Tests : Base_SeancePlanning_UI_Tests
    {

    }
}
