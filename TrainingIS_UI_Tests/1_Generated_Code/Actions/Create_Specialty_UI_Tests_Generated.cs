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
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.Specialties
{
    public class Base_Create_Specialty_UI_Tests : Create_Entity_UI_Test<Specialty>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public SpecialtyTestDataFactory Specialty_TestData { set; get; }
        public SpecialtyBLO SpecialtyBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/Specialties";
            this.Entity_Reference = "Specialty_CRUD_Test";

			// TestData and BLO
			Specialty_TestData = new SpecialtyTestDataFactory(this.UnitOfWork, this.GAppContext);
            SpecialtyBLO = new SpecialtyBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = Specialty_TestData.CreateValideSpecialtyInstance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_Specialty_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
		/// <summary>
        /// InitData well be executed one time for all TestMethod
        /// </summary>
        [TestInitialize]
        public virtual void InitData()
        {
            if (!InitData_Initlizalize)
            {
                Specialty_TestData.Insert_Test_Data_If_Not_Exist();
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
           Specialty Create_Data_Test = SpecialtyBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                SpecialtyBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Specialty_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Specialty_Create_Test()
        {
            Specialty_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void Specialty_UI_Create(Specialty Specialty)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Specialty
            Default_Form_Specialty_Model Default_Form_Specialty_Model = new Default_Form_Specialty_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Specialty_Model(Specialty);

			this.Select.SelectValue("SectorId", Default_Form_Specialty_Model.SectorId.ToString());
			this.Select.SelectValue("TrainingLevelId", Default_Form_Specialty_Model.TrainingLevelId.ToString());
			var Code = b.FindElement(By.Id(nameof(Default_Form_Specialty_Model.Code)));
            Code.SendKeys(Default_Form_Specialty_Model.Code.ToString());
			var Name = b.FindElement(By.Id(nameof(Default_Form_Specialty_Model.Name)));
            Name.SendKeys(Default_Form_Specialty_Model.Name.ToString());
			var Description = b.FindElement(By.Id(nameof(Default_Form_Specialty_Model.Description)));
            Description.SendKeys(Default_Form_Specialty_Model.Description.ToString());
			var Reference = b.FindElement(By.Id(nameof(Default_Form_Specialty_Model.Reference)));
            Reference.SendKeys(Default_Form_Specialty_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	public partial class Create_Specialty_UI_Tests : Base_Create_Specialty_UI_Tests
    {
		public Create_Specialty_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Specialty_UI_Tests() : base(null){}
    }
}
