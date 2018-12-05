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

using TrainingIS.Entities.Resources.WorkGroupResources;
using TrainingIS.Models.WorkGroups;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_WorkGroupBLOTests : Base_BLO_Tests
    {
        public WorkGroupTestDataFactory WorkGroup_TestData { set; get; }
		public WorkGroupBLO WorkGroupBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_WorkGroupBLOTests()
        {
            WorkGroup_TestData = new WorkGroupTestDataFactory(this.UnitOfWork, this.GAppContext);
            WorkGroupBLO = new WorkGroupBLO(this.UnitOfWork, this.GAppContext);
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
            WorkGroup Create_Data_Test = WorkGroupBLO.FindBaseEntityByReference(this.WorkGroup_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                WorkGroupBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_WorkGroup_Test()
        {
            // BLO
            WorkGroupBLO sanctionBLO = new WorkGroupBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("WorkGroupsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[WorkGroupState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_WorkGroup
            ExportService exportService = new ExportService(typeof(WorkGroup), typeof(Default_WorkGroup_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "WorkGroupsController");
            var data = new Default_WorkGroup_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_WorkGroup_Export_Model First_Exptected_WorkGroup = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_WorkGroup);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_WorkGroup)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class WorkGroupBLOTests : Base_WorkGroupBLOTests
    {

    }
}
