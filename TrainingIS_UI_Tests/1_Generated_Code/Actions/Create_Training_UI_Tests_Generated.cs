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

namespace TrainingIS_UI_Tests.Trainings
{
    public class Base_Create_Training_UI_Tests : Create_Entity_UI_Test<Training>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public TrainingTestDataFactory Training_TestData { set; get; }
        public TrainingBLO TrainingBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/Trainings";
            this.Entity_Reference = "Training_CRUD_Test";

			// TestData and BLO
			Training_TestData = new TrainingTestDataFactory(this.UnitOfWork, this.GAppContext);
            TrainingBLO = new TrainingBLO(this.UnitOfWork, this.GAppContext);
        }

		public Base_Create_Training_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
		/// <summary>
        /// InitData well be executed one time for all TestMethod
        /// </summary>
        [TestInitialize]
        public virtual void InitData()
        {
            if (!InitData_Initlizalize)
            {
                Training_TestData.Insert_Test_Data_If_Not_Exist();
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
           Training Create_Data_Test = TrainingBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                TrainingBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Training_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Training_Create_Test()
        {
            Training_UI_Create(this.Valide_Entity_Insrance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void Training_UI_Create(Training Training)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Training
            Default_Form_Training_Model Default_Form_Training_Model = new Default_Form_Training_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Training_Model(Training);

			this.Select.SelectValue("TrainingYearId", Default_Form_Training_Model.TrainingYearId.ToString());
			this.Select.SelectValue("ModuleTrainingId", Default_Form_Training_Model.ModuleTrainingId.ToString());
			var Hourly_Mass_To_Teach = b.FindElement(By.Id(nameof(Default_Form_Training_Model.Hourly_Mass_To_Teach)));
            Hourly_Mass_To_Teach.SendKeys(Default_Form_Training_Model.Hourly_Mass_To_Teach.ToString());
			this.Select.SelectValue("FormerId", Default_Form_Training_Model.FormerId.ToString());
			this.Select.SelectValue("GroupId", Default_Form_Training_Model.GroupId.ToString());
			var Code = b.FindElement(By.Id(nameof(Default_Form_Training_Model.Code)));
            Code.SendKeys(Default_Form_Training_Model.Code.ToString());
			var Description = b.FindElement(By.Id(nameof(Default_Form_Training_Model.Description)));
            Description.SendKeys(Default_Form_Training_Model.Description.ToString());
			var Reference = b.FindElement(By.Id(nameof(Default_Form_Training_Model.Reference)));
            Reference.SendKeys(Default_Form_Training_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	public partial class Create_Training_UI_Tests : Base_Create_Training_UI_Tests
    {
		public Create_Training_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Training_UI_Tests() : base(null){}
    }
}
