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
using TrainingIS.Models.Absences;
namespace TrainingIS_UI_Tests.Absences
{
    public class Base_Absence_UI_Tests : Base_UI_Tests
    {
       

        public Base_Absence_UI_Tests()
        {
            this.Entity_Path = "/Absences";
        }
       
        [TestMethod]
        public virtual void Absence_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void Absence_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Absence
            Absence Absence = new AbsencesControllerTests_Service().CreateValideAbsenceInstance(null,GAppContext);
            Create_Absence_Model Create_Absence_Model = new Create_Absence_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Create_Absence_Model(Absence);



			this.Select.SelectValue("TraineeId", Create_Absence_Model.TraineeId.ToString());

			var isHaveAuthorization = b.FindElement(By.Id(nameof(Create_Absence_Model.isHaveAuthorization)));
			if (Create_Absence_Model.isHaveAuthorization)
                isHaveAuthorization.Click();

			this.Select.SelectValue("SeanceTrainingId", Create_Absence_Model.SeanceTrainingId.ToString());

	 


 
			var FormerComment = b.FindElement(By.Id(nameof(Create_Absence_Model.FormerComment)));
            FormerComment.SendKeys(Create_Absence_Model.FormerComment.ToString());

	 


 
			var TraineeComment = b.FindElement(By.Id(nameof(Create_Absence_Model.TraineeComment)));
            TraineeComment.SendKeys(Create_Absence_Model.TraineeComment.ToString());

	 


 
			var SupervisorComment = b.FindElement(By.Id(nameof(Create_Absence_Model.SupervisorComment)));
            SupervisorComment.SendKeys(Create_Absence_Model.SupervisorComment.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class Absence_UI_Tests : Base_Absence_UI_Tests
    {

    }
}
