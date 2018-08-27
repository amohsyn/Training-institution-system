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
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Absence_Create_UI_Tests : Base_UI_Tests
    {
       

        public Absence_Create_UI_Tests()
        {
            this.Entity_Path = "/Absences";
        }
       
        [TestMethod]
        public void Absence_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void Absence_Create_Test()
        {
            this.GoTo_Index();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Absence Absence = new AbsencesControllerTests_Service().CreateValideAbsenceInstance(null,GAppContext);
            Create_Absence_Model Create_Absence_Model = new Create_Absence_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Create_Absence_Model(Absence);



			string xpath_TraineeId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TraineeId", Create_Absence_Model.TraineeId.ToString());
            b.FindElement(By.XPath(xpath_TraineeId)).Click(); 

			var isHaveAuthorization = b.FindElement(By.Id(nameof(Create_Absence_Model.isHaveAuthorization)));
			if (Create_Absence_Model.isHaveAuthorization)
                isHaveAuthorization.Click();

			string xpath_SeanceTrainingId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SeanceTrainingId", Create_Absence_Model.SeanceTrainingId.ToString());
            b.FindElement(By.XPath(xpath_SeanceTrainingId)).Click(); 

			string xpath_SeancePlanningId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SeancePlanningId", Create_Absence_Model.SeancePlanningId.ToString());
            b.FindElement(By.XPath(xpath_SeancePlanningId)).Click(); 

 
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
}
