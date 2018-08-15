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

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Absence Absence = new AbsencesControllerTests_Service().CreateValideAbsenceInstance();
            Default_Form_Absence_Model2 Default_Form_Absence_Model2 = new Default_Form_Absence_Model2BLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_Absence_Model2(Absence);



			string xpath_TraineeId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TraineeId", Default_Form_Absence_Model2.TraineeId.ToString());
            b.FindElement(By.XPath(xpath_TraineeId)).Click(); 

			var isHaveAuthorization = b.FindElement(By.Id(nameof(Default_Form_Absence_Model2.isHaveAuthorization)));
			if (Default_Form_Absence_Model2.isHaveAuthorization)
                isHaveAuthorization.Click();

			string xpath_SeanceTrainingId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SeanceTrainingId", Default_Form_Absence_Model2.SeanceTrainingId.ToString());
            b.FindElement(By.XPath(xpath_SeanceTrainingId)).Click(); 

 
			var FormerComment = b.FindElement(By.Id(nameof(Default_Form_Absence_Model2.FormerComment)));
            FormerComment.SendKeys(Default_Form_Absence_Model2.FormerComment.ToString());

 
			var TraineeComment = b.FindElement(By.Id(nameof(Default_Form_Absence_Model2.TraineeComment)));
            TraineeComment.SendKeys(Default_Form_Absence_Model2.TraineeComment.ToString());

 
			var SupervisorComment = b.FindElement(By.Id(nameof(Default_Form_Absence_Model2.SupervisorComment)));
            SupervisorComment.SendKeys(Default_Form_Absence_Model2.SupervisorComment.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
