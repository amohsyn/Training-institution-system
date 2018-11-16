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

namespace TrainingIS.BLLTests
{
    public class Base_Mission_Working_Group_BLO_Tests : Base_BLO_Tests
    {
        public Mission_Working_GroupTestDataFactory Mission_Working_Group_TestData { set; get; }
        public Base_Mission_Working_Group_BLO_Tests()
        {
            Mission_Working_Group_TestData = new Mission_Working_GroupTestDataFactory(this.UnitOfWork, this.GAppContext);
           
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
    public partial class Mission_Working_Group_BLO_Tests : Base_Mission_Working_Group_BLO_Tests
    {

    }
}
