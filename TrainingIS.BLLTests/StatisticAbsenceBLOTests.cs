using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TrainingIS.Entities;
using System.Collections;
using TrainingIS.Models.StatisticAbsence;

namespace TrainingIS.BLL.Tests
{
    [TestClass()]
    public class StatisticAbsenceBLOTests : Base_BLO_Tests
    {
        [TestMethod()]
        public void CalculateTest()
        {
            StatisticAbsenceForm StatisticAbsenceForm = new StatisticAbsenceForm();
            StatisticAbsenceForm.StartDate = DateTime.Now.AddDays(-30);
            StatisticAbsenceForm.EndDate = DateTime.Now;
            StatisticAbsenceForm.Selected_StatisticSelectors = new List<string> { nameof(Trainee), nameof(Group) };


            StatisticAbsenceBLO statisticAbsenceBLO = new StatisticAbsenceBLO(this.GAppContext);

           

            Statistic Statistic = statisticAbsenceBLO.Calculate(StatisticAbsenceForm);






        }

       
    }
}