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

using TrainingIS.Entities.Resources.CalendarDayResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLLTests
{
    public class Base_CalendarDay_BLO_Tests : Base_BLO_Tests
    {
        public CalendarDayTestDataFactory CalendarDay_TestData { set; get; }
        public Base_CalendarDay_BLO_Tests()
        {
            CalendarDay_TestData = new CalendarDayTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_CalendarDay_Test()
        {
            // BLO
            CalendarDayBLO sanctionBLO = new CalendarDayBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("CalendarDaysController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[CalendarDayState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_CalendarDay
            ExportService exportService = new ExportService(typeof(CalendarDay), typeof(Default_CalendarDay_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "CalendarDaysController");
            var data = new Default_CalendarDay_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_CalendarDay_Export_Model First_Exptected_CalendarDay = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_CalendarDay);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_CalendarDay)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class CalendarDay_BLO_Tests : Base_CalendarDay_BLO_Tests
    {

    }
}
