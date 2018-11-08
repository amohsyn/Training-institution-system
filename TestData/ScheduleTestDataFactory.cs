using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class ScheduleTestDataFactory
    {
        protected override List<Schedule> Generate_TestData()
        {
            ScheduleBLO ScheduleBLO = new ScheduleBLO(this.UnitOfWork, this.GAppContext);

            List<Schedule> Data = new List<Schedule>();

            var TrainingYears = this.UnitOfWork.context.TrainingYears.ToList();


            foreach (var TrainingYear in TrainingYears)
            {
                DateTime Start_TrainingYear = TrainingYear.StartDate;
                DateTime End_TrainingYear = TrainingYear.EndtDate;

                int jours = 15;
                while (Start_TrainingYear  < End_TrainingYear)
                {
                    Schedule schedule = ScheduleBLO.CreateInstance();
                    schedule.TrainingYear = TrainingYear;
                    schedule.StartDate = Start_TrainingYear;

                    // End Schedule 
                    jours = jours + 15;
                    Start_TrainingYear = Start_TrainingYear.AddDays(jours);
                    schedule.EndtDate = Start_TrainingYear;
                    schedule.Reference = schedule.CalculateReference();
                    Data.Add(schedule);
                }
            }

            return Data;
        }
    }
}
