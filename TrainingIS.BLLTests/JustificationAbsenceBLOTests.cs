using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL.Tests
{
    [TestClass()]
    public class JustificationAbsenceBLOTests : Base_BLO_Tests
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
    }
}