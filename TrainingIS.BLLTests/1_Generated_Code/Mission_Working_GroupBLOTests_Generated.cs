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

using TrainingIS.Entities.Resources.Mission_Working_GroupResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_Mission_Working_GroupBLOTests : Base_BLO_Tests
    {
        public Mission_Working_GroupTestDataFactory Mission_Working_Group_TestData { set; get; }
		public Mission_Working_GroupBLO Mission_Working_GroupBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_Mission_Working_GroupBLOTests()
        {
            Mission_Working_Group_TestData = new Mission_Working_GroupTestDataFactory(this.UnitOfWork, this.GAppContext);
            Mission_Working_GroupBLO = new Mission_Working_GroupBLO(this.UnitOfWork, this.GAppContext);
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
            Mission_Working_Group Create_Data_Test = Mission_Working_GroupBLO.FindBaseEntityByReference(this.Mission_Working_Group_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                Mission_Working_GroupBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_Mission_Working_Group_Test()
        {
            // BLO
            Mission_Working_GroupBLO sanctionBLO = new Mission_Working_GroupBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("Mission_Working_GroupsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[Mission_Working_GroupState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Mission_Working_Group
            ExportService exportService = new ExportService(typeof(Mission_Working_Group), typeof(Default_Mission_Working_Group_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "Mission_Working_GroupsController");
            var data = new Default_Mission_Working_Group_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Mission_Working_Group_Export_Model First_Exptected_Mission_Working_Group = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Mission_Working_Group);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Mission_Working_Group)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class Mission_Working_GroupBLOTests : Base_Mission_Working_GroupBLOTests
    {

    }
}
