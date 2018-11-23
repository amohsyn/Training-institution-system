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

namespace TrainingIS_UI_Tests.Administrators
{
    [TestCategory("Create_UI_Test")]
    [TestCategory("Administrator")]
    public class Base_Create_Administrator_UI_Tests : Base_Create_Entity_UI_Test<Administrator>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public AdministratorTestDataFactory Administrator_TestData { set; get; }
        public AdministratorBLO AdministratorBLO  { set; get; }
        public string Reference_Created_Object = null;

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);

			//
            // GApp Context
            //
            this.UnitOfWork = new UnitOfWork<TrainingISModel>();
            this.GAppContext = new GAppContext(RoleBLO.Root_ROLE);
            TrainingYear CurrentTrainingYear = new TrainingYearBLO(this.UnitOfWork, this.GAppContext).getCurrentTrainingYear();
            this.GAppContext.Session.Add(UnitOfWorkBLO.UnitOfWork_Key, this.UnitOfWork);
            this.GAppContext.Session.Add(TrainingYearBLO.Current_TrainingYear_Key, CurrentTrainingYear);

			// Controller Name
            this.UI_Test_Context.ControllerName = "/Administrators";
            this.Entity_Reference = "Administrator_CRUD_Test";

			// TestData and BLO
			Administrator_TestData = new AdministratorTestDataFactory(this.UnitOfWork, this.GAppContext);
            AdministratorBLO = new AdministratorBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = Administrator_TestData.CreateValideAdministratorInstance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_Administrator_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
		/// <summary>
        /// InitData well be executed one time for all TestMethod
        /// </summary>
        [TestInitialize]
        public virtual void InitData()
        {
            if (!InitData_Initlizalize)
            {
                this.CleanData();
                InitData_Initlizalize = true;
            }
           
        }

        /// <summary>
        /// CleanData well be executed after each TestMethod
        /// </summary>
        [TestCleanup]
        public virtual void CleanData()
        {
            // Clean Create Data Test
           Administrator Create_Data_Test = AdministratorBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                AdministratorBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Administrator_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Administrator_Create_Test()
        {
            Administrator_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void Administrator_UI_Create(Administrator Administrator)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Administrator
            Default_Administrator_Create_Model Default_Administrator_Create_Model = new Default_Administrator_Create_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Administrator_Create_Model(Administrator);

			var Photo = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Photo)));
            Photo.SendKeys(Default_Administrator_Create_Model.Photo.ToString());
			var RegistrationNumber = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.RegistrationNumber)));
            RegistrationNumber.SendKeys(Default_Administrator_Create_Model.RegistrationNumber.ToString());
			var CreateUserAccount = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.CreateUserAccount)));
			if (Default_Administrator_Create_Model.CreateUserAccount)
                CreateUserAccount.Click();
			var Login = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Login)));
            Login.SendKeys(Default_Administrator_Create_Model.Login.ToString());
			var Password = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Password)));
            Password.SendKeys(Default_Administrator_Create_Model.Password.ToString());
			var FirstName = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.FirstName)));
            FirstName.SendKeys(Default_Administrator_Create_Model.FirstName.ToString());
			var LastName = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.LastName)));
            LastName.SendKeys(Default_Administrator_Create_Model.LastName.ToString());
			var FirstNameArabe = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.FirstNameArabe)));
            FirstNameArabe.SendKeys(Default_Administrator_Create_Model.FirstNameArabe.ToString());
			var LastNameArabe = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.LastNameArabe)));
            LastNameArabe.SendKeys(Default_Administrator_Create_Model.LastNameArabe.ToString());
			this.Select.SelectValue("Sex", Convert.ToInt32(Default_Administrator_Create_Model.Sex).ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_Administrator_Create_Model.Birthdate), Default_Administrator_Create_Model.Birthdate.ToString());
			this.Select.SelectValue("NationalityId", Default_Administrator_Create_Model.NationalityId.ToString());
			var BirthPlace = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.BirthPlace)));
            BirthPlace.SendKeys(Default_Administrator_Create_Model.BirthPlace.ToString());
			var CIN = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.CIN)));
            CIN.SendKeys(Default_Administrator_Create_Model.CIN.ToString());
			var Cellphone = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Cellphone)));
            Cellphone.SendKeys(Default_Administrator_Create_Model.Cellphone.ToString());
			var Email = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Email)));
            Email.SendKeys(Default_Administrator_Create_Model.Email.ToString());
			var Address = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Address)));
            Address.SendKeys(Default_Administrator_Create_Model.Address.ToString());
			var FaceBook = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.FaceBook)));
            FaceBook.SendKeys(Default_Administrator_Create_Model.FaceBook.ToString());
			var WebSite = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.WebSite)));
            WebSite.SendKeys(Default_Administrator_Create_Model.WebSite.ToString());
			var Reference = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Reference)));
            Reference.SendKeys(Default_Administrator_Create_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	
	public partial class Create_Administrator_UI_Tests : Base_Create_Administrator_UI_Tests
    {
		public Create_Administrator_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Administrator_UI_Tests() : base(null){}
    }
}
