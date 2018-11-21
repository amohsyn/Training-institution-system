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

using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_GroupBLOTests : Base_BLO_Tests
    {
        public GroupTestDataFactory Group_TestData { set; get; }
        public Base_GroupBLOTests()
        {
            Group_TestData = new GroupTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_Group_Test()
        {
            // BLO
            GroupBLO sanctionBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("GroupsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[GroupState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Group
            ExportService exportService = new ExportService(typeof(Group), typeof(Default_Group_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "GroupsController");
            var data = new Default_Group_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Group_Export_Model First_Exptected_Group = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Group);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Group)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class GroupBLOTests : Base_GroupBLOTests
    {

    }
}
