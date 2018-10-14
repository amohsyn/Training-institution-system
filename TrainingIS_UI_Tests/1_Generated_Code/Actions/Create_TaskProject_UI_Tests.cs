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

namespace TrainingIS_UI_Tests.TaskProjects
{
    public class Base_Create_TaskProject_UI_Tests : Create_Entity_UI_Test<TaskProject>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/TaskProjects";
            this.Entity_Reference = "TaskProject_CRUD_Test";
        }

		public Base_Create_TaskProject_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void TaskProject_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void TaskProject_Create_Test()
        {
            TaskProject_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void TaskProject_Create(TaskProject TaskProject)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert TaskProject
            Default_Form_TaskProject_Model Default_Form_TaskProject_Model = new Default_Form_TaskProject_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_TaskProject_Model(TaskProject);



			this.Select.SelectValue("ProjectId", Default_Form_TaskProject_Model.ProjectId.ToString());

			this.Select.SelectValue("TaskState", Convert.ToInt32(Default_Form_TaskProject_Model.TaskState).ToString());

	 


 
			var Name = b.FindElement(By.Id(nameof(Default_Form_TaskProject_Model.Name)));
            Name.SendKeys(Default_Form_TaskProject_Model.Name.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_TaskProject_Model.Description)));
            Description.SendKeys(Default_Form_TaskProject_Model.Description.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_TaskProject_Model.StartDate), Default_Form_TaskProject_Model.StartDate.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_TaskProject_Model.EndtDate), Default_Form_TaskProject_Model.EndtDate.ToString());

			var isPublic = b.FindElement(By.Id(nameof(Default_Form_TaskProject_Model.isPublic)));
			if (Default_Form_TaskProject_Model.isPublic)
                isPublic.Click();
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new TaskProjectTestDataFactory(null, this.GAppContext).CreateValideTaskProjectInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete TaskProject_CRUD_Test if Exist
            TaskProjectBLO TaskProjectBLO = new TaskProjectBLO(this.UnitOfWork, this.GAppContext);
            TaskProject existante_entity = TaskProjectBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                TaskProjectBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_TaskProject_UI_Tests : Base_Create_TaskProject_UI_Tests
    {
		public Create_TaskProject_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_TaskProject_UI_Tests() : base(null){}
    }
}
