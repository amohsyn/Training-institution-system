using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TrainingIS_UI_Tests.Absences
{
    public partial class Filter_Absence_UI_Tests
    {
   
        public override void Absence_Search_Test()
        {
            Absence absence = this.AbsenceBLO.FindBaseEntityByReference(TestData.TestData_Descriptions.Absence_TestData_Description.Filter_Tests_Absence_Reference);
    
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Search the created entity
            this.DataTable.Search(absence.Reference);

            // Check Resault
            this.DataTable.Init("Absences_Entities");
            Assert.AreEqual(this.DataTable.Lines.Count, 1);
        }
    }
}
