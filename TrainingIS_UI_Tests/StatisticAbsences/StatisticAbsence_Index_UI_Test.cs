using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest.Context;
using GApp.UnitTest.UI_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS_UI_Tests.StatisticAbsences
{
    [TestClass]
    [TestCategory("Absence")]
    public class StatisticAbsence_Index_UI_Test : PageTest
    {
        // GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }
        protected string GAppDataTable_Html_Id = "Absences_entities";

        // Properties
        public bool InitData_Initlizalize = false;
        public string Reference_Created_Object = null;

        public StatisticAbsence_Index_UI_Test(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
        {

        }
        public StatisticAbsence_Index_UI_Test() : base(null)
        {

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

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            // Controller Name
            this.UI_Test_Context.ControllerName = "/StatisticAbsence";
        }

        [TestMethod]
        public void StatisticAbsence_Valid_Absence_All_Group_by_Treainee_Test()
        {
            // BLO
            AbsenceBLO absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            this.GoTo_Index_And_Login_If_Not_Ahenticated();
            this.Select.SelectValue("AbsenceState", "1");
            this.Select.SelectValue("Selected_StatisticSelectors", "Trainee");
            this.Html.Click("Submit_Statistic_Absence");

            this.DataTable.Init("DataTables_Table_0");

            // Assert Absences Statisitic of 2 Trainee
            for (int i = 0; i < this.DataTable.Lines.Count && i < 10; i++)
            {
                var Line = DataTable.Lines[i];
                int Absence_Count = Convert.ToInt32( Line[5].Text);
                string Reference_Trainee = Line[0].Text;
                Trainee trainee = traineeBLO.FindBaseEntityByReference(Reference_Trainee);

                var Absences = absenceBLO.Find_Absences_By_States(trainee.Id, TrainingIS.Entities.enums.AbsenceStates.Valid_Absence);
                Assert.AreEqual(Absences.Count, Absence_Count);
            }

            // Export Test
            this.Html.Click("Export_All_Entities");
            Assert.IsFalse(this.Alert.is_Alert());
        }


    }
}
