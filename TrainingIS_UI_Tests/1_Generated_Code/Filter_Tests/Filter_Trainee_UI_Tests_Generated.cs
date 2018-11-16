﻿using GApp.Core.Context;
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
using TrainingIS.Models.Trainees;

namespace TrainingIS_UI_Tests.Trainees
{
	[TestCategory("Filter_UI_Test")]
    public class Base_Filter_Trainee_UI_Tests : Base_Index_Entity_UI_Test<Trainee>
    {
        // GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        // Properties
        public bool InitData_Initlizalize = false;
        public TraineeTestDataFactory Trainee_TestData { set; get; }
        public TraineeBLO TraineeBLO { set; get; }

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
            this.UI_Test_Context.ControllerName = "/Trainees";


            // TestData and BLO
            Trainee_TestData = new TraineeTestDataFactory(this.UnitOfWork, this.GAppContext);
            TraineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
        }
        public Base_Filter_Trainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
        {
        }
 
        [TestMethod]
        public virtual void Search_Test()
        {
            // Arrange
            this.GoTo_Index_And_Login_If_Not_Ahenticated();
            string SearchText = Trainee_TestData.Get_TestData().First().Reference;

            // Acte
            this.Search(SearchText);

            // Check Resault
            this.DataTable.Init("Trainees_Entities");
            Assert.AreEqual(this.DataTable.Lines.Count, 1);
            Assert.AreEqual(this.DataTable.Lines[0][1].Text, SearchText);
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
    [TestCategory("Trainees")]
	public partial class Filter_Trainee_UI_Tests : Base_Filter_Trainee_UI_Tests
    {
		public Filter_Trainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Filter_Trainee_UI_Tests() : base(null){}
    }
}
