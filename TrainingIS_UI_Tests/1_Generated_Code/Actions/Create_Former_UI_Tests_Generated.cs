﻿using System;
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
using TrainingIS.Entities.ModelsViews.FormerModelsViews;

namespace TrainingIS_UI_Tests.Formers
{
    public class Base_Create_Former_UI_Tests : Create_Entity_UI_Test<Former>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/Formers";
            this.Entity_Reference = "Former_CRUD_Test";
        }

		public Base_Create_Former_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
		{
            //
            // GApp Context
            //
            this.UnitOfWork = new UnitOfWork<TrainingISModel>();
            this.GAppContext = new GAppContext(RoleBLO.Root_ROLE);
            TrainingYear CurrentTrainingYear = new TrainingYearBLO(this.UnitOfWork, this.GAppContext).getCurrentTrainingYear();
            this.GAppContext.Session.Add(UnitOfWorkBLO.UnitOfWork_Key, this.UnitOfWork);
            this.GAppContext.Session.Add(TrainingYearBLO.Current_TrainingYear_Key, CurrentTrainingYear);

        }
 
        
        [TestMethod]
        public virtual void Former_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Former_Create_Test()
        {
            Former_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void Former_Create(Former Former)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            FormerFormView FormerFormView = new FormerFormViewBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_FormerFormView(Former);



	 


 
			var RegistrationNumber = b.FindElement(By.Id(nameof(FormerFormView.RegistrationNumber)));
            RegistrationNumber.SendKeys(FormerFormView.RegistrationNumber.ToString());

	 


 
			var FirstName = b.FindElement(By.Id(nameof(FormerFormView.FirstName)));
            FirstName.SendKeys(FormerFormView.FirstName.ToString());

	 


 
			var LastName = b.FindElement(By.Id(nameof(FormerFormView.LastName)));
            LastName.SendKeys(FormerFormView.LastName.ToString());

	 


 
			var Photo = b.FindElement(By.Id(nameof(FormerFormView.Photo)));
            Photo.SendKeys(FormerFormView.Photo.ToString());

			this.Select.SelectValue("FormerSpecialtyId", FormerFormView.FormerSpecialtyId.ToString());

	 


 
			var WeeklyHourlyMass = b.FindElement(By.Id(nameof(FormerFormView.WeeklyHourlyMass)));
            WeeklyHourlyMass.SendKeys(FormerFormView.WeeklyHourlyMass.ToString());

	 


 
			var FirstNameArabe = b.FindElement(By.Id(nameof(FormerFormView.FirstNameArabe)));
            FirstNameArabe.SendKeys(FormerFormView.FirstNameArabe.ToString());

	 


 
			var LastNameArabe = b.FindElement(By.Id(nameof(FormerFormView.LastNameArabe)));
            LastNameArabe.SendKeys(FormerFormView.LastNameArabe.ToString());

			this.Select.SelectValue("NationalityId", FormerFormView.NationalityId.ToString());

			this.Select.SelectValue("Sex", Convert.ToInt32(FormerFormView.Sex).ToString());

			
			this.DateTimePicker.SelectDate(nameof(FormerFormView.Birthdate), FormerFormView.Birthdate.ToString());

	 


 
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

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new FormerTestDataFactory(null, this.GAppContext).CreateValideFormerInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete Former_CRUD_Test if Exist
            FormerBLO FormerBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            Former existante_entity = FormerBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                FormerBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_Former_UI_Tests : Base_Create_Former_UI_Tests
    {
		public Create_Former_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Former_UI_Tests() : base(null){}
    }
}