using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Models.Seances;

namespace TrainingIS.BLL.ModelsViews
{
    public class SeanceModelBLM : BaseModelBLM
    {
        public SeanceModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) : base(unitOfWork, GAppContext)
        {
        }

        /// <summary>
        /// Get Seance with Created and not created SeancePlanning
        /// </summary>
        /// <param name="seanceDate"></param>
        /// <returns></returns>
        public List<SeanceModel> GetSeances(DateTime seanceDate, string seanceNumberReference )
        {
            SeanceDay seanceDay = new SeanceDayBLO(this.UnitOfWork, this.GAppContext).GetSeanceDay(seanceDate);

            // Get All SeancePlanning by SeanceDay in Current Schedule
            var SeancePlannings = from seancePlanning in this.UnitOfWork.context.SeancePlannings
                        join schedule in this.UnitOfWork.context.Schedules on seancePlanning.Schedule.Id equals schedule.Id
                        where schedule.StartDate <= seanceDate.Date && schedule.EndtDate >= seanceDate.Date
                        where seancePlanning.SeanceDayId == seanceDay.Id
                        select seancePlanning;

            // Add SeanceNumber Condition if not null
            if( !string.IsNullOrEmpty(seanceNumberReference) )
            {
                SeancePlannings = from s in SeancePlannings
                                  where s.SeanceNumber.Reference == seanceNumberReference
                                  select s;

            }

            var SeanceTrainings = from seanceTraining in this.UnitOfWork.context.SeanceTrainings
                                  where seanceTraining.SeanceDate == seanceDate.Date
                                  select seanceTraining;

            var Seances_Query = from seance_planning in SeancePlannings
                                join seance_training in SeanceTrainings on seance_planning.Id equals seance_training.SeancePlanningId
                                into SeanceTrainings_All
                                from seance_training_outer in SeanceTrainings_All.DefaultIfEmpty()
                                select new SeanceModel { SeancePlanning = seance_planning, SeanceTraining = seance_training_outer };


            var result = Seances_Query.ToList();

            return result;
        }
    }
}
