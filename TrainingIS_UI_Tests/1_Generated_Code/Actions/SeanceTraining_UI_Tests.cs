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
using TrainingIS.Models.SeanceTrainings;
namespace TrainingIS_UI_Tests.SeanceTrainings
{
    public class Base_SeanceTraining_UI_Tests : Base_UI_Tests
    {
       

        public Base_SeanceTraining_UI_Tests()
        {
            this.Entity_Path = "/SeanceTrainings";
        }
       
        [TestMethod]
        public virtual void SeanceTraining_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void SeanceTraining_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert SeanceTraining
            SeanceTraining SeanceTraining = new SeanceTrainingsControllerTests_Service().CreateValideSeanceTrainingInstance(null,GAppContext);
            Create_SeanceTraining_Model Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Create_SeanceTraining_Model(SeanceTraining);



			
			this.DateTimePicker.SelectDate(nameof(Create_SeanceTraining_Model.SeanceDate), Create_SeanceTraining_Model.SeanceDate.ToString());

	 


 
			var ScheduleCode = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.ScheduleCode)));
            ScheduleCode.SendKeys(Create_SeanceTraining_Model.ScheduleCode.ToString());

	 


 
			var SeanceNumberId = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.SeanceNumberId)));
            SeanceNumberId.SendKeys(Create_SeanceTraining_Model.SeanceNumberId.ToString());

	 


 
			var ClassroomId = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.ClassroomId)));
            ClassroomId.SendKeys(Create_SeanceTraining_Model.ClassroomId.ToString());

	 


 
			var GroupId = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.GroupId)));
            GroupId.SendKeys(Create_SeanceTraining_Model.GroupId.ToString());

	 


 
			var ModuleTrainingId = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.ModuleTrainingId)));
            ModuleTrainingId.SendKeys(Create_SeanceTraining_Model.ModuleTrainingId.ToString());

			this.Select.SelectValue("SeancePlanningId", Create_SeanceTraining_Model.SeancePlanningId.ToString());

	 


 
			var Contained = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.Contained)));
            Contained.SendKeys(Create_SeanceTraining_Model.Contained.ToString());

	 


 
			var SeancePlannings = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.SeancePlannings)));
            SeancePlannings.SendKeys(Create_SeanceTraining_Model.SeancePlannings.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class SeanceTraining_UI_Tests : Base_SeanceTraining_UI_Tests
    {

    }
}
