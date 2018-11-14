//modelType = Default_Form_ApplicationParam_Model

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_ApplicationParam_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ApplicationParam_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ApplicationParam ConverTo_ApplicationParam(Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model)
        {
			ApplicationParam ApplicationParam = null;
            if (Default_Form_ApplicationParam_Model.Id != 0)
            {
                ApplicationParam = new ApplicationParamBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ApplicationParam_Model.Id);
            }
            else
            {
                ApplicationParam = new ApplicationParam();
            } 
			ApplicationParam.Code = Default_Form_ApplicationParam_Model.Code;
			ApplicationParam.Name = Default_Form_ApplicationParam_Model.Name;
			ApplicationParam.Value = Default_Form_ApplicationParam_Model.Value;
			ApplicationParam.Description = Default_Form_ApplicationParam_Model.Description;
			ApplicationParam.Reference = Default_Form_ApplicationParam_Model.Reference;
			ApplicationParam.Id = Default_Form_ApplicationParam_Model.Id;
            return ApplicationParam;
        }
        public virtual void ConverTo_Default_Form_ApplicationParam_Model(Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model, ApplicationParam ApplicationParam)
        {  
			 
			Default_Form_ApplicationParam_Model.toStringValue = ApplicationParam.ToString();
			Default_Form_ApplicationParam_Model.Code = ApplicationParam.Code;
			Default_Form_ApplicationParam_Model.Name = ApplicationParam.Name;
			Default_Form_ApplicationParam_Model.Value = ApplicationParam.Value;
			Default_Form_ApplicationParam_Model.Description = ApplicationParam.Description;
			Default_Form_ApplicationParam_Model.Id = ApplicationParam.Id;
			Default_Form_ApplicationParam_Model.Reference = ApplicationParam.Reference;
                     
        }

    }

	public partial class Default_Form_ApplicationParam_ModelBLM : BaseDefault_Form_ApplicationParam_Model_BLM
	{
		public Default_Form_ApplicationParam_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
