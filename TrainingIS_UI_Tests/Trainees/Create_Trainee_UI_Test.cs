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
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.Trainees
{
    public partial class Create_Trainee_UI_Tests
    {
        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.Login = "Supervisor";
            this.UI_Test_Context.Password = "Supervisor@123456";
        }

        [TestInitialize]
        public override void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Instance.Email = string.Format("madani_{0}@gmail.com", this.Entity_Reference);
        }

        public override void Trainee_UI_Create(Trainee Trainee)
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Trainee
            Default_Trainee_Create_Model Default_Trainee_Create_Model = new Default_Trainee_Create_ModelBLM(new UnitOfWork<TrainingISModel>(), GAppContext)
                .ConverTo_Default_Trainee_Create_Model(Trainee);

           
            var CNE = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.CNE)));
            CNE.SendKeys(Default_Trainee_Create_Model.CNE.ToString());

            this.DateTimePicker.SelectDate(nameof(Default_Trainee_Create_Model.DateRegistration), Default_Trainee_Create_Model.DateRegistration.ToString());
            this.Select.SelectValue("isActif", Convert.ToInt32(Default_Trainee_Create_Model.isActif).ToString());
            this.Select.SelectValue("SchoollevelId", Default_Trainee_Create_Model.SchoollevelId.ToString());
            this.Select.SelectValue("SpecialtyId", Default_Trainee_Create_Model.SpecialtyId.ToString());
            this.Select.SelectValue("YearStudyId", Default_Trainee_Create_Model.YearStudyId.ToString());
            this.Select.SelectValue("GroupId", Default_Trainee_Create_Model.GroupId.ToString());
            var FirstName = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.FirstName)));
            FirstName.SendKeys(Default_Trainee_Create_Model.FirstName.ToString());
            var LastName = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.LastName)));
            LastName.SendKeys(Default_Trainee_Create_Model.LastName.ToString());
            var FirstNameArabe = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.FirstNameArabe)));
            FirstNameArabe.SendKeys(Default_Trainee_Create_Model.FirstNameArabe.ToString());
            var LastNameArabe = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.LastNameArabe)));
            LastNameArabe.SendKeys(Default_Trainee_Create_Model.LastNameArabe.ToString());
            this.Select.SelectValue("Sex", Convert.ToInt32(Default_Trainee_Create_Model.Sex).ToString());

            this.DateTimePicker.SelectDate(nameof(Default_Trainee_Create_Model.Birthdate), Default_Trainee_Create_Model.Birthdate.ToString());
            this.Select.SelectValue("NationalityId", Default_Trainee_Create_Model.NationalityId.ToString());
            var BirthPlace = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.BirthPlace)));
            BirthPlace.SendKeys(Default_Trainee_Create_Model.BirthPlace.ToString());
            var CIN = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.CIN)));
            CIN.SendKeys(Default_Trainee_Create_Model.CIN.ToString());
            var Cellphone = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.Cellphone)));
            Cellphone.SendKeys(Default_Trainee_Create_Model.Cellphone.ToString());
            var Email = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.Email)));
            Email.SendKeys(Default_Trainee_Create_Model.Email.ToString());
            var Address = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.Address)));
            Address.SendKeys(Default_Trainee_Create_Model.Address.ToString());
            var FaceBook = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.FaceBook)));
            FaceBook.SendKeys(Default_Trainee_Create_Model.FaceBook.ToString());
            var WebSite = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.WebSite)));
            WebSite.SendKeys(Default_Trainee_Create_Model.WebSite.ToString());
            var Reference = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.Reference)));
            Reference.SendKeys(Default_Trainee_Create_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }

    }
}
