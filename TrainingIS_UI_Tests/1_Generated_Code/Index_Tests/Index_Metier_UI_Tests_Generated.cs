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

namespace TrainingIS_UI_Tests.Metiers
{
    [TestCategory("Index_UI_Test")]
	[TestCategory("Metier")]
    public class Base_Index_Metier_UI_Tests : Base_Index_Entity_UI_Test<Metier>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }
		protected string GAppDataTable_Html_Id = "Metiers_entities";

		// Properties
		public bool InitData_Initlizalize = false;
        public MetierTestDataFactory Metier_TestData { set; get; }
        public MetierBLO MetierBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/Metiers";
            // this.Entity_Reference = "Metier_CRUD_Test";

			// TestData and BLO
			Metier_TestData = new MetierTestDataFactory(this.UnitOfWork, this.GAppContext);
            MetierBLO = new MetierBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            //this.Valide_Entity_Instance = Metier_TestData.CreateValideMetierInstance();
            // this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Index_Metier_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           //Metier Create_Data_Test = MetierBLO.FindBaseEntityByReference(this.Entity_Reference);
           // if (Create_Data_Test != null)
           //     MetierBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Metier_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Metier_Index_Test()
        {
           
        }
		[TestMethod]
        public virtual void Export_Metiers_Tests()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();
            this.Html.Click("Export_All_Entities");
            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
        }


		[TestMethod]
		public virtual void Import_And_Import_File_Example_Metiers_Test()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Export
            this.Html.Click("Export_Import_File_Example");
            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());

            // Import from TestMachin
            this.Html.Click("Import_Entities");
            string file_path = this.Test_Machine.Get_Downloads_Directory() + this.MetierBLO.Get_Import_File_Name();
            this.Html.Input_File("import_objects", file_path);
            this.Html.Click("Import_Submit");
         
            // Assert updated_rows 
            Assert.IsTrue(this.Elements.IsElementIdExist("Number_of_updated_rows"));

            // Assert Update only without eroors
            Assert.IsFalse(this.Elements.IsElementIdExist("Number_of_inserted_erros_rows"));
            Assert.IsFalse(this.Elements.IsElementIdExist("Number_of_updated_erros_rows"));
            Assert.IsFalse(this.Elements.IsElementIdExist("Number_of_inserted_rows"));
        }

    }

    [TestClass]
	
	public partial class Index_Metier_UI_Tests : Base_Index_Metier_UI_Tests
    {
		public Index_Metier_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Index_Metier_UI_Tests() : base(null){}
    }
}
