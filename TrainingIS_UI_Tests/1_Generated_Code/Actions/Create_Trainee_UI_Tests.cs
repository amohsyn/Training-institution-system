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
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.Trainees
{
    public class Base_Create_Trainee_UI_Tests : Create_Entity_UI_Test<Trainee>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/Trainees";
            this.Entity_Reference = "Trainee_CRUD_Test";
        }

		public Base_Create_Trainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void Trainee_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Trainee_Create_Test()
        {
            Trainee_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void Trainee_Create(Trainee Trainee)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Trainee
            Default_Form_Trainee_Model Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Trainee_Model(Trainee);



	 


 
			var CNE = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.CNE)));
            CNE.SendKeys(Default_Form_Trainee_Model.CNE.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Trainee_Model.DateRegistration), Default_Form_Trainee_Model.DateRegistration.ToString());

			this.Select.SelectValue("isActif", Convert.ToInt32(Default_Form_Trainee_Model.isActif).ToString());

			this.Select.SelectValue("SchoollevelId", Default_Form_Trainee_Model.SchoollevelId.ToString());

			this.Select.SelectValue("SpecialtyId", Default_Form_Trainee_Model.SpecialtyId.ToString());

			this.Select.SelectValue("YearStudyId", Default_Form_Trainee_Model.YearStudyId.ToString());

			this.Select.SelectValue("GroupId", Default_Form_Trainee_Model.GroupId.ToString());

	 


 
			var FirstName = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.FirstName)));
            FirstName.SendKeys(Default_Form_Trainee_Model.FirstName.ToString());

	 


 
			var LastName = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.LastName)));
            LastName.SendKeys(Default_Form_Trainee_Model.LastName.ToString());

	 


 
			var FirstNameArabe = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.FirstNameArabe)));
            FirstNameArabe.SendKeys(Default_Form_Trainee_Model.FirstNameArabe.ToString());

	 


 
			var LastNameArabe = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.LastNameArabe)));
            LastNameArabe.SendKeys(Default_Form_Trainee_Model.LastNameArabe.ToString());

			this.Select.SelectValue("Sex", Convert.ToInt32(Default_Form_Trainee_Model.Sex).ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Trainee_Model.Birthdate), Default_Form_Trainee_Model.Birthdate.ToString());

			this.Select.SelectValue("NationalityId", Default_Form_Trainee_Model.NationalityId.ToString());

	 


 
			var BirthPlace = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.BirthPlace)));
            BirthPlace.SendKeys(Default_Form_Trainee_Model.BirthPlace.ToString());

	 


 
			var CIN = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.CIN)));
            CIN.SendKeys(Default_Form_Trainee_Model.CIN.ToString());

	 


 
			var Photo = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.Photo)));
            Photo.SendKeys(Default_Form_Trainee_Model.Photo.ToString());

	 


 
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

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new TraineeTestDataFactory(null, this.GAppContext).CreateValideTraineeInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete Trainee_CRUD_Test if Exist
            TraineeBLO TraineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            Trainee existante_entity = TraineeBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                TraineeBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_Trainee_UI_Tests : Base_Create_Trainee_UI_Tests
    {
		public Create_Trainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Trainee_UI_Tests() : base(null){}
    }
}