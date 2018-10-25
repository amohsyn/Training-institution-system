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
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.Administrators
{
    public class Base_Create_Administrator_UI_Tests : Create_Entity_UI_Test<Administrator>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/Administrators";
            this.Entity_Reference = "Administrator_CRUD_Test";
        }

		public Base_Create_Administrator_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void Administrator_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Administrator_Create_Test()
        {
            Administrator_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void Administrator_Create(Administrator Administrator)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Administrator
            Default_Form_Administrator_Model Default_Form_Administrator_Model = new Default_Form_Administrator_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Administrator_Model(Administrator);



	 


 
			var RegistrationNumber = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.RegistrationNumber)));
            RegistrationNumber.SendKeys(Default_Form_Administrator_Model.RegistrationNumber.ToString());

			var CreateUserAccount = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.CreateUserAccount)));
			if (Default_Form_Administrator_Model.CreateUserAccount)
                CreateUserAccount.Click();

	 


 
			var Login = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.Login)));
            Login.SendKeys(Default_Form_Administrator_Model.Login.ToString());

	 


 
			var Password = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.Password)));
            Password.SendKeys(Default_Form_Administrator_Model.Password.ToString());

	 


 
			var FirstName = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.FirstName)));
            FirstName.SendKeys(Default_Form_Administrator_Model.FirstName.ToString());

	 


 
			var LastName = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.LastName)));
            LastName.SendKeys(Default_Form_Administrator_Model.LastName.ToString());

	 


 
			var FirstNameArabe = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.FirstNameArabe)));
            FirstNameArabe.SendKeys(Default_Form_Administrator_Model.FirstNameArabe.ToString());

	 


 
			var LastNameArabe = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.LastNameArabe)));
            LastNameArabe.SendKeys(Default_Form_Administrator_Model.LastNameArabe.ToString());

			this.Select.SelectValue("Sex", Convert.ToInt32(Default_Form_Administrator_Model.Sex).ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Administrator_Model.Birthdate), Default_Form_Administrator_Model.Birthdate.ToString());

			this.Select.SelectValue("NationalityId", Default_Form_Administrator_Model.NationalityId.ToString());

	 


 
			var BirthPlace = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.BirthPlace)));
            BirthPlace.SendKeys(Default_Form_Administrator_Model.BirthPlace.ToString());

	 


 
			var CIN = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.CIN)));
            CIN.SendKeys(Default_Form_Administrator_Model.CIN.ToString());

	 


 
			var Photo = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.Photo)));
            Photo.SendKeys(Default_Form_Administrator_Model.Photo.ToString());

	 


 
			var Cellphone = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.Cellphone)));
            Cellphone.SendKeys(Default_Form_Administrator_Model.Cellphone.ToString());

	 


 
			var Email = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.Email)));
            Email.SendKeys(Default_Form_Administrator_Model.Email.ToString());

	 


 
			var Address = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.Address)));
            Address.SendKeys(Default_Form_Administrator_Model.Address.ToString());

	 


 
			var FaceBook = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.FaceBook)));
            FaceBook.SendKeys(Default_Form_Administrator_Model.FaceBook.ToString());

	 


 
			var WebSite = b.FindElement(By.Id(nameof(Default_Form_Administrator_Model.WebSite)));
            WebSite.SendKeys(Default_Form_Administrator_Model.WebSite.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new AdministratorTestDataFactory(null, this.GAppContext).CreateValideAdministratorInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete Administrator_CRUD_Test if Exist
            AdministratorBLO AdministratorBLO = new AdministratorBLO(this.UnitOfWork, this.GAppContext);
            Administrator existante_entity = AdministratorBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                AdministratorBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_Administrator_UI_Tests : Base_Create_Administrator_UI_Tests
    {
		public Create_Administrator_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Administrator_UI_Tests() : base(null){}
    }
}
