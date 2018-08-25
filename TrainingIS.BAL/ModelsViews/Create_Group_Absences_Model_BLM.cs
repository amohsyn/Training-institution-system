using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;
using GApp.DAL;
using GApp.DAL.Exceptions;
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
            model.SeancePlannings = new List<SeancePlanning>();
            model.AbsenceDate = AbsenceDate;

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
                else
                {

                    throw new GApp.Exceptions.GAppException(string.Format("The reference {0} not exist in database", SeanceNumber_Reference));
                }
            }

            // Set Schedule
            Schedule Schedule = new ScheduleBLO(this.UnitOfWork, this.GAppContext).GetExistantSchedule(AbsenceDate);
            if (Schedule == null) return model;

            model.ScheduleCode = Schedule.Reference;

          

            if(model.SeanceNumber != null)
            {
                // SeancePlannings
                model.SeancePlannings = seancePlanningBLO.GetSeancesPlanning(AbsenceDate, model.SeanceNumber);
            }
 
            return model;
        }
    }
}
