using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class ApplicationParamBLO
    {
        public static string CURRENT_TrainingYear_Reference = "Current_TrainingYear_Reference";

        public void AddParam(string paramReference, string value)
        {
            ApplicationParam applicationParam = new ApplicationParam();
            applicationParam.Code = paramReference;
            applicationParam.Reference = paramReference;
            applicationParam.Value = value;
            this.Save(applicationParam);
        }

        public void ChangeCurrentTrainingYear(string Value)
        {
          
            var applicationParam = this.FindBaseEntityByReference(ApplicationParamBLO.CURRENT_TrainingYear_Reference);
            applicationParam.Value = Value;
            this.Save(applicationParam);
        }
    }
}
