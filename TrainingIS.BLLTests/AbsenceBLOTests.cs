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
    public class AbsenceBLOTests : Base_BLO_Tests
    {
        [TestMethod()]
        public void FindAllTest()
        {

            var ls = new AbsenceBLO(this.UnitOfWork, this.GAppContext).FindAll();
            Absence FirstAbsence = ls.FirstOrDefault();

            Absence Expected_First_Absene = this.UnitOfWork.context.Absences.OrderByDescending(a => a.UpdateDate).FirstOrDefault();

            Assert.AreEqual(FirstAbsence, Expected_First_Absene);

        }
    }
}