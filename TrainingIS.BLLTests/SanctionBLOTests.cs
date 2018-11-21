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

namespace TrainingIS.BLL.Tests
{
    [CleanTestDB]
    public partial class SanctionBLOTests
    {
        public AbsenceTestDataFactory Absence_TestData { set; get; }

        

        public SanctionBLOTests():base()
        {
            Sanction_TestData = new SanctionTestDataFactory(this.UnitOfWork, this.GAppContext);
            Absence_TestData = new AbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);
        }

        /// <summary>
        /// Test  for Calculate Invalide Sanctions for Attendance_DisciplineCategory
        ///  
        /// </summary>
        [TestMethod()]
        public void Calculate_InValide_Sanctions_For_Absences_without_ValideSanction_Test()
        {

            // BLO
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            AbsenceBLO absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            SanctionBLO sanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            SanctionCategoryBLO sanctionCategoryBLO = new SanctionCategoryBLO(this.UnitOfWork, this.GAppContext);

            // TestData
            AbsenceTestDataFactory absenceTestDataFactory = new AbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);

            var SeanceTrainings = this.UnitOfWork.context.SeanceTrainings.ToList();
            foreach (var SeanceTraining in SeanceTrainings)
            {
                // Arrage
                var Trainees = SeanceTraining.SeancePlanning.Training.Group.Trainees.OrderBy(t => t.Ordre).ToList();
                var trainee = Trainees.First();
                var Absences = absenceBLO.Find_By_TraineeId(trainee.Id).Where(a => a.AbsenceState == AbsenceStates.Valid_Absence).ToList();
                var Attendance_Sanctions_Categories = sanctionCategoryBLO.Find_By_System_DisciplineCategory(System_DisciplineCategories.Attendance)
                    .OrderBy(c => c.WorkflowOrder)
                    .ToList();

                // Exprected 
                int Exprected_Sanctions_Count = this.Calculate_Exptected_InvalideSanction(Absences);

                // Acte
                var Invalide_Sanctions = sanctionBLO.Calculate_InValide_Sanctions(trainee.Id);
                var Invalide_Sanctions_Count = Invalide_Sanctions.Count();
                // Assert 
                // Assert the trainee have absence
                Assert.IsTrue(Absences.Count > 0);

                Assert.AreEqual(Exprected_Sanctions_Count, Invalide_Sanctions_Count);

                // Assert - SanctionCategory
                for (int i = 0; i < Invalide_Sanctions.Count; i++)
                {
                    Assert.AreEqual(Invalide_Sanctions[i].SanctionCategory, Attendance_Sanctions_Categories[i]);
                }

            }

        }

        [TestMethod()]
        public void Calculate_InValide_Sanctions_For_Absences_with_ValideSanction_Test()
        {
            // Calcule the invalide sanction for the trainne that have already Valide_Sanctions
            Assert.Fail();
        }
        private int Calculate_Exptected_InvalideSanction(List<Absence> absences)
        {
            var Validate_Absence = absences.Where(a => a.AbsenceState == AbsenceStates.Valid_Absence)
                .ToList();
            int DayNumber = Validate_Absence.Count / 2;
            int Sanction_Count = DayNumber;
            if (Sanction_Count > 10) Sanction_Count = 10;
            return Sanction_Count;
        }

        [TestMethod()]
        public void Update_InValide_SanctionTest()
        {
            // BLO
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            SanctionBLO sanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);

            // TestData
            AbsenceTestDataFactory absenceTestDataFactory = new AbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);

            var SeanceTrainings = this.UnitOfWork.context.SeanceTrainings.ToList();
            var First_Trainee_of_All_SeanceTrainings = this.UnitOfWork
                .context
                .SeanceTrainings
                .Select(s => s.SeancePlanning
                    .Training
                    .Group
                    .Trainees
                    .OrderBy(t => t.Ordre)
                    .FirstOrDefault())
                .ToList();
            // Distinc
            var First_Trainee_of_All_SeanceTrainings_Distinct = First_Trainee_of_All_SeanceTrainings.Distinct();


            foreach (var trainee in First_Trainee_of_All_SeanceTrainings_Distinct)
            {
                // Acte
                sanctionBLO.Update_InValide_Sanction(trainee.Id);

            }
        }

        [TestMethod()]
        public void Delete_InValide_SanctionTest()
        {
            // BLO
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            SanctionBLO sanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);

            // TestData
            AbsenceTestDataFactory absenceTestDataFactory = new AbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);

            var SeanceTrainings = this.UnitOfWork.context.SeanceTrainings.ToList();
            foreach (var SeanceTraining in SeanceTrainings)
            {
                // Arrage
                var Trainees = SeanceTraining.SeancePlanning.Training.Group.Trainees.OrderBy(t => t.Ordre).ToList();
                var trainee = Trainees.First();
                var Invalide_Sanction_Count = sanctionBLO.Find_InValide_Sanction(trainee.Id);
                // Acte
                int Deleted_Invalide_Sanction = sanctionBLO.Delete_InValide_Sanction(trainee.Id);

                //Assert
                Assert.AreEqual(Invalide_Sanction_Count, Deleted_Invalide_Sanction);

            }
        }

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
            sanctionBLO.Validate_Sanction(Sanction_to_valide.Id);

        }
    }
}