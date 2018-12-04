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

namespace TrainingIS_UI_Tests.Specialties
{
    [TestCategory("Edit_UI_Test")]
	[TestCategory("Specialty")]
    public class Base_Edit_Specialty_UI_Tests : Base_Edit_Index_Entity_UI_Test<Specialty>
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

		public Base_Edit_Specialty_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           Specialty Create_Data_Test = SpecialtyBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                SpecialtyBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Specialty_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Specialty_Edit_Test()
        {
            // Arrange
            // Add Specialty to be Edited
            this.SpecialtyBLO.Save(this.Valide_Entity_Instance);


            this.GoTo_Index_And_Login_If_Not_Ahenticated();


            // Search the created entity
            this.DataTable.Search(this.Valide_Entity_Instance.Reference);

            // Edit the entity
            this.DataTable.Init("Specialties_Entities");
			Assert.AreEqual(this.DataTable.Lines.Count, 1);
            this.DataTable.Lines[0].Edit_Element.Click();

            // Submit Edit Form
            this.Html.Click("Edit_Entity_Submit");

            // Assert
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

    }

    [TestClass]
	
	public partial class Edit_Specialty_UI_Tests : Base_Edit_Specialty_UI_Tests
    {
		public Edit_Specialty_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Edit_Specialty_UI_Tests() : base(null){}
    }
}
