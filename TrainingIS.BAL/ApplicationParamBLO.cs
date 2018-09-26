using GApp.Entities;
using GApp.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrainingIS.Entities;
using GApp.XML;
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

        public FilterRequestParams Read_FilterRequestParams_State(string current_User, string current_Controller)
        {
            string ParamsReference = string.Format("{0}.{1}.{2}", current_User, current_Controller, nameof(FilterRequestParams));
            FilterRequestParams filterRequestParams = new FilterRequestParams();

            var parameter = this.FindBaseEntityByReference(ParamsReference);
            if(parameter != null)
            {
                filterRequestParams.Deserialize(parameter.Value);
            }
            return filterRequestParams;
        }

        public void Save_FilterRequestParams_State(FilterRequestParams filterRequestParams, string current_User, string current_Controller)
        {

            string ParamsReference = string.Format("{0}.{1}.{2}", current_User, current_Controller, nameof(FilterRequestParams));

            var parameter = this.FindBaseEntityByReference(ParamsReference);
            if(parameter == null)
            {
                var value = filterRequestParams.Serialize();
                this.AddParam(ParamsReference, value);
            }
            else
            {
                var value = filterRequestParams.Serialize();
                parameter.Value = value;
                this.Update(parameter);
            }
           
        }
    }
}
