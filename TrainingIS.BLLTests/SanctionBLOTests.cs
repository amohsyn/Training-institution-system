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

namespace TrainingIS.BLL.Tests
{
    [TestClass()]
    [CleanTestDB]
    public class SanctionBLOTests : Base_BLO_Tests
    {
        public SanctionTestDataFactory Sanction_TestData { set; get; }
        public AbsenceTestDataFactory Absence_TestData { set; get; }

        public SanctionBLOTests()
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
        public void Export_Sanction_Test()
        {
            SanctionBLO sanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("SanctionsController");

            //FilterRequestParams filterRequestParams = new FilterRequestParams();
            //filterRequestParams.FilterBy = "[SanctionState,0]";

            var filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(null, "SanctionsController");
            var data = new Default_Sanction_Index_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int i);

            Assert.AreEqual(dataTable.Columns.Count, 8);

            // Check First Data row
            // First Name
            Assert.AreEqual(dataTable.Rows[1][1].ToString(), data.First().Trainee.FirstName);
            // Sanction Name
            Assert.AreEqual(dataTable.Rows[1][5].ToString(), data.First().SanctionCategory.Name);


        }
    }
}