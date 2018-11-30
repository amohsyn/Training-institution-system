using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.DAL;
using GApp.Core.Context;
using GApp.UnitTest.UI_Tests;
using GApp.UnitTest.Context;
using TestData;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL;
using System.Linq;
using TrainingIS_UI_Tests.Base;
using TrainingIS.Models.FormerModelsViews;

namespace TrainingIS_UI_Tests.Formers
{
    public partial class Create_Former_UI_Tests
    {
        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.Valide_Entity_Instance.Email = "CRUD_Former_Tests@gapp.com";
        }
        public override void Former_UI_Create(Former Former)
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Create_Former_Model Create_Former_Model = new Create_Former_ModelBLM(new UnitOfWork<TrainingISModel>(), GAppContext)
                .ConverTo_Create_Former_Model(Former);

            var RegistrationNumber = b.FindElement(By.Id(nameof(Create_Former_Model.RegistrationNumber)));
            RegistrationNumber.SendKeys(Create_Former_Model.RegistrationNumber.ToString());
            var FirstName = b.FindElement(By.Id(nameof(Create_Former_Model.FirstName)));
            FirstName.SendKeys(Create_Former_Model.FirstName.ToString());
            var LastName = b.FindElement(By.Id(nameof(Create_Former_Model.LastName)));
            LastName.SendKeys(Create_Former_Model.LastName.ToString());
            
            this.Select.SelectValue("FormerSpecialtyId", Create_Former_Model.FormerSpecialtyId.ToString());
            var WeeklyHourlyMass = b.FindElement(By.Id(nameof(Create_Former_Model.WeeklyHourlyMass)));
            WeeklyHourlyMass.SendKeys(Create_Former_Model.WeeklyHourlyMass.ToString());
            var FirstNameArabe = b.FindElement(By.Id(nameof(Create_Former_Model.FirstNameArabe)));
            FirstNameArabe.SendKeys(Create_Former_Model.FirstNameArabe.ToString());
            var LastNameArabe = b.FindElement(By.Id(nameof(Create_Former_Model.LastNameArabe)));
            LastNameArabe.SendKeys(Create_Former_Model.LastNameArabe.ToString());
            this.Select.SelectValue("NationalityId", Create_Former_Model.NationalityId.ToString());
            this.Select.SelectValue("Sex", Convert.ToInt32(Create_Former_Model.Sex).ToString());

            this.DateTimePicker.SelectDate(nameof(Create_Former_Model.Birthdate), Create_Former_Model.Birthdate.ToString());
            var BirthPlace = b.FindElement(By.Id(nameof(Create_Former_Model.BirthPlace)));
            BirthPlace.SendKeys(Create_Former_Model.BirthPlace.ToString());
            var CIN = b.FindElement(By.Id(nameof(Create_Former_Model.CIN)));
            CIN.SendKeys(Create_Former_Model.CIN.ToString());
            var Cellphone = b.FindElement(By.Id(nameof(Create_Former_Model.Cellphone)));
            Cellphone.SendKeys(Create_Former_Model.Cellphone.ToString());
            var Email = b.FindElement(By.Id(nameof(Create_Former_Model.Email)));
            Email.SendKeys(Create_Former_Model.Email.ToString());
            var Address = b.FindElement(By.Id(nameof(Create_Former_Model.Address)));
            Address.SendKeys(Create_Former_Model.Address.ToString());
            var CreateUserAccount = b.FindElement(By.Id(nameof(Create_Former_Model.CreateUserAccount)));
            if (Create_Former_Model.CreateUserAccount)
                CreateUserAccount.Click();
            var Login = b.FindElement(By.Id(nameof(Create_Former_Model.Login)));
            Login.SendKeys(Create_Former_Model.Login.ToString());
            var Password = b.FindElement(By.Id(nameof(Create_Former_Model.Password)));
            Password.SendKeys(Create_Former_Model.Password.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }
}
