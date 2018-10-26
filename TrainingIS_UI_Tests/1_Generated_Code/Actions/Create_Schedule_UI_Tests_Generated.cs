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

namespace TrainingIS_UI_Tests.Schedules
{
    public class Base_Create_Schedule_UI_Tests : Create_Entity_UI_Test<Schedule>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/Schedules";
            this.Entity_Reference = "Schedule_CRUD_Test";
        }

		public Base_Create_Schedule_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void Schedule_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Schedule_Create_Test()
        {
            Schedule_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void Schedule_Create(Schedule Schedule)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Schedule
            Default_Form_Schedule_Model Default_Form_Schedule_Model = new Default_Form_Schedule_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Schedule_Model(Schedule);



			this.Select.SelectValue("TrainingYearId", Default_Form_Schedule_Model.TrainingYearId.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Schedule_Model.StartDate), Default_Form_Schedule_Model.StartDate.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Schedule_Model.EndtDate), Default_Form_Schedule_Model.EndtDate.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_Schedule_Model.Description)));
            Description.SendKeys(Default_Form_Schedule_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new ScheduleTestDataFactory(null, this.GAppContext).CreateValideScheduleInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete Schedule_CRUD_Test if Exist
            ScheduleBLO ScheduleBLO = new ScheduleBLO(this.UnitOfWork, this.GAppContext);
            Schedule existante_entity = ScheduleBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                ScheduleBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_Schedule_UI_Tests : Base_Create_Schedule_UI_Tests
    {
		public Create_Schedule_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Schedule_UI_Tests() : base(null){}
    }
}