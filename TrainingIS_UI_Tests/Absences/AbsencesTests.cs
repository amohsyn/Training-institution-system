using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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

        public AbsencesTests() : base("Supervisor", "Supervisor@123456", "/Absences")
        {
        }


        private void GotTo_Seances_S1_Index()
        {
            this.IndexPage.GoTo_Index();
            this.IndexPage.Click("Create_New_Entity");
            this.DateTimePicker.SelectDate("AbsenceDate", "10/09/2018");
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
            int Absence_Count_befor = Convert.ToInt32(this.DataTable.Lines[3][3].Text);
            int Absence_Module_Count_befor = Convert.ToInt32(this.DataTable.Lines[3][4].Text);

            this.DataTable.Lines[3].Line_Element.FindElement(By.CssSelector(".present_icon")).Click();
            this.Ajax.WaitForAjax();

            this.DataTable.Init("DataTables_Table_0");
            int Absence_Count_afer = Convert.ToInt32(this.DataTable.Lines[3][3].Text);
            int Absence_Module_Count_after = Convert.ToInt32(this.DataTable.Lines[3][4].Text);

            Assert.AreEqual(Absence_Count_befor + 1, Absence_Count_afer);
            Assert.AreEqual(Absence_Module_Count_befor + 1, Absence_Module_Count_after);

            // Assert bell exist
            this.DataTable.Lines[3][5].FindElement(By.CssSelector(".fa-bell-o"));
        }
 
    }
}
