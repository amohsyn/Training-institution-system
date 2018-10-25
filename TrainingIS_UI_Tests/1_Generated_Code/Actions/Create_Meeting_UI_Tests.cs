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

namespace TrainingIS_UI_Tests.Meetings
{
    public class Base_Create_Meeting_UI_Tests : Create_Entity_UI_Test<Meeting>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/Meetings";
            this.Entity_Reference = "Meeting_CRUD_Test";
        }

		public Base_Create_Meeting_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void Meeting_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Meeting_Create_Test()
        {
            Meeting_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void Meeting_Create(Meeting Meeting)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Meeting
            Form_Meeting_Model Form_Meeting_Model = new Form_Meeting_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Form_Meeting_Model(Meeting);



			
			this.DateTimePicker.SelectDate(nameof(Form_Meeting_Model.MeetingDate), Form_Meeting_Model.MeetingDate.ToString());

			this.Select.SelectValue("WorkGroupId", Form_Meeting_Model.WorkGroupId.ToString());

			this.Select.SelectValue("Mission_Working_GroupId", Form_Meeting_Model.Mission_Working_GroupId.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Form_Meeting_Model.Description)));
            Description.SendKeys(Form_Meeting_Model.Description.ToString());

			var Presence_Of_President = b.FindElement(By.Id(nameof(Form_Meeting_Model.Presence_Of_President)));
			if (Form_Meeting_Model.Presence_Of_President)
                Presence_Of_President.Click();

			var Presence_Of_VicePresident = b.FindElement(By.Id(nameof(Form_Meeting_Model.Presence_Of_VicePresident)));
			if (Form_Meeting_Model.Presence_Of_VicePresident)
                Presence_Of_VicePresident.Click();

			var Presence_Of_Protractor = b.FindElement(By.Id(nameof(Form_Meeting_Model.Presence_Of_Protractor)));
			if (Form_Meeting_Model.Presence_Of_Protractor)
                Presence_Of_Protractor.Click();

			var Selected_Presences_Of_Formers = b.FindElement(By.Id(nameof(Form_Meeting_Model.Selected_Presences_Of_Formers)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_Presences_Of_Formers);
            foreach (var item in Form_Meeting_Model.Selected_Presences_Of_Formers)
            {
                selectElement.SelectByValue(item);
            }	 


			var Selected_Presences_Of_Administrators = b.FindElement(By.Id(nameof(Form_Meeting_Model.Selected_Presences_Of_Administrators)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_Presences_Of_Administrators);
            foreach (var item in Form_Meeting_Model.Selected_Presences_Of_Administrators)
            {
                selectElement.SelectByValue(item);
            }	 


			var Selected_Presences_Of_Trainees = b.FindElement(By.Id(nameof(Form_Meeting_Model.Selected_Presences_Of_Trainees)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_Presences_Of_Trainees);
            foreach (var item in Form_Meeting_Model.Selected_Presences_Of_Trainees)
            {
                selectElement.SelectByValue(item);
            }	 


			var Selected_Presences_Of_Guests_Formers = b.FindElement(By.Id(nameof(Form_Meeting_Model.Selected_Presences_Of_Guests_Formers)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_Presences_Of_Guests_Formers);
            foreach (var item in Form_Meeting_Model.Selected_Presences_Of_Guests_Formers)
            {
                selectElement.SelectByValue(item);
            }	 


			var Selected_Presences_Of_Guests_Administrators = b.FindElement(By.Id(nameof(Form_Meeting_Model.Selected_Presences_Of_Guests_Administrators)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_Presences_Of_Guests_Administrators);
            foreach (var item in Form_Meeting_Model.Selected_Presences_Of_Guests_Administrators)
            {
                selectElement.SelectByValue(item);
            }	 


			var Selected_Presences_Of_Guests_Trainees = b.FindElement(By.Id(nameof(Form_Meeting_Model.Selected_Presences_Of_Guests_Trainees)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_Presences_Of_Guests_Trainees);
            foreach (var item in Form_Meeting_Model.Selected_Presences_Of_Guests_Trainees)
            {
                selectElement.SelectByValue(item);
            }	 

 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new MeetingTestDataFactory(null, this.GAppContext).CreateValideMeetingInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete Meeting_CRUD_Test if Exist
            MeetingBLO MeetingBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);
            Meeting existante_entity = MeetingBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                MeetingBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_Meeting_UI_Tests : Base_Create_Meeting_UI_Tests
    {
		public Create_Meeting_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Meeting_UI_Tests() : base(null){}
    }
}
