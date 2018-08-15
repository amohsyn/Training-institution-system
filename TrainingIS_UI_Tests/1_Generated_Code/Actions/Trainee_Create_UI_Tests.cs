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
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Trainee_Create_UI_Tests : Base_UI_Tests
    {
       

        public Trainee_Create_UI_Tests()
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
            Default_Form_Trainee_Model Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_Trainee_Model(Trainee);



 
			var CNE = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.CNE)));
            CNE.SendKeys(Default_Form_Trainee_Model.CNE.ToString());

 
			var DateRegistration = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.DateRegistration)));
            DateRegistration.SendKeys(Default_Form_Trainee_Model.DateRegistration.ToString());

  			string xpath_isActif = string.Format("//select[@id='{0}']/option[@value='{1}']", "isActif", Default_Form_Trainee_Model.isActif.ToString());
            b.FindElement(By.XPath(xpath_isActif)).Click();

 
			var FirstName = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.FirstName)));
            FirstName.SendKeys(Default_Form_Trainee_Model.FirstName.ToString());

 
			var LastName = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.LastName)));
            LastName.SendKeys(Default_Form_Trainee_Model.LastName.ToString());

 
			var FirstNameArabe = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.FirstNameArabe)));
            FirstNameArabe.SendKeys(Default_Form_Trainee_Model.FirstNameArabe.ToString());

 
			var LastNameArabe = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.LastNameArabe)));
            LastNameArabe.SendKeys(Default_Form_Trainee_Model.LastNameArabe.ToString());

  			string xpath_Sex = string.Format("//select[@id='{0}']/option[@value='{1}']", "Sex", Default_Form_Trainee_Model.Sex.ToString());
            b.FindElement(By.XPath(xpath_Sex)).Click();

 
			var Birthdate = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.Birthdate)));
            Birthdate.SendKeys(Default_Form_Trainee_Model.Birthdate.ToString());

 
			var BirthPlace = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.BirthPlace)));
            BirthPlace.SendKeys(Default_Form_Trainee_Model.BirthPlace.ToString());

 
			var CIN = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.CIN)));
            CIN.SendKeys(Default_Form_Trainee_Model.CIN.ToString());

 
			var Cellphone = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.Cellphone)));
            Cellphone.SendKeys(Default_Form_Trainee_Model.Cellphone.ToString());

 
			var Email = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.Email)));
            Email.SendKeys(Default_Form_Trainee_Model.Email.ToString());

 
			var Address = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.Address)));
            Address.SendKeys(Default_Form_Trainee_Model.Address.ToString());

 
			var FaceBook = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.FaceBook)));
            FaceBook.SendKeys(Default_Form_Trainee_Model.FaceBook.ToString());

 
			var WebSite = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.WebSite)));
            WebSite.SendKeys(Default_Form_Trainee_Model.WebSite.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
