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

namespace TrainingIS_UI_Tests.ModuleTrainings
{
    public class Base_Create_ModuleTraining_UI_Tests : Create_Entity_UI_Test<ModuleTraining>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/ModuleTrainings";
            this.Entity_Reference = "ModuleTraining_CRUD_Test";
        }

		public Base_Create_ModuleTraining_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void ModuleTraining_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void ModuleTraining_Create_Test()
        {
            ModuleTraining_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void ModuleTraining_Create(ModuleTraining ModuleTraining)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert ModuleTraining
            Default_Form_ModuleTraining_Model Default_Form_ModuleTraining_Model = new Default_Form_ModuleTraining_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_ModuleTraining_Model(ModuleTraining);



			this.Select.SelectValue("SpecialtyId", Default_Form_ModuleTraining_Model.SpecialtyId.ToString());

			this.Select.SelectValue("MetierId", Default_Form_ModuleTraining_Model.MetierId.ToString());

			this.Select.SelectValue("YearStudyId", Default_Form_ModuleTraining_Model.YearStudyId.ToString());

	 


 
			var Name = b.FindElement(By.Id(nameof(Default_Form_ModuleTraining_Model.Name)));
            Name.SendKeys(Default_Form_ModuleTraining_Model.Name.ToString());

	 


 
			var Code = b.FindElement(By.Id(nameof(Default_Form_ModuleTraining_Model.Code)));
            Code.SendKeys(Default_Form_ModuleTraining_Model.Code.ToString());

	 


 
			var HourlyMass = b.FindElement(By.Id(nameof(Default_Form_ModuleTraining_Model.HourlyMass)));
            HourlyMass.SendKeys(Default_Form_ModuleTraining_Model.HourlyMass.ToString());

	 


 
			var Hourly_Mass_To_Teach = b.FindElement(By.Id(nameof(Default_Form_ModuleTraining_Model.Hourly_Mass_To_Teach)));
            Hourly_Mass_To_Teach.SendKeys(Default_Form_ModuleTraining_Model.Hourly_Mass_To_Teach.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_ModuleTraining_Model.Description)));
            Description.SendKeys(Default_Form_ModuleTraining_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new ModuleTrainingTestDataFactory(null, this.GAppContext).CreateValideModuleTrainingInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete ModuleTraining_CRUD_Test if Exist
            ModuleTrainingBLO ModuleTrainingBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
            ModuleTraining existante_entity = ModuleTrainingBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                ModuleTrainingBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_ModuleTraining_UI_Tests : Base_Create_ModuleTraining_UI_Tests
    {
		public Create_ModuleTraining_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_ModuleTraining_UI_Tests() : base(null){}
    }
}