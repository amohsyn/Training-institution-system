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
namespace TrainingIS_UI_Tests.WarningTrainees
{
    public class Base_WarningTrainee_UI_Tests : Base_UI_Tests
    {
       

        public Base_WarningTrainee_UI_Tests()
        {
            this.Entity_Path = "/WarningTrainees";
        }
       
        [TestMethod]
        public virtual void WarningTrainee_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void WarningTrainee_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert WarningTrainee
            WarningTrainee WarningTrainee = new WarningTraineesControllerTests_Service().CreateValideWarningTraineeInstance(null,GAppContext);
            Default_Form_WarningTrainee_Model Default_Form_WarningTrainee_Model = new Default_Form_WarningTrainee_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_WarningTrainee_Model(WarningTrainee);



			this.Select.SelectValue("TraineeId", Default_Form_WarningTrainee_Model.TraineeId.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_WarningTrainee_Model.WarningDate), Default_Form_WarningTrainee_Model.WarningDate.ToString());

			this.Select.SelectValue("Category_WarningTraineeId", Default_Form_WarningTrainee_Model.Category_WarningTraineeId.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_WarningTrainee_Model.Description)));
            Description.SendKeys(Default_Form_WarningTrainee_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class WarningTrainee_UI_Tests : Base_WarningTrainee_UI_Tests
    {

    }
}
