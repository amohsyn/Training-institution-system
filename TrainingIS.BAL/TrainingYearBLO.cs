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
        public TrainingYear getCurrentTrainingYear()
        {
          
            ApplicationParamBLO applicationParamBLO = new ApplicationParamBLO(this._UnitOfWork);
            ApplicationParam CurrentTrainingYear_Param = applicationParamBLO
                .FindBaseEntityByReference(ApplicationParamBLO.CURRENT_TrainingYear_Reference);

            // if param exist
            if(CurrentTrainingYear_Param != null)
            {
                var CurrentTrainingYear = this.FindBaseEntityByReference(CurrentTrainingYear_Param.Value);
                return CurrentTrainingYear;
            }
            else
            {
                TrainingISModel trainingISModel = (TrainingISModel)this.getContext();
                var Query = from t in trainingISModel.TrainingYears
                            where t.CreateDate < DateTime.Now && t.EndtDate > DateTime.Now
                            select t;
                var currentTrainingYear = Query.FirstOrDefault();

                if(currentTrainingYear != null)
                {
                    applicationParamBLO.AddParam(ApplicationParamBLO.CURRENT_TrainingYear_Reference, currentTrainingYear.Reference);
                    return currentTrainingYear;
                }
                return null;
            }


          
        }
    }
}
