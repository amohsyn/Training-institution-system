using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class TrainingYearBLO
    {
        public static string Current_TrainingYear_Key { get; set; } = "CurrentTrainingYear";

        public override TrainingYear CreateInstance()
        {
            TrainingYear trainingYear = base.CreateInstance();
            trainingYear.StartDate = DateTime.Now;
            trainingYear.EndtDate = DateTime.Now.AddDays(+365);
            return trainingYear;
        }

        public override int Save(TrainingYear item)
        {
            var r = base.Save(item);

            CalendarDayBLO calendarDayBLO = new CalendarDayBLO(this._UnitOfWork, this.GAppContext);
            calendarDayBLO.Fill_CalendarDay(item.StartDate.Date, item.EndtDate.Date);
            return r;


        }

        public TrainingYear getCurrentTrainingYear()
        {

            ApplicationParamBLO applicationParamBLO = new ApplicationParamBLO(this._UnitOfWork, this.GAppContext);
            ApplicationParam CurrentTrainingYear_Param = applicationParamBLO
                .FindBaseEntityByReference(ApplicationParamBLO.CURRENT_TrainingYear_Reference);

            // if param exist
            if (CurrentTrainingYear_Param != null)
            {
                var CurrentTrainingYear = this.FindBaseEntityByReference(CurrentTrainingYear_Param.Value);
                if (CurrentTrainingYear != null)
                    return CurrentTrainingYear;
                else
                {
                    // Delete Not Wel params
                    applicationParamBLO.Delete(CurrentTrainingYear_Param.Id);
                }
            }

            TrainingISModel trainingISModel = (TrainingISModel)this.getContext();
            var Query = from t in trainingISModel.TrainingYears
                        where t.CreateDate < DateTime.Now && t.EndtDate > DateTime.Now
                        select t;
            var currentTrainingYear = Query.FirstOrDefault();

            if (currentTrainingYear != null)
            {
                applicationParamBLO.AddParam(ApplicationParamBLO.CURRENT_TrainingYear_Reference, currentTrainingYear.Reference);
                return currentTrainingYear;
            }
            return null;




        }
    }
}
