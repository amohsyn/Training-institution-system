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

namespace TrainingIS.BLL.Tests
{
    public class Base_CalendarDayBLOTests : Base_BLO_Tests
    {
        public CalendarDayTestDataFactory CalendarDay_TestData { set; get; }
		public CalendarDayBLO CalendarDayBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_CalendarDayBLOTests()
        {
            CalendarDay_TestData = new CalendarDayTestDataFactory(this.UnitOfWork, this.GAppContext);
            CalendarDayBLO = new CalendarDayBLO(this.UnitOfWork, this.GAppContext);
        }
 

        [TestInitialize]
        public virtual void InitData()
        {
            if (!InitData_Initlizalize)
            {
                this.CleanData();
                InitData_Initlizalize = true;
            }

        }

        [TestCleanup]
        public virtual void CleanData()
        {
            // Clean Create Data Test
            CalendarDay Create_Data_Test = CalendarDayBLO.FindBaseEntityByReference(this.CalendarDay_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                CalendarDayBLO.Delete(Create_Data_Test);
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
    public partial class CalendarDayBLOTests : Base_CalendarDayBLOTests
    {

    }
}
