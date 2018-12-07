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
    [TestCategory("Create_UI_Test")]
    [TestCategory("Trainee")]
    public class Base_Create_Trainee_UI_Tests : Base_Create_Entity_UI_Test<Trainee>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public TraineeTestDataFactory Trainee_TestData { set; get; }
        public TraineeBLO TraineeBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/Trainees";
            this.Entity_Reference = "Trainee_CRUD_Test";

			// TestData and BLO
			Trainee_TestData = new TraineeTestDataFactory(this.UnitOfWork, this.GAppContext);
            TraineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = Trainee_TestData.Create_CRUD_Trainee_Test_Instance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_Trainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           Trainee Create_Data_Test = TraineeBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                TraineeBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Trainee_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Trainee_Create_Test()
        {
            Trainee_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void Trainee_UI_Create(Trainee Trainee)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Trainee
            Default_Trainee_Create_Model Default_Trainee_Create_Model = new Default_Trainee_Create_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Trainee_Create_Model(Trainee);

			var Photo = b.FindElement(By.Id(nameof(Default_Trainee_Create_Model.Photo)));
            Photo.SendKeys(Default_Trainee_Create_Model.Photo.ToString());
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
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	
	public partial class Create_Trainee_UI_Tests : Base_Create_Trainee_UI_Tests
    {
		public Create_Trainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Trainee_UI_Tests() : base(null){}
    }
}
