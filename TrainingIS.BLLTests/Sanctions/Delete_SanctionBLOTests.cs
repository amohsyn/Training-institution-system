using GApp.UnitTest.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData;
using TestData.TestData_Descriptions;
using TrainingIS.BLL.Exceptions;
using TrainingIS.Entities;
using TrainingIS.Entities.enums;

namespace TrainingIS.BLL.Tests
{
    
    public partial class SanctionBLOTests 
    {
        [TestMethod()]
        public void Delete_First_Valid_Sanction_Test()
        {
            // BLO
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);

            // Create Valid_Sanction
            // Find The Trainee with 2 InValideSanction
            Trainee Trainee_With_2_InValide_Sanctions = traineeBLO.FindBaseEntityByReference(Sanction_TestData_Description.Trainee_With_2_InValide_Sanctions_Reference);
            var Sanctions = this.SanctionBLO.Find_InValide_Sanction(Trainee_With_2_InValide_Sanctions.Id);
            var First_Invalid_Sanction = Sanctions.OrderBy(s => s.SanctionCategory.WorkflowOrder).First();

            // Validate Sanction
            this.SanctionBLO.Validate_Sanction(First_Invalid_Sanction.Id);

            // Delete the valide Sanction
            var Absences = First_Invalid_Sanction.Absences.ToArray();
            this.SanctionBLO.Delete(First_Invalid_Sanction.Id);

            // Assert Change State of Absences to From Sanctioned to Valide
            foreach (var absence in Absences)
            {
                var absence_db = this.AbsenceBLO.FindBaseEntityByID(absence.Id);
                Assert.AreEqual(absence_db.AbsenceState, AbsenceStates.Valid_Absence);
            }
        }
        [TestMethod()]
        public void Delete_Last_Valid_Sanction_Test()
        {
            // BLO
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);

            // Create Valid_Sanction
            // Find The Trainee with 2 InValideSanction
            Trainee Trainee_With_2_InValide_Sanctions = traineeBLO.FindBaseEntityByReference(Sanction_TestData_Description.Trainee_With_2_InValide_Sanctions_Reference);
            var Sanctions = this.SanctionBLO.Find_InValide_Sanction(Trainee_With_2_InValide_Sanctions.Id);
            var First_Invalid_Sanction = Sanctions.OrderBy(s => s.SanctionCategory.WorkflowOrder).First();
            var Last_Invalid_Sanction = Sanctions.OrderBy(s => s.SanctionCategory.WorkflowOrder).Last();

            // Validate Sanction
            this.SanctionBLO.Validate_Sanction(First_Invalid_Sanction.Id);
            this.SanctionBLO.Validate_Sanction(Last_Invalid_Sanction.Id);

            // Delete the valide Sanction
            try
            {
                this.SanctionBLO.Delete(Last_Invalid_Sanction.Id);
                Assert.Fail("Must throw BLL_Exception");
            }
            catch (BLL_Exception)
            {


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
        public void Change_AbsenceState_to_Valid_When_Delete_Valide_Sanction()
        {
            // BLO
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);

            // Create Valid_Sanction
            // Find The Trainee with 2 InValideSanction
            Trainee Trainee_With_2_InValide_Sanctions = traineeBLO.FindBaseEntityByReference(Sanction_TestData_Description.Trainee_With_2_InValide_Sanctions_Reference);
            var Sanctions = this.SanctionBLO.Find_InValide_Sanction(Trainee_With_2_InValide_Sanctions.Id);
            var First_Invalid_Sanction = Sanctions.OrderBy(s => s.SanctionCategory.WorkflowOrder).First();
            var Last_Invalid_Sanction = Sanctions.OrderBy(s => s.SanctionCategory.WorkflowOrder).Last();

            // Validate Sanction
            this.SanctionBLO.Validate_Sanction(First_Invalid_Sanction.Id);
            this.SanctionBLO.Validate_Sanction(Last_Invalid_Sanction.Id);

            // Delete the valide Sanction
            var Absences = Last_Invalid_Sanction.Absences.ToArray();
            this.SanctionBLO.Delete(Last_Invalid_Sanction);

            foreach (var absence in Absences)
            {
                Assert.AreEqual(absence.AbsenceState ,AbsenceStates.Valid_Absence);
            }

        }
    }
}
