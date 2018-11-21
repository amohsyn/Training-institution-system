using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData;
using TrainingIS.BLL.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL;
using TrainingIS.BLL.Services.Import;
using GApp.Models.Pages;
using TrainingIS.Entities;
using TrainingIS.BLL.ModelsViews;
using System.Reflection;
using System.Data;
using GApp.Entities;

using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Models.Absences;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_AbsenceBLOTests : Base_BLO_Tests
    {
        public AbsenceTestDataFactory Absence_TestData { set; get; }
        public Base_AbsenceBLOTests()
        {
            Absence_TestData = new AbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_Absence_Test()
        {
            // BLO
            AbsenceBLO sanctionBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("AbsencesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[AbsenceState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Absence
            ExportService exportService = new ExportService(typeof(Absence), typeof(Default_Absence_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "AbsencesController");
            var data = new Default_Absence_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Absence_Export_Model First_Exptected_Absence = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Absence);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Absence)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class AbsenceBLOTests : Base_AbsenceBLOTests
    {

    }
}
