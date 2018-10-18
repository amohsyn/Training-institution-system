using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest.Context;
using GApp.UnitTest.UI_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS_UI_Tests.Absences
{
    [TestClass]
    public class AbsencesTests : PageTest
    {
        #region GAppContext
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }
        #endregion


        public SeanceTraining Note_Created_SeanceTraining { set; get; }
        public AbsencesTests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
        {
          
        }

        public AbsencesTests() : base(null) {

            #region GAppContext Init
            this.UnitOfWork = new UnitOfWork<TrainingISModel>();

            // Create GAppContext Instance
            this.GAppContext = new GAppContext("Supervisor");
            TrainingYear CurrentTrainingYear = new TrainingYearBLO(this.UnitOfWork, this.GAppContext).getCurrentTrainingYear();

            // Fill GAppContext
            this.GAppContext.Session.Add(UnitOfWorkBLO.UnitOfWork_Key, this.UnitOfWork);
            this.GAppContext.Session.Add(TrainingYearBLO.Current_TrainingYear_Key, CurrentTrainingYear);
            #endregion

        }

        [TestInitialize]
        public  void InitDataBase()
        {

            Note_Created_SeanceTraining = new SeanceTraining();
            Note_Created_SeanceTraining.SeanceDate = Convert.ToDateTime("10/09/2018");
            Note_Created_SeanceTraining.SeancePlanningId = 7;
            this.CleanDataBase();
        }

        [TestCleanup]
        public void CleanDataBase()
        {
            SeanceTrainingBLO seanceTrainingBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            var Existant_Not_Create_SeanceTraining = seanceTrainingBLO.Find(this.Note_Created_SeanceTraining.SeancePlanningId, Convert.ToDateTime(this.Note_Created_SeanceTraining.SeanceDate));
            if (Existant_Not_Create_SeanceTraining != null)
                seanceTrainingBLO.Delete(Existant_Not_Create_SeanceTraining);
        }


        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.Login = "Supervisor";
            this.UI_Test_Context.Password = "Supervisor@123456";
            this.UI_Test_Context.ControllerName = "Absences";
        }

        /// <summary>
        /// Go to Seance and Select S1
        /// </summary>
        private void GotTo_Seances_S1_Index()
        {

            this.GoTo_Index_And_Login_If_Not_Ahenticated();
            this.IndexPage.Click("Create_Group_Absences");
            this.DateTimePicker.SelectDate("AbsenceDate", Convert.ToDateTime( Note_Created_SeanceTraining.SeanceDate).ToShortDateString());
            this.Select.SelectValue("SeanceNumberId", "25");
        }

        [TestMethod]
        public void Show_List_Seances_Test()
        {
            // Init
            this.GotTo_Seances_S1_Index();

            // Arrange
            this.DataTable.Init("All_Seances_Trainings_And_Plannings");

            // Assert seance count
            Assert.AreEqual(this.DataTable.Lines.Count, 2);

            // Assert Created Seance existance
            var edit_element = this.DataTable.Lines[0].Line_Element.FindElement(By.CssSelector(".edit"));

            // Assert Not Created Seabce existance
            var create_element = this.DataTable.Lines[1].Line_Element.FindElement(By.CssSelector(".create"));
        }

        [TestMethod]
        public void Create_Not_Created_SeanceTraining_Test()
        {
            // Init
            this.GotTo_Seances_S1_Index();

            // Arrange
            this.DataTable.Init("All_Seances_Trainings_And_Plannings");


            // Create Seance
            var create_element = this.DataTable.Lines[1].Line_Element.FindElement(By.CssSelector(".create"));

            // Collect Data : Note_Created_SeanceTraining
            long SeancePlanningId = Convert.ToInt64( create_element.GetAttribute("data-seance_planning_id"));
            DateTime SeanceDate = Convert.ToDateTime( create_element.GetAttribute("data-seance_date"));
            this.Note_Created_SeanceTraining.SeanceDate = SeanceDate;
            this.Note_Created_SeanceTraining.SeancePlanningId = SeancePlanningId;


            create_element.Click();
            this.Ajax.WaitForAjax();

            // Assert Existante of Table Absences
            this.DataTable.Init("DataTables_Table_0");
            Assert.AreEqual(this.DataTable.Lines.Count(), 27);

        }

        [TestMethod]
        public void Edit_Created_SeanceTraining_Test()
        {
            // Init
            this.GotTo_Seances_S1_Index();

            // Arrange
            this.DataTable.Init("All_Seances_Trainings_And_Plannings");

            // Create Seance
            var edit_element = this.DataTable.Lines[0].Line_Element.FindElement(By.CssSelector(".edit"));
            edit_element.Click();
            this.Ajax.WaitForAjax();

            // Assert Existante of Table Absences
            this.DataTable.Init("DataTables_Table_0");
            Assert.AreEqual(this.DataTable.Lines.Count(), 5);

            // Go to Create_Group_Absences page
            b.FindElement(By.Id("Chose_other_group_button")).Click();

            // Assert Seances Table existante
            this.DataTable.Init("All_Seances_Trainings_And_Plannings");
            Assert.AreEqual(this.DataTable.Lines.Count, 2);
        }

        [TestMethod]
        public void Chose_Other_Groups_Button_Test()
        {
            // Init
            this.GotTo_Seances_S1_Index();

            // Arrange
            this.DataTable.Init("All_Seances_Trainings_And_Plannings");

            // Create Seance
            var edit_element = this.DataTable.Lines[0].Line_Element.FindElement(By.CssSelector(".edit"));
            edit_element.Click();
            this.Ajax.WaitForAjax();

            // Go to Create_Group_Absences page
            b.FindElement(By.Id("Chose_other_group_button")).Click();

            // Assert Seances Table existante
            this.DataTable.Init("All_Seances_Trainings_And_Plannings");
            Assert.AreEqual(this.DataTable.Lines.Count, 2);
        }


        [TestMethod]
        public void Create_Absence_Test()
        {
            // Init
            this.GotTo_Seances_S1_Index();

            // Arrange
            this.DataTable.Init("All_Seances_Trainings_And_Plannings");

            // Create Seance
            var edit_element = this.DataTable.Lines[0].Line_Element.FindElement(By.CssSelector(".edit"));
            edit_element.Click();
            this.Ajax.WaitForAjax();

            // Statistic befor Create Absence - Madani Kamal
            this.DataTable.Init("DataTables_Table_0");
            int Absence_Count_befor = Convert.ToInt32(this.DataTable.Lines[3][4].Text);
            int Absence_Module_Count_befor = Convert.ToInt32(this.DataTable.Lines[3][5].Text);

            this.DataTable.Lines[3].Line_Element.FindElement(By.CssSelector(".present_icon")).Click();
            this.Ajax.WaitForAjax();

            this.DataTable.Init("DataTables_Table_0");
            int Absence_Count_afer = Convert.ToInt32(this.DataTable.Lines[3][4].Text);
            int Absence_Module_Count_after = Convert.ToInt32(this.DataTable.Lines[3][5].Text);

            Assert.AreEqual(Absence_Count_befor + 1, Absence_Count_afer);
            Assert.AreEqual(Absence_Module_Count_befor + 1, Absence_Module_Count_after);

            // Assert bell exist
            this.DataTable.Lines[3][6].FindElement(By.CssSelector(".fa-bell-o"));
        }
 
    }
}
