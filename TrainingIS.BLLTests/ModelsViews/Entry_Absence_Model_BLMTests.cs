using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL.ModelsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Tests;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews.Tests
{
    [TestClass()]
    public class Entry_Absence_Model_BLMTests : Base_BLO_Tests
    {
        [TestMethod()]
        public void Get_Entry_Absence_ModelsTest()
        {
            this.UnitOfWork.context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            Entry_Absence_Model_BLM entry_Absence_Model_BLM = new Entry_Absence_Model_BLM(this.UnitOfWork, this.GAppContext);

            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByReference("TDI101-S1 [M01, ES-SARRAJ Fouad]-10/09/2018 00:00:00");

            var ls = entry_Absence_Model_BLM.Get_Entry_Absence_Models(SeanceTraining);

            Assert.AreEqual(ls.Count, 5);
            Assert.AreEqual(ls.First().AbsenceCount, 2);
            Assert.AreEqual(ls.First().AbsenceCount_In_Current_Module, 2);
        }
    }
}