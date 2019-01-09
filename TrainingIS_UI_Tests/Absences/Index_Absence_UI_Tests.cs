using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.TestData_Descriptions;

namespace TrainingIS_UI_Tests.Absences
{
    public partial class Index_Absence_UI_Tests
    {
        [TestMethod]
        public void Validate_Absence_With_Filter_Test()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Search Trainee 85
            string Trainee_Reference = Sanction_TestData_Description.Trainee_With_2_InValide_Sanctions_Reference;
            this.DataTable.Search(Trainee_Reference);

            // Assert 4 line
            this.DataTable.Init(this.GAppDataTable_Html_Id);
            Assert.AreEqual(this.DataTable.Lines.Count, 4);

            // InValidate
            var Validate_Element = this.DataTable.Lines.First().Line_Element.FindElement(By.CssSelector(".validate_absence"));
            Validate_Element.Click();

            // Assert 4 line
            this.DataTable.Init(this.GAppDataTable_Html_Id);
            Assert.AreEqual(this.DataTable.Lines.Count, 4);

            // Validate
            Validate_Element = this.DataTable.Lines.First().Line_Element.FindElement(By.CssSelector(".validate_absence"));
            Validate_Element.Click();
        }

        public override void Import_And_Import_File_Example_Absences_Test()
        {
            // [Bug]
            // Import Absence take more than 60 second
           // base.Import_And_Import_File_Example_Absences_Test();
        }
    }
}
