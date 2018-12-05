using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using GApp.UnitTest.DataAnnotations;

namespace TrainingIS.BLL.Tests
{

    public partial class JustificationAbsenceBLOTests 
    {

        [TestMethod()]
        public void Add_Justification_to_Sanctioned_Absence()
        {

            // Arrange
            AbsenceBLO absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            JustificationAbsenceBLO justificationAbsenceBLO = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);

            Absence absence = this.UnitOfWork.context.Absences.First();
            absence.AbsenceState = Entities.enums.AbsenceStates.Sanctioned_Absence;
            absenceBLO.Save(absence);

            JustificationAbsence justificationAbsence = new JustificationAbsence();
            justificationAbsence.Trainee = absence.Trainee;
            justificationAbsence.StartDate = absence.AbsenceDate;
            justificationAbsence.EndtDate = absence.AbsenceDate.AddHours(23) ;
            justificationAbsence.Category_JustificationAbsence = this.UnitOfWork.context.Category_JustificationAbsences.First();
            justificationAbsenceBLO.Save(justificationAbsence);


            // Acte


            //Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void Add_Justification_to_Valid_Absences()
        {
            // BLO
            AbsenceBLO absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);

            JustificationAbsence justificationAbsence = this.JustificationAbsence_TestData.Create_CRUD_JustificationAbsence_Test_Instance();
            this.JustificationAbsenceBLO.Save(justificationAbsence);

            // Check Absence States is Justified_Absence
            var Absences = absenceBLO
                .Find_By_TraineeId(justificationAbsence.TraineeId)
                .Where(a => a.AbsenceDate > justificationAbsence.StartDate)
                .Where(a => a.AbsenceDate < justificationAbsence.EndtDate)
                .ToList();

            foreach (var Absence in Absences)
            {
                Assert.AreEqual(Absence.AbsenceState, Entities.enums.AbsenceStates.Justified_Absence);
            }


        }
    }
}