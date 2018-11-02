using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;
using GApp.DAL;
using GApp.Models.Pages;
using TrainingIS.DAL;
using TrainingIS.Models.SeanceInfos;

namespace TrainingIS.BLL.ModelsViews
{
    public class SeanceInfoBLM : BaseModelBLM
    {
        public SeanceInfoBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) : base(unitOfWork, GAppContext)
        {

        }

        /// <summary>
        /// Fin All Seance as SeancesInfo List
        /// </summary>
        /// <param name="filterRequestParams"></param>
        /// <param name="SearchCreteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<SeanceInfo> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceTrainingBLO seanceTrainingBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            seanceTrainingBLO.Add_Former_Filter_Constraint(filterRequestParams);

            BaseDAO<SeanceInfo> SeanceInfoDAO = new BaseDAO<SeanceInfo>(this.UnitOfWork.context);

            SeanceInfoDAO.DataSource = SeancesInfo_Query();

            var Query = SeanceInfoDAO.Find(filterRequestParams, SearchCreteria, out totalRecords);

            var ls = Query.ToList();
            return ls;
        }

        /// <summary>
        /// Fin All Seance as SeancesInfo List
        /// </summary>
        /// <param name="filterRequestParams"></param>
        /// <param name="SearchCreteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public IQueryable<SeanceInfo> SeancesInfo_Query()
        {
            // Test : Former : EssarrajFouad
           

            // DayOfWeek != 0 must be parametered
            var Schedule_CalendarDay_Query = from schedule in this.UnitOfWork.context.Schedules
                                             from calendarDay in this.UnitOfWork.context.CalendarDays
                                             where DbFunctions.TruncateTime(calendarDay.Date) >= DbFunctions.TruncateTime(schedule.StartDate)
                                             && DbFunctions.TruncateTime(calendarDay.Date) <= DbFunctions.TruncateTime(schedule.EndtDate)
                                             where calendarDay.DayOfWeek != 0
                                             select new
                                             {
                                                 CalendarDay = calendarDay,
                                                 Schedule = schedule
                                             };
            // var ls_1 = Schedule_CalendarDay_Query.ToList();


            // With FormerId
            var SeancePlanning_CalendarDay_Query = from schedule_calendarDay in Schedule_CalendarDay_Query
                                                   join seancePlanning in this.UnitOfWork.context.SeancePlannings
                                                   on schedule_calendarDay.Schedule.Id equals seancePlanning.Schedule.Id
                                                   where seancePlanning.SeanceDay.Day == schedule_calendarDay.CalendarDay.DayOfWeek
                                                   select new
                                                   {
                                                       schedule_calendarDay.CalendarDay,
                                                       SeancePlanning = seancePlanning,
                                                   };
            // var ls_2 = SeancePlanning_CalendarDay_Query.ToList();



            var SeanceTraining_SeancePlanning_Query = from seancePlanning_calendarDay in SeancePlanning_CalendarDay_Query
                                                      join SeanceTraining in this.UnitOfWork.context.SeanceTrainings

                                                      on
                                                      new
                                                      {
                                                          joinProperty1 = seancePlanning_calendarDay.SeancePlanning.Id,
                                                          joinProperty2 = DbFunctions.TruncateTime(seancePlanning_calendarDay.CalendarDay.Date)

                                                      }

                                                      equals
                                                       new
                                                       {
                                                           joinProperty1 = SeanceTraining.SeancePlanning.Id,
                                                           joinProperty2 = DbFunctions.TruncateTime(SeanceTraining.SeanceDate)
                                                       }

                                                      into gj
                                                      from sub_SeanceTraining in gj.DefaultIfEmpty()
                                                          //select new 
                                                          //{
                                                          //    CalendarDay = seancePlanning_calendarDay.CalendarDay,
                                                          //    SeancePlanning = seancePlanning_calendarDay.SeancePlanning,
                                                          //    SeanceTraining = sub_SeanceTraining
                                                          //};
                                                      select new SeanceInfo
                                                      {
                                                          CalendarDay = seancePlanning_calendarDay.CalendarDay,
                                                          SeancePlanning = seancePlanning_calendarDay.SeancePlanning,
                                                          SeanceTraining = sub_SeanceTraining,
                                                          Group = seancePlanning_calendarDay.SeancePlanning.Training.Group,
                                                          ModuleTraining = seancePlanning_calendarDay.SeancePlanning.Training.ModuleTraining,
                                                          SeanceNumber = seancePlanning_calendarDay.SeancePlanning.SeanceNumber,
                                                          Id = seancePlanning_calendarDay.CalendarDay.Id,
                                                          Plurality_In_Minute = sub_SeanceTraining.Plurality
                                                      };


            // var ls_3 = SeanceTraining_SeancePlanning_Query.ToList();


            // var SeanceTeaining_Plurality_Query = from seanceTraining_seancePlanning_1 in SeanceTraining_SeancePlanning_Query
            //                                      from seanceTraining_seancePlanning_2 in SeanceTraining_SeancePlanning_Query
            //                                      where
            //                                      DbFunctions.TruncateTime(seanceTraining_seancePlanning_2.SeanceTraining.SeanceDate) <= DbFunctions.TruncateTime(seanceTraining_seancePlanning_1.SeanceTraining.SeanceDate)
            //                                      && seanceTraining_seancePlanning_1.SeancePlanning.Training.Group.Id == seanceTraining_seancePlanning_2.SeancePlanning.Training.Group.Id
            //                                      && seanceTraining_seancePlanning_1.SeancePlanning.Training.ModuleTraining.Id == seanceTraining_seancePlanning_2.SeancePlanning.Training.ModuleTraining.Id
            //                                      group new
            //                                      {
            //                                          SeanceTraining_1 = seanceTraining_seancePlanning_1.SeanceTraining,
            //                                          SeanceTraining_2 = seanceTraining_seancePlanning_2.SeanceTraining,
            //                                      }

            //                                      by seanceTraining_seancePlanning_1.SeanceTraining.Id
            //                                      into newGroup
            //                                      select new
            //                                      {
            //                                          SeanceTraining = newGroup.FirstOrDefault().SeanceTraining_1,
            //                                          Plurality_In_Minute = newGroup.Sum(s => s.SeanceTraining_2.Duration)
            //                                      };
            //// var ls_4 = SeanceTeaining_Plurality_Query.ToList();

            // var SeanceTraining_Durration_SeancePlanning_Query = from seanceTraining_seancePlanning in SeanceTraining_SeancePlanning_Query
            //                                                     join seanceTeaining_plurality in SeanceTeaining_Plurality_Query
            //                                                     on seanceTraining_seancePlanning.SeanceTraining.Id equals seanceTeaining_plurality.SeanceTraining.Id
            //                                                     into gj
            //                                                     from seanceTeaining_plurality_sub in gj.DefaultIfEmpty()

            //                                                     select new SeanceInfo
            //                                                     {
            //                                                         CalendarDay = seanceTraining_seancePlanning.CalendarDay,
            //                                                         SeancePlanning = seanceTraining_seancePlanning.SeancePlanning,
            //                                                         SeanceTraining = seanceTraining_seancePlanning.SeanceTraining,
            //                                                         Plurality_In_Minute = seanceTeaining_plurality_sub.Plurality_In_Minute,
            //                                                         Group = seanceTraining_seancePlanning.SeancePlanning.Training.Group,
            //                                                         ModuleTraining = seanceTraining_seancePlanning.SeancePlanning.Training.ModuleTraining,
            //                                                         SeanceNumber = seanceTraining_seancePlanning.SeancePlanning.SeanceNumber
            //                                                     };



            return SeanceTraining_SeancePlanning_Query;

            //var ls_SeanceInfos = SeanceTraining_Durration_SeancePlanning_Query.ToList();

            //totalRecords = ls_SeanceInfos.Count();
            //return ls_SeanceInfos; 
        }
    }
}
