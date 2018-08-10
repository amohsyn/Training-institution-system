using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.BLL.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Former_UI_Index_Tests : Base_UI_Tests
    {
       

        public Former_UI_Index_Tests()
        {
            this.Entity_Path = "/Formers";
        }
       
        [TestMethod]
        public void Former_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void Former_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Former Former = new FormersControllerTests_Service().CreateValideFormerInstance();
            FormerFormView FormerFormView = new FormerFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_FormerFormView(Former);



 
			var RegistrationNumber = b.FindElement(By.Id(nameof(FormerFormView.RegistrationNumber)));
            RegistrationNumber.SendKeys(FormerFormView.RegistrationNumber.ToString());

 
			var FirstName = b.FindElement(By.Id(nameof(FormerFormView.FirstName)));
            FirstName.SendKeys(FormerFormView.FirstName.ToString());

 
			var LastName = b.FindElement(By.Id(nameof(FormerFormView.LastName)));
            LastName.SendKeys(FormerFormView.LastName.ToString());

 
			var FirstNameArabe = b.FindElement(By.Id(nameof(FormerFormView.FirstNameArabe)));
            FirstNameArabe.SendKeys(FormerFormView.FirstNameArabe.ToString());

 
			var LastNameArabe = b.FindElement(By.Id(nameof(FormerFormView.LastNameArabe)));
            LastNameArabe.SendKeys(FormerFormView.LastNameArabe.ToString());

			string xpath_NationalityId = string.Format("//select[@id='{0}']/option[@value='{1}']", "NationalityId", FormerFormView.NationalityId.ToString());
            b.FindElement(By.XPath(xpath_NationalityId)).Click(); 

  			string xpath_Sex = string.Format("//select[@id='{0}']/option[@value='{1}']", "Sex", FormerFormView.Sex.ToString());
            b.FindElement(By.XPath(xpath_Sex)).Click();

 
			var Birthdate = b.FindElement(By.Id(nameof(FormerFormView.Birthdate)));
            Birthdate.SendKeys(FormerFormView.Birthdate.ToString());

 
			var BirthPlace = b.FindElement(By.Id(nameof(FormerFormView.BirthPlace)));
            BirthPlace.SendKeys(FormerFormView.BirthPlace.ToString());

 
			var CIN = b.FindElement(By.Id(nameof(FormerFormView.CIN)));
            CIN.SendKeys(FormerFormView.CIN.ToString());

 
			var Cellphone = b.FindElement(By.Id(nameof(FormerFormView.Cellphone)));
            Cellphone.SendKeys(FormerFormView.Cellphone.ToString());

 
			var Email = b.FindElement(By.Id(nameof(FormerFormView.Email)));
            Email.SendKeys(FormerFormView.Email.ToString());

 
			var Address = b.FindElement(By.Id(nameof(FormerFormView.Address)));
            Address.SendKeys(FormerFormView.Address.ToString());

			var CreateUserAccount = b.FindElement(By.Id(nameof(FormerFormView.CreateUserAccount)));
			if (FormerFormView.CreateUserAccount)
                CreateUserAccount.Click();

 
			var Login = b.FindElement(By.Id(nameof(FormerFormView.Login)));
            Login.SendKeys(FormerFormView.Login.ToString());

 
			var Password = b.FindElement(By.Id(nameof(FormerFormView.Password)));
            Password.SendKeys(FormerFormView.Password.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
