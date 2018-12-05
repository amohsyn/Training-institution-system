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

using TrainingIS.Entities.Resources.ScheduleResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_ScheduleBLOTests : Base_BLO_Tests
    {
        public ScheduleTestDataFactory Schedule_TestData { set; get; }
		public ScheduleBLO ScheduleBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_ScheduleBLOTests()
        {
            Schedule_TestData = new ScheduleTestDataFactory(this.UnitOfWork, this.GAppContext);
            ScheduleBLO = new ScheduleBLO(this.UnitOfWork, this.GAppContext);
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
            Schedule Create_Data_Test = ScheduleBLO.FindBaseEntityByReference(this.Schedule_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                ScheduleBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_Schedule_Test()
        {
            // BLO
            ScheduleBLO sanctionBLO = new ScheduleBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("SchedulesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[ScheduleState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Schedule
            ExportService exportService = new ExportService(typeof(Schedule), typeof(Default_Schedule_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "SchedulesController");
            var data = new Default_Schedule_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Schedule_Export_Model First_Exptected_Schedule = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Schedule);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Schedule)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class ScheduleBLOTests : Base_ScheduleBLOTests
    {

    }
}
