using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class ScheduleBLO
    {
        /// <summary>
        /// Close the existant schedule and start the new schedule form the datetime params
        /// </summary>
        /// <param name="schedule_Start_Date_Value">The date time to start a new schedule </param>
        /// <returns>The new created schdule</returns>
        public Schedule Close_And_Start_Schedule(DateTime schedule_Start_Date_Value)
        {

            TrainingYear CurrentTrainingYear = this.GAppContext.Session[TrainingYearBLO.Current_TrainingYear_Key] as TrainingYear;
            if(CurrentTrainingYear == null)
            {
                throw new ArgumentNullException(TrainingYearBLO.Current_TrainingYear_Key);
            }

            Schedule existant_schedule = this.GetExistantSchedule(schedule_Start_Date_Value);
            if(existant_schedule != null)
            {
                existant_schedule.EndtDate = schedule_Start_Date_Value;
                this.Save(existant_schedule);
            }

            Schedule new_schedule = this.CreateInstance();
            new_schedule.StartDate = schedule_Start_Date_Value;
            new_schedule.EndtDate = CurrentTrainingYear.EndtDate ;
            new_schedule.TrainingYear = CurrentTrainingYear;
            this.Save(new_schedule);
            return new_schedule;

        }

        public Schedule GetExistantSchedule(DateTime a_date)
        {
            Schedule schedule = this._UnitOfWork.context.Schedules.Where(t => t.StartDate <= a_date && t.EndtDate >= a_date).FirstOrDefault();
            return schedule;
        }
    }
}
