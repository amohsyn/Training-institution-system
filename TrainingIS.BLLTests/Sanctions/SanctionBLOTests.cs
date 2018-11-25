using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TestData;
using TrainingIS.Entities.enums;
using System.Data;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.BLL.ModelsViews;
using GApp.Models.Pages;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.Services.Import;
using System.Reflection;
using TrainingIS.BLL.Exceptions;
using TestData.TestData_Descriptions;

namespace TrainingIS.BLL.Tests
{
    [CleanTestDB]
    public partial class SanctionBLOTests
    {
        public AbsenceTestDataFactory Absence_TestData { set; get; }
        public SanctionBLO SanctionBLO { set; get; }
        public AbsenceBLO AbsenceBLO { set; get; }

        public SanctionBLOTests():base()
        {
            Sanction_TestData = new SanctionTestDataFactory(this.UnitOfWork, this.GAppContext);
            Absence_TestData = new AbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);
            SanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            AbsenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
        }
 
        #region Export and Import Tests
        [TestMethod()]
        public virtual void Export_Sanction_Test()
        {
            // BLO
            SanctionBLO sanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("SanctionsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            filterRequestParams.FilterBy = "[SanctionState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Sanction
            ExportService exportService = new ExportService(typeof(Sanction), typeof(Export_Sanction_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "SanctionsController");
            var data = new Export_Sanction_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Export_Sanction_Model First_Exptected_Sanction = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Sanction);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Sanction)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
        #endregion

        #region Validate_Sanction Tests
        [TestMethod()]
        public void Validate_a_Valide_SanctionTest()
        {
            SanctionBLO sanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);

            // Arrange
            var Valid_Sanction = this.UnitOfWork.context.Sanctions
                .FirstOrDefault();
            Valid_Sanction.SanctionState = SanctionStates.Valid;
            sanctionBLO.Save(Valid_Sanction);

            // Acte
            try
            {
                sanctionBLO.Validate_Sanction(Valid_Sanction.Id);
                Assert.Fail("Must throw BLL_Exception");
            }
            catch (BLL_Exception e)
            {

            }
        }

        [TestMethod()]
        public void Validate_Not_First_InValide_Sanction_In_WorkFlowSanctionTest()
        {

            SanctionBLO sanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);

            // Arrange
            var Sanctions = this.UnitOfWork.context.Sanctions
                .Where(s => s.SanctionState == SanctionStates.InValid)
                .ToList();
            var Trainees_Sanctions = Sanctions.GroupBy(s => s.Trainee)
                .Select(g => new { Trainee = g.Key, Sanctions = g.ToList() })
                .Where(g => g.Sanctions.Count == 2)
                .First();
 
            // Acte
            try
            {
                var Sanction_to_valide = Trainees_Sanctions.Sanctions.OrderBy(s => s.SanctionCategory.WorkflowOrder).Last();
                sanctionBLO.Validate_Sanction(Sanction_to_valide.Id);
                Assert.Fail("Must throw BLL_Exception");
            }
            catch (BLL_Exception e)
            {

            }
        }

        [TestMethod()]
        public void Validate_Sanction_Test()
        {

            SanctionBLO sanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);

            // Arrange
            var Sanctions = this.UnitOfWork.context.Sanctions
                .Where(s => s.SanctionState == SanctionStates.InValid)
                .ToList();
            var Trainees_Sanctions = Sanctions.GroupBy(s => s.Trainee)
                .Select(g => new { Trainee = g.Key, Sanctions = g.ToList() })
                .Where(g => g.Sanctions.Count == 2)
                .First();

            // Acte
            var Sanction_to_valide = Trainees_Sanctions.Sanctions.OrderBy(s => s.SanctionCategory.WorkflowOrder).First();
            var Meeting = sanctionBLO.Validate_Sanction(Sanction_to_valide.Id);
            Assert.AreEqual(Sanction_to_valide.SanctionState ,SanctionStates.Valid);

        }
        #endregion
    }
}