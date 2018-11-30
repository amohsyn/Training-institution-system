using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest.Attributes;
using GApp.UnitTest.Context;
using GApp.UnitTest.UI_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestData.TestData_Descriptions;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS_UI_Tests.SeanceTrainings
{
    [TestClass]
    [TestCategory("SeanceTrainings")]
    public class CreateSeanceTrainings_By_Former_Tests : PageTest
    {

        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        protected SeanceTrainingBLO SeanceTrainingBLO { set; get; }

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

            SeanceTrainingBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);


            this.UI_Test_Context.Login = "Formateur13@gapp.com";
            this.UI_Test_Context.Password = "Formateur@123456";
            this.UI_Test_Context.ControllerName = "SeanceTrainings";

        }

        public CreateSeanceTrainings_By_Former_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
        {
        }

        public CreateSeanceTrainings_By_Former_Tests() : base(null) { }



        [TestMethod]
        public void Add_Existante_SeanceTraining()
        {

            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Select SeanceDate : 
            this.DateTimePicker.SelectDate("SeanceDate", "03/09/2018");

            // Select S2
            this.Select.SelectValue("SeanceNumberId", "2");

            // Save Clic
            b.FindElement(By.Id("Create_Seance_Training_Submit")).Click();

            this.Ajax.WaitForAjax();

            string Liste_Of_Absence_Title = b.FindElement(By.XPath("//div[@id='Absences_Trainees']/h2[1]")).Text;
            Assert.AreEqual(Liste_Of_Absence_Title, "Liste des stagiaires");
        }

        [TestMethod]
        public void Add_Absence_And_Statistic_Change_Test()
        {
            this.Add_Existante_SeanceTraining();

            this.DataTable.Init("DataTables_Table_0");

            // Nom_102
            var Number_of_Absence = Convert.ToInt32(this.DataTable.Lines[2][4].Text);
            var Number_of_Absence_In_Current_Module = Convert.ToInt32(this.DataTable.Lines[2][5].Text);

            this.DataTable.Lines[2].Line_Element.FindElement(By.CssSelector(".present_icon")).Click();
            this.Ajax.WaitForAjax();

            this.DataTable.Init("DataTables_Table_0");

            var Number_of_Absence_After_Click = Convert.ToInt32(this.DataTable.Lines[2][4].Text);
            var Number_of_Absence_In_Current_Module_After_Click = Convert.ToInt32(this.DataTable.Lines[2][5].Text);

            Assert.AreEqual(Number_of_Absence +1, Number_of_Absence_After_Click  );
            Assert.AreEqual(Number_of_Absence_In_Current_Module +1, Number_of_Absence_In_Current_Module_After_Click );

            // Clean Data
            this.DataTable.Lines[2].Line_Element.FindElement(By.CssSelector(".absent_icon")).Click();

            // Assert Statistic after click
            var Number_of_Absence_Delete_Absence_Click = Convert.ToInt32(this.DataTable.Lines[2][4].Text);
            var Number_of_Absence_In_Current_Module_After_Delete_Absence_Click = Convert.ToInt32(this.DataTable.Lines[2][5].Text);


        }

        [TestMethod]
        public void Absence_Notification_Test()
        {
            this.Add_Existante_SeanceTraining();

            this.DataTable.Init("DataTables_Table_0");

            // Trainee : Nom_107
            string bell_xpath = "//tr[8]/td[7]/Span[1]";
            bool is_bell_exist = this.Elements.IsElementExist(By.XPath(bell_xpath));

            // Assert bill not exist
            Assert.IsFalse(is_bell_exist);

            // Act : Add absence Nom_107
            this.Ajax.AjaxClick("//tr[8]/td[8]/Span[1]");

            // Assert bill exist
            is_bell_exist = this.Elements.IsElementExist(By.XPath(bell_xpath));
            Assert.IsTrue(is_bell_exist);

            // Delete Absence from Nom_107
            this.Ajax.AjaxClick("//tr[8]/td[8]/Span[1]");
            is_bell_exist = this.Elements.IsElementExist(By.XPath(bell_xpath));
            Assert.IsFalse(is_bell_exist);
        }


        [TestMethod]
        public void Save_The_Content_Of_SeanceTraining_Test()
        {
            this.Add_Existante_SeanceTraining();
            b.FindElement(By.Id("Contained")).Clear();
            b.FindElement(By.Id("Contained")).SendKeys("Introduction à la formation");
            b.FindElement(By.Id("Edit_Entity_Submit")).Click();
            Assert.IsTrue(this.Alert.Is_Info_Alert());
            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            this.Alert.Close();
        }

        [TestMethod]
        public void Create_New_Seance_Planning_Test()
        {
            this.Clean_Data_Create_New_SeanceTraining();

            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Select SeanceDate : 
            this.DateTimePicker.SelectDate("SeanceDate", "04/09/2018");


            // Select S3
            this.Select.SelectValue("SeanceNumberId", "3");

            // Save Clic
            b.FindElement(By.Id("Create_Seance_Training_Submit")).Click();

            // Assert Info Alert
            Assert.IsTrue(this.Alert.Is_Info_Alert());
            this.Alert.Close();

            // Clean Data
            this.Html.Click("Back_to_List");

            this.Clean_Data_Create_New_SeanceTraining();


        }

        private void Clean_Data_Create_New_SeanceTraining()
        {
            // Delete Created SeanceTraining
            DateTime SeanceDate = Convert.ToDateTime("04/09/2018");
            Former former = new FormerBLO(this.UnitOfWork, this.GAppContext)
                .FindBaseEntityByReference(Former_TestData_Description.Former_SeanceTraining_Test_Reference);
            SeanceNumber seanceNumber = new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByReference("S3");
            SeanceTraining seanceTraining = this.SeanceTrainingBLO.Find_By_Former_Date_Seance(former, SeanceDate, seanceNumber);
            if (seanceTraining != null)
                this.SeanceTrainingBLO.Delete(seanceTraining);
        }
    }
}
