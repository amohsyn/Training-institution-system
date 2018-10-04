using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.BLL.Tests
{
    [TestClass()]
    public class SeancePlanningBLOTests : Base_BLO_Tests
    {
        [TestMethod()]
        public void Find_All_Planified_SeanceTrainingTest()
        {
            SeancePlanningBLO seancePlanningBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);

            this.UnitOfWork.context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            seancePlanningBLO.Find_All_Planified_SeanceTraining();
        }
    }
}