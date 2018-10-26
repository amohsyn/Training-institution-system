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
using TrainingIS.Models.SeanceTrainings;

namespace TrainingIS_UI_Tests.SeanceTrainings
{
    public class Base_Create_SeanceTraining_UI_Tests : Create_Entity_UI_Test<SeanceTraining>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/SeanceTrainings";
            this.Entity_Reference = "SeanceTraining_CRUD_Test";
        }

		public Base_Create_SeanceTraining_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void SeanceTraining_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void SeanceTraining_Create_Test()
        {
            SeanceTraining_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void SeanceTraining_Create(SeanceTraining SeanceTraining)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert SeanceTraining
            Create_SeanceTraining_Model Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Create_SeanceTraining_Model(SeanceTraining);



			
			this.DateTimePicker.SelectDate(nameof(Create_SeanceTraining_Model.SeanceDate), Create_SeanceTraining_Model.SeanceDate.ToString());

	 


 
			var ScheduleCode = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.ScheduleCode)));
            ScheduleCode.SendKeys(Create_SeanceTraining_Model.ScheduleCode.ToString());

	 


 
			var SeanceNumberId = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.SeanceNumberId)));
            SeanceNumberId.SendKeys(Create_SeanceTraining_Model.SeanceNumberId.ToString());

	 


 
			var ClassroomId = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.ClassroomId)));
            ClassroomId.SendKeys(Create_SeanceTraining_Model.ClassroomId.ToString());

	 


 
			var GroupId = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.GroupId)));
            GroupId.SendKeys(Create_SeanceTraining_Model.GroupId.ToString());

	 


 
			var ModuleTrainingId = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.ModuleTrainingId)));
            ModuleTrainingId.SendKeys(Create_SeanceTraining_Model.ModuleTrainingId.ToString());

			this.Select.SelectValue("SeancePlanningId", Create_SeanceTraining_Model.SeancePlanningId.ToString());

	 


 
			var Contained = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.Contained)));
            Contained.SendKeys(Create_SeanceTraining_Model.Contained.ToString());

	 


 
			var SeancePlannings = b.FindElement(By.Id(nameof(Create_SeanceTraining_Model.SeancePlannings)));
            SeancePlannings.SendKeys(Create_SeanceTraining_Model.SeancePlannings.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new SeanceTrainingTestDataFactory(null, this.GAppContext).CreateValideSeanceTrainingInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete SeanceTraining_CRUD_Test if Exist
            SeanceTrainingBLO SeanceTrainingBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            SeanceTraining existante_entity = SeanceTrainingBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                SeanceTrainingBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_SeanceTraining_UI_Tests : Base_Create_SeanceTraining_UI_Tests
    {
		public Create_SeanceTraining_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_SeanceTraining_UI_Tests() : base(null){}
    }
}
