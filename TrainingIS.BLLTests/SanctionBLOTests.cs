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

namespace TrainingIS.BLL.Tests
{
    [TestClass()]
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
        /// </summary>
        [TestMethod()]
        public void Calculate_InValide_SanctionsTest()
        {
            // BLO
            AbsenceBLO absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            SanctionBLO sanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            SanctionCategoryBLO sanctionCategoryBLO = new SanctionCategoryBLO(this.UnitOfWork, this.GAppContext);

          

            // Arrange 
            // Find trainnees with absences for all cases
            // 1 j = 2 absences
            // 2 j, 3j, 4j, 5j, 6j, 7j, 8j, 9j, 10j
            List<Trainee> Trainees = new List<Trainee>();
            Trainee Trainee_50_abs = this.UnitOfWork.context.Trainees.Where(t => t.FirstName == "Nom_1_Specialty_1-Cours du jour-11").FirstOrDefault();
            Trainees.Add(Trainee_50_abs);
            Trainee Trainee_26_abs = this.UnitOfWork.context.Trainees.Where(t => t.FirstName == "Nom_1_Specialty_1-Cours de soir-12").FirstOrDefault();
            Trainees.Add(Trainee_26_abs);
            Trainee Trainee_13_abs = this.UnitOfWork.context.Trainees.Where(t => t.FirstName == "Nom_28_Specialty_1-Cours de soir-11").FirstOrDefault();
            Trainees.Add(Trainee_13_abs);
            Trainee Trainee_8_abs = this.UnitOfWork.context.Trainees.Where(t => t.FirstName == "Nom_1_Specialty_1-Cours du jour-21").FirstOrDefault();
            Trainees.Add(Trainee_8_abs);
            Trainee Trainee_4_abs = this.UnitOfWork.context.Trainees.Where(t => t.FirstName == "Nom_1_Specialty_1-Cours du jour-12").FirstOrDefault();
            Trainees.Add(Trainee_4_abs);


            // All CategorySanctions Cases
            List<SanctionCategory> All_CategorySanctions_Cases = sanctionCategoryBLO
                .Find_By_System_DisciplineCategory(System_DisciplineCategories.Attendance)
                .OrderBy(c => c.WorkflowOrder)
                .ToList();

            // Test 1 j

            // Test 1 j
            var InvalideSanctions_Trainee_4_abs = sanctionBLO.Calculate_InValide_Sanctions(Trainee_4_abs.Id);
            Assert.AreEqual(InvalideSanctions_Trainee_4_abs.Count, 2);
            Assert.AreEqual(InvalideSanctions_Trainee_4_abs[0].SanctionCategory.Id, All_CategorySanctions_Cases[0].Id);
            Assert.AreEqual(InvalideSanctions_Trainee_4_abs[1].SanctionCategory.Id, All_CategorySanctions_Cases[1].Id);

            
        }

        
    }
}