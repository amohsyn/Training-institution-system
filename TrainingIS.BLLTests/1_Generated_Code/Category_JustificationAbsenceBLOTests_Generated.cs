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

using TrainingIS.Entities.Resources.Category_JustificationAbsenceResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_Category_JustificationAbsenceBLOTests : Base_BLO_Tests
    {
        public Category_JustificationAbsenceTestDataFactory Category_JustificationAbsence_TestData { set; get; }
		public Category_JustificationAbsenceBLO Category_JustificationAbsenceBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_Category_JustificationAbsenceBLOTests()
        {
            Category_JustificationAbsence_TestData = new Category_JustificationAbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);
            Category_JustificationAbsenceBLO = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
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
            Category_JustificationAbsence Create_Data_Test = Category_JustificationAbsenceBLO.FindBaseEntityByReference(this.Category_JustificationAbsence_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                Category_JustificationAbsenceBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_Category_JustificationAbsence_Test()
        {
            // BLO
            Category_JustificationAbsenceBLO sanctionBLO = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("Category_JustificationAbsencesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[Category_JustificationAbsenceState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Category_JustificationAbsence
            ExportService exportService = new ExportService(typeof(Category_JustificationAbsence), typeof(Default_Category_JustificationAbsence_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "Category_JustificationAbsencesController");
            var data = new Default_Category_JustificationAbsence_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Category_JustificationAbsence_Export_Model First_Exptected_Category_JustificationAbsence = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Category_JustificationAbsence);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Category_JustificationAbsence)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class Category_JustificationAbsenceBLOTests : Base_Category_JustificationAbsenceBLOTests
    {

    }
}
