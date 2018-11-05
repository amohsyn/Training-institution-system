using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TestData;

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

        [TestMethod()]
        public void Update_InValide_SanctionTest()
        {
            // BLO
            AbsenceBLO absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            SanctionBLO sanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);

            // Arrange - Find trainne with absences for test

            //Absence absence  = Absence_TestData.Get_Trainee_to_Test
            //Trainee trainee = absence.Trainee;


            //// Acte
            //sanctionBLO.Update_InValide_Sanction(trainee.Id);


            //Assert
            Assert.Fail();
        }
    }
}