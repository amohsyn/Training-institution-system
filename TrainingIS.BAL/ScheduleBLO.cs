﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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

           

            if (CurrentTrainingYear == null)
            {
                throw new ArgumentNullException(TrainingYearBLO.Current_TrainingYear_Key);
            }

            // Load CurrentTriaingYear with current Context
            CurrentTrainingYear = new TrainingYearBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(CurrentTrainingYear.Id);


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
            // Delete Time
            a_date = a_date.Date;

            Schedule schedule = this._UnitOfWork.context.Schedules.Where(t => t.StartDate <= a_date && t.EndtDate >= a_date).FirstOrDefault();
            return schedule;
        }

        public override int Delete(Schedule item)
        {
            // BLO
            SeancePlanningBLO seancePlanningBLO = new SeancePlanningBLO(this._UnitOfWork, this.GAppContext);


            int return_value = 0;
            // Transaction Delete 
            
            using(TransactionScope transactionScope = new TransactionScope())
            {
                // Delete All SeancePlanning if possible

                var SeancePlannings = item.SeancePlannings.ToArray();
                for (int i = 0; i < SeancePlannings.Count(); i++)
                {
                    seancePlanningBLO.Delete(SeancePlannings[i]);
                }
                 

                return_value = base.Delete(item);

                transactionScope.Complete();
            }

            return return_value;
        }
    }
}
