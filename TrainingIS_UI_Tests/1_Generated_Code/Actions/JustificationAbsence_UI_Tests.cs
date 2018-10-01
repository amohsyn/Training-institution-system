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
namespace TrainingIS_UI_Tests.JustificationAbsences
{
    public class Base_JustificationAbsence_UI_Tests : Base_UI_Tests
    {
       

        public Base_JustificationAbsence_UI_Tests()
        {
            this.Entity_Path = "/JustificationAbsences";
        }
       
        [TestMethod]
        public virtual void JustificationAbsence_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void JustificationAbsence_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert JustificationAbsence
            JustificationAbsence JustificationAbsence = new JustificationAbsencesControllerTests_Service().CreateValideJustificationAbsenceInstance(null,GAppContext);
            Default_Form_JustificationAbsence_Model Default_Form_JustificationAbsence_Model = new Default_Form_JustificationAbsence_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_JustificationAbsence_Model(JustificationAbsence);



			this.Select.SelectValue("TraineeId", Default_Form_JustificationAbsence_Model.TraineeId.ToString());

			this.Select.SelectValue("Category_JustificationAbsenceId", Default_Form_JustificationAbsence_Model.Category_JustificationAbsenceId.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_JustificationAbsence_Model.StartDate), Default_Form_JustificationAbsence_Model.StartDate.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_JustificationAbsence_Model.StartTime), Default_Form_JustificationAbsence_Model.StartTime.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_JustificationAbsence_Model.EndtDate), Default_Form_JustificationAbsence_Model.EndtDate.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_JustificationAbsence_Model.EndTime), Default_Form_JustificationAbsence_Model.EndTime.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_JustificationAbsence_Model.Description)));
            Description.SendKeys(Default_Form_JustificationAbsence_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class JustificationAbsence_UI_Tests : Base_JustificationAbsence_UI_Tests
    {

    }
}
