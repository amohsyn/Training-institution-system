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

namespace TrainingIS_UI_Tests.TaskProjects
{
    [TestCategory("Create_UI_Test")]
    [TestCategory("TaskProject")]
    public class Base_Create_TaskProject_UI_Tests : Base_Create_Entity_UI_Test<TaskProject>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public TaskProjectTestDataFactory TaskProject_TestData { set; get; }
        public TaskProjectBLO TaskProjectBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/TaskProjects";
            this.Entity_Reference = "TaskProject_CRUD_Test";

			// TestData and BLO
			TaskProject_TestData = new TaskProjectTestDataFactory(this.UnitOfWork, this.GAppContext);
            TaskProjectBLO = new TaskProjectBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = TaskProject_TestData.CreateValideTaskProjectInstance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_TaskProject_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           TaskProject Create_Data_Test = TaskProjectBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                TaskProjectBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void TaskProject_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void TaskProject_Create_Test()
        {
            TaskProject_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void TaskProject_UI_Create(TaskProject TaskProject)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert TaskProject
            Default_TaskProject_Create_Model Default_TaskProject_Create_Model = new Default_TaskProject_Create_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_TaskProject_Create_Model(TaskProject);

			this.Select.SelectValue("ProjectId", Default_TaskProject_Create_Model.ProjectId.ToString());
			this.Select.SelectValue("TaskState", Convert.ToInt32(Default_TaskProject_Create_Model.TaskState).ToString());
			var Name = b.FindElement(By.Id(nameof(Default_TaskProject_Create_Model.Name)));
            Name.SendKeys(Default_TaskProject_Create_Model.Name.ToString());
			var Description = b.FindElement(By.Id(nameof(Default_TaskProject_Create_Model.Description)));
            Description.SendKeys(Default_TaskProject_Create_Model.Description.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_TaskProject_Create_Model.StartDate), Default_TaskProject_Create_Model.StartDate.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_TaskProject_Create_Model.EndtDate), Default_TaskProject_Create_Model.EndtDate.ToString());
			var isPublic = b.FindElement(By.Id(nameof(Default_TaskProject_Create_Model.isPublic)));
			if (Default_TaskProject_Create_Model.isPublic)
                isPublic.Click();
			var Reference = b.FindElement(By.Id(nameof(Default_TaskProject_Create_Model.Reference)));
            Reference.SendKeys(Default_TaskProject_Create_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	
	public partial class Create_TaskProject_UI_Tests : Base_Create_TaskProject_UI_Tests
    {
		public Create_TaskProject_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_TaskProject_UI_Tests() : base(null){}
    }
}
