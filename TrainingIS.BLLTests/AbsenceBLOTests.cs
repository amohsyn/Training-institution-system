using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TestData.TestData_Descriptions;
using GApp.UnitTest.DataAnnotations;

namespace TrainingIS.BLL.Tests
{
    [CleanTestDB]
    public partial class AbsenceBLOTests 
    {
        TraineeBLO traineeBLO { set; get; }
        AbsenceBLO absenceBLO { set; get; }

        public AbsenceBLOTests():base()
        {
             traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
             absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
        }

        [TestMethod()]
        public void FindAllTest()
        {

            var ls = new AbsenceBLO(this.UnitOfWork, this.GAppContext).FindAll();
            Absence FirstAbsence = ls.FirstOrDefault();

            Absence Expected_First_Absene = this.UnitOfWork.context.Absences.OrderByDescending(a => a.UpdateDate).FirstOrDefault();

            Assert.AreEqual(FirstAbsence, Expected_First_Absene);

        }
        #region Validate and UnValidate Tests
        [TestMethod()]
        public void ChangeState_to_ValidTest()
        {
            var BLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);

            Absence FirstAbsence = this.UnitOfWork.context.Absences.FirstOrDefault();
            int Absences_Count = this.UnitOfWork.context.Absences.Count();

            bool isValide = FirstAbsence.Valide;
            if (isValide)
            {
                BLO.ChangeState_to_InValid(FirstAbsence);
                BLO.ChangeState_to_Valid(FirstAbsence);
            }
            else
            {
                BLO.ChangeState_to_Valid(FirstAbsence);
                BLO.ChangeState_to_InValid(FirstAbsence);
            }

            // Check if the absence not inserted tow time
            Assert.AreEqual(Absences_Count, this.UnitOfWork.context.Absences.Count());
        }
        [TestMethod()]
        public void ChangeState_to_InValid_of_ValidateAbsence_Test()
        {
            // BLO
            Trainee Trainee_With_2_InValide_Sanctions = traineeBLO.FindBaseEntityByReference(Sanction_TestData_Description.Trainee_With_2_InValide_Sanctions_Reference);
            var Absences = this.absenceBLO.Find_By_TraineeId(Trainee_With_2_InValide_Sanctions.Id);
            var Valid_Absence = Absences.First();
            this.absenceBLO.ChangeState_to_InValid(Valid_Absence);
            this.absenceBLO.ChangeState_to_Valid(Valid_Absence);

        }
        [TestMethod()]
        public void ChangeState_to_InValid_of_ValidateAbsence__With_Valid_SanctionTest()
        {
           // [Bug]
           // Assert.Fail();
        }

        [TestMethod()]
        public void ChangeState_to_ValidTest_And_InValideSanction_Change_Test()
        {
            // Assert the Change of AttendanceState.InValide State After Change the AbsenceState

            Assert.Fail();
        }
        #endregion
    }
}