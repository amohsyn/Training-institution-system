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

using GApp.Entities.Resources.EntityPropertyShortcutResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_EntityPropertyShortcutBLOTests : Base_BLO_Tests
    {
        public EntityPropertyShortcutTestDataFactory EntityPropertyShortcut_TestData { set; get; }
        public Base_EntityPropertyShortcutBLOTests()
        {
            EntityPropertyShortcut_TestData = new EntityPropertyShortcutTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_EntityPropertyShortcut_Test()
        {
            // BLO
            EntityPropertyShortcutBLO sanctionBLO = new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("EntityPropertyShortcutsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[EntityPropertyShortcutState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_EntityPropertyShortcut
            ExportService exportService = new ExportService(typeof(EntityPropertyShortcut), typeof(Default_EntityPropertyShortcut_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "EntityPropertyShortcutsController");
            var data = new Default_EntityPropertyShortcut_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_EntityPropertyShortcut_Export_Model First_Exptected_EntityPropertyShortcut = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_EntityPropertyShortcut);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_EntityPropertyShortcut)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class EntityPropertyShortcutBLOTests : Base_EntityPropertyShortcutBLOTests
    {

    }
}
