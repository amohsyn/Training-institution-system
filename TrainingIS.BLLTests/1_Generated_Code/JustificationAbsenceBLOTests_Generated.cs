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

using TrainingIS.Entities.Resources.JustificationAbsenceResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_JustificationAbsenceBLOTests : Base_BLO_Tests
    {
        public JustificationAbsenceTestDataFactory JustificationAbsence_TestData { set; get; }
		public JustificationAbsenceBLO JustificationAbsenceBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_JustificationAbsenceBLOTests()
        {
            JustificationAbsence_TestData = new JustificationAbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);
            JustificationAbsenceBLO = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
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
            JustificationAbsence Create_Data_Test = JustificationAbsenceBLO.FindBaseEntityByReference(this.JustificationAbsence_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                JustificationAbsenceBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_JustificationAbsence_Test()
        {
            // BLO
            JustificationAbsenceBLO sanctionBLO = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("JustificationAbsencesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[JustificationAbsenceState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_JustificationAbsence
            ExportService exportService = new ExportService(typeof(JustificationAbsence), typeof(Default_JustificationAbsence_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "JustificationAbsencesController");
            var data = new Default_JustificationAbsence_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_JustificationAbsence_Export_Model First_Exptected_JustificationAbsence = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_JustificationAbsence);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_JustificationAbsence)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class JustificationAbsenceBLOTests : Base_JustificationAbsenceBLOTests
    {

    }
}
