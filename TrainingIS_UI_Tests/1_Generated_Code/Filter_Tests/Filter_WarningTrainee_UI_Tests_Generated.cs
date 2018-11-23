using GApp.Core.Context;
using GApp.DAL;
using GApp.Entities;
using GApp.UnitTest.Context;
using GApp.UnitTest.UI_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS_UI_Tests.Base;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.WarningTrainees
{
	[TestCategory("Filter_UI_Test")]
	[TestCategory("WarningTrainee")]
    public class Base_Filter_WarningTrainee_UI_Tests : Base_Index_Entity_UI_Test<WarningTrainee>
    {
        // GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        // Properties
        public bool InitData_Initlizalize = false;
        public WarningTraineeTestDataFactory WarningTrainee_TestData { set; get; }
        public WarningTraineeBLO WarningTraineeBLO { set; get; }

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
            this.UI_Test_Context.ControllerName = "/WarningTrainees";


            // TestData and BLO
            WarningTrainee_TestData = new WarningTraineeTestDataFactory(this.UnitOfWork, this.GAppContext);
            WarningTraineeBLO = new WarningTraineeBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = WarningTrainee_TestData.CreateValideWarningTraineeInstance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }
        public Base_Filter_WarningTrainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
        {
        }
 
        
		[TestMethod]
        public virtual void WarningTrainee_Search_Test()
        {
            // Arrange
            // Add WarningTrainee to be Edited
            this.WarningTraineeBLO.Save(this.Valide_Entity_Instance);


            this.GoTo_Index_And_Login_If_Not_Ahenticated();


            // Search the created entity
            this.DataTable.Search(this.Valide_Entity_Instance.Reference);

            // Check Resault
            this.DataTable.Init("WarningTrainees_Entities");
            Assert.AreEqual(this.DataTable.Lines.Count, 1);
        }

        protected virtual void Search(string SearchText)
        {
            string Fire_Keyup_js = string.Format("$( '#{0}' ).change();", "Search_GAppDataTable");
            var SearchInput_Element = this.Html.GetElement("Search_GAppDataTable");

            // Clear
            SearchInput_Element.Clear();
            this.JavaScript.ExecuteScript(Fire_Keyup_js);
            this.Ajax.WaitForAjax();

            // SendText
            SearchInput_Element.SendKeys(SearchText);
            this.JavaScript.ExecuteScript(Fire_Keyup_js);
            this.Ajax.WaitForAjax();
        }
    }
    [TestClass]
    [TestCategory("WarningTrainees")]
	public partial class Filter_WarningTrainee_UI_Tests : Base_Filter_WarningTrainee_UI_Tests
    {
		public Filter_WarningTrainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Filter_WarningTrainee_UI_Tests() : base(null){}
    }
}
