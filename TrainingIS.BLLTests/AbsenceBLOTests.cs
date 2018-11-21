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
   
    public partial class AbsenceBLOTests 
    {
        [TestMethod()]
        public void FindAllTest()
        {

            var ls = new AbsenceBLO(this.UnitOfWork, this.GAppContext).FindAll();
            Absence FirstAbsence = ls.FirstOrDefault();

            Absence Expected_First_Absene = this.UnitOfWork.context.Absences.OrderByDescending(a => a.UpdateDate).FirstOrDefault();

            Assert.AreEqual(FirstAbsence, Expected_First_Absene);

        }

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
        public void ChangeState_to_ValidTest_And_InValideSanction_Change_Test()
        {
            // Assert the Change of AttendanceState.InValide State After Change the AbsenceState

            Assert.Fail();
        }
    }
}