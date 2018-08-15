//modelType = Default_Details_ApplicationParam_Model

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_ApplicationParam_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_ApplicationParam_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ApplicationParam ConverTo_ApplicationParam(Default_Details_ApplicationParam_Model Default_Details_ApplicationParam_Model)
        {
			ApplicationParam ApplicationParam = null;
            if (Default_Details_ApplicationParam_Model.Id != 0)
            {
                ApplicationParam = new ApplicationParamBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_ApplicationParam_Model.Id);
            }
            else
            {
                ApplicationParam = new ApplicationParam();
            } 
			ApplicationParam.Code = Default_Details_ApplicationParam_Model.Code;
			ApplicationParam.Name = Default_Details_ApplicationParam_Model.Name;
			ApplicationParam.Value = Default_Details_ApplicationParam_Model.Value;
			ApplicationParam.Description = Default_Details_ApplicationParam_Model.Description;
			ApplicationParam.Id = Default_Details_ApplicationParam_Model.Id;
            return ApplicationParam;
        }
        public virtual Default_Details_ApplicationParam_Model ConverTo_Default_Details_ApplicationParam_Model(ApplicationParam ApplicationParam)
        {  
			Default_Details_ApplicationParam_Model Default_Details_ApplicationParam_Model = new Default_Details_ApplicationParam_Model();
			Default_Details_ApplicationParam_Model.toStringValue = ApplicationParam.ToString();
			Default_Details_ApplicationParam_Model.Code = ApplicationParam.Code;
			Default_Details_ApplicationParam_Model.Name = ApplicationParam.Name;
			Default_Details_ApplicationParam_Model.Value = ApplicationParam.Value;
			Default_Details_ApplicationParam_Model.Description = ApplicationParam.Description;
			Default_Details_ApplicationParam_Model.Id = ApplicationParam.Id;
            return Default_Details_ApplicationParam_Model;            
        }

		public virtual Default_Details_ApplicationParam_Model CreateNew()
        {
            ApplicationParam ApplicationParam = new ApplicationParam();
            Default_Details_ApplicationParam_Model Default_Details_ApplicationParam_Model = this.ConverTo_Default_Details_ApplicationParam_Model(ApplicationParam);
            return Default_Details_ApplicationParam_Model;
        } 
    }

	public partial class Default_Details_ApplicationParam_ModelBLM : BaseDefault_Details_ApplicationParam_ModelBLM
	{
		public Default_Details_ApplicationParam_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
