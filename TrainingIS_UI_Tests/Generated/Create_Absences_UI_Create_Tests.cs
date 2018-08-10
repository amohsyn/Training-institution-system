using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Absence_UI_Index_Tests : Base_UI_Tests
    {
       

        public Absence_UI_Index_Tests()
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

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Absence Absence = new AbsencesControllerTests_Service().CreateValideAbsenceInstance();
            Default_AbsenceFormView Default_AbsenceFormView = new Default_AbsenceFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_AbsenceFormView(Absence);



			string xpath_TraineeId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TraineeId", Default_AbsenceFormView.TraineeId.ToString());
            b.FindElement(By.XPath(xpath_TraineeId)).Click(); 

			var isHaveAuthorization = b.FindElement(By.Id(nameof(Default_AbsenceFormView.isHaveAuthorization)));
			if (Default_AbsenceFormView.isHaveAuthorization)
                isHaveAuthorization.Click();

			string xpath_SeanceTrainingId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SeanceTrainingId", Default_AbsenceFormView.SeanceTrainingId.ToString());
            b.FindElement(By.XPath(xpath_SeanceTrainingId)).Click(); 

 
			var FormerComment = b.FindElement(By.Id(nameof(Default_AbsenceFormView.FormerComment)));
            FormerComment.SendKeys(Default_AbsenceFormView.FormerComment.ToString());

 
			var TraineeComment = b.FindElement(By.Id(nameof(Default_AbsenceFormView.TraineeComment)));
            TraineeComment.SendKeys(Default_AbsenceFormView.TraineeComment.ToString());

 
			var SupervisorComment = b.FindElement(By.Id(nameof(Default_AbsenceFormView.SupervisorComment)));
            SupervisorComment.SendKeys(Default_AbsenceFormView.SupervisorComment.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
