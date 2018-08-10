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
    public class Trainee_UI_Index_Tests : Base_UI_Tests
    {
       

        public Trainee_UI_Index_Tests()
        {
            this.Entity_Path = "/Trainees";
        }
       
        [TestMethod]
        public void Trainee_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void Trainee_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Trainee Trainee = new TraineesControllerTests_Service().CreateValideTraineeInstance();
            Default_TraineeFormView Default_TraineeFormView = new Default_TraineeFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_TraineeFormView(Trainee);



 
			var CNE = b.FindElement(By.Id(nameof(Default_TraineeFormView.CNE)));
            CNE.SendKeys(Default_TraineeFormView.CNE.ToString());

 
			var DateRegistration = b.FindElement(By.Id(nameof(Default_TraineeFormView.DateRegistration)));
            DateRegistration.SendKeys(Default_TraineeFormView.DateRegistration.ToString());

  			string xpath_isActif = string.Format("//select[@id='{0}']/option[@value='{1}']", "isActif", Default_TraineeFormView.isActif.ToString());
            b.FindElement(By.XPath(xpath_isActif)).Click();

			string xpath_SchoollevelId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SchoollevelId", Default_TraineeFormView.SchoollevelId.ToString());
            b.FindElement(By.XPath(xpath_SchoollevelId)).Click(); 

			string xpath_GroupId = string.Format("//select[@id='{0}']/option[@value='{1}']", "GroupId", Default_TraineeFormView.GroupId.ToString());
            b.FindElement(By.XPath(xpath_GroupId)).Click(); 

 
			var FirstName = b.FindElement(By.Id(nameof(Default_TraineeFormView.FirstName)));
            FirstName.SendKeys(Default_TraineeFormView.FirstName.ToString());

 
			var LastName = b.FindElement(By.Id(nameof(Default_TraineeFormView.LastName)));
            LastName.SendKeys(Default_TraineeFormView.LastName.ToString());

 
			var FirstNameArabe = b.FindElement(By.Id(nameof(Default_TraineeFormView.FirstNameArabe)));
            FirstNameArabe.SendKeys(Default_TraineeFormView.FirstNameArabe.ToString());

 
			var LastNameArabe = b.FindElement(By.Id(nameof(Default_TraineeFormView.LastNameArabe)));
            LastNameArabe.SendKeys(Default_TraineeFormView.LastNameArabe.ToString());

  			string xpath_Sex = string.Format("//select[@id='{0}']/option[@value='{1}']", "Sex", Default_TraineeFormView.Sex.ToString());
            b.FindElement(By.XPath(xpath_Sex)).Click();

 
			var Birthdate = b.FindElement(By.Id(nameof(Default_TraineeFormView.Birthdate)));
            Birthdate.SendKeys(Default_TraineeFormView.Birthdate.ToString());

			string xpath_NationalityId = string.Format("//select[@id='{0}']/option[@value='{1}']", "NationalityId", Default_TraineeFormView.NationalityId.ToString());
            b.FindElement(By.XPath(xpath_NationalityId)).Click(); 

 
			var BirthPlace = b.FindElement(By.Id(nameof(Default_TraineeFormView.BirthPlace)));
            BirthPlace.SendKeys(Default_TraineeFormView.BirthPlace.ToString());

 
			var CIN = b.FindElement(By.Id(nameof(Default_TraineeFormView.CIN)));
            CIN.SendKeys(Default_TraineeFormView.CIN.ToString());

 
			var Cellphone = b.FindElement(By.Id(nameof(Default_TraineeFormView.Cellphone)));
            Cellphone.SendKeys(Default_TraineeFormView.Cellphone.ToString());

 
			var Email = b.FindElement(By.Id(nameof(Default_TraineeFormView.Email)));
            Email.SendKeys(Default_TraineeFormView.Email.ToString());

 
			var Address = b.FindElement(By.Id(nameof(Default_TraineeFormView.Address)));
            Address.SendKeys(Default_TraineeFormView.Address.ToString());

 
			var FaceBook = b.FindElement(By.Id(nameof(Default_TraineeFormView.FaceBook)));
            FaceBook.SendKeys(Default_TraineeFormView.FaceBook.ToString());

 
			var WebSite = b.FindElement(By.Id(nameof(Default_TraineeFormView.WebSite)));
            WebSite.SendKeys(Default_TraineeFormView.WebSite.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
