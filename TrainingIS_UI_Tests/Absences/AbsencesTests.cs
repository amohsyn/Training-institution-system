using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.Absences
{
    [TestClass]
    public class AbsencesTests : Base_UI_Tests
    {

        public AbsencesTests():base("Supervisor", "Supervisor@123456", "/Absences")
        {
        }
 
        [TestMethod]
        public void Create_Absence_with_No_Created_SeanceTraining_Test()
        {
            this.IndexPage.GoTo_Index();
            this.IndexPage.Click("Create_New_Entity");

            // Select not yet created SeanceTraining 
            this.DateTimePicker.SelectDate("AbsenceDate", "12/09/2018");

            // S1
            this.Select.SelectValue("SeanceNumberId", "25");

            // TP4
            this.Select.SelectValue("ClassroomId", "39");
            this.Ajax.WaitForAjax();

            this.DataTable.Init("DataTables_Table_0");

            // Chami Moad exist
            Assert.AreEqual(this.DataTable.Lines[0][0].Text, "Chami");
            Assert.AreEqual(this.DataTable.Lines[0][1].Text, "Moad");

            // Loubna exist
            Assert.AreEqual(this.DataTable.Lines[1][1].Text, "Loubna");


        }

        //[TestMethod]
        //public void Create_Absence_with_Created_SeanceTraining_Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Edit_Absence_Statistic_Line_Test()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Give_Authorization_to_a_trainee_Test()
        //{
        //    Assert.Fail();
        //}
    }
}
