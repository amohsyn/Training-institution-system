using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Models.Absences;

namespace TrainingIS.BLL.ModelsViews
{
    public class Create_Group_Absences_ModelBLM : BaseModelBLM
    {
        public Create_Group_Absences_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) : base(unitOfWork, GAppContext)
        {
        }

       

        public Create_Group_Absences_Model CreateInstance(DateTime AbsenceDate,string SeanceNumber_Reference )
        {
            // BLO
            SeancePlanningBLO seancePlanningBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);

            // Model
            Create_Group_Absences_Model model = new Create_Group_Absences_Model();
            model.AbsenceDate = AbsenceDate;

            // Set Schedule
            Schedule Schedule = new ScheduleBLO(this.UnitOfWork, this.GAppContext).GetExistantSchedule(AbsenceDate);
            if (Schedule == null) return model;
            model.ScheduleCode = Schedule.Reference;

            // SeanceNumber
            if (string.IsNullOrEmpty(SeanceNumber_Reference))
            {
                model.SeanceNumber = new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).GetSeanceNumber(DateTime.Now.TimeOfDay);
                if (model.SeanceNumber != null)
                    model.SeanceNumberId = model.SeanceNumber.Id;
            }
            else
            {
                model.SeanceNumber = new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByReference(SeanceNumber_Reference);
                if (model.SeanceNumber != null)
                    model.SeanceNumberId = model.SeanceNumber.Id;
            }

            // SeancePlannings
            model.SeancePlannings = seancePlanningBLO.GetSeancesPlanning(AbsenceDate, model.SeanceNumber);

 
            return model;


        }
    }
}
