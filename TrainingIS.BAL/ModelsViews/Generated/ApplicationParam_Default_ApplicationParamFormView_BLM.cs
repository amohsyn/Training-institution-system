using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_ApplicationParamFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ApplicationParamFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ApplicationParam ConverTo_ApplicationParam(Default_ApplicationParamFormView Default_ApplicationParamFormView)
        {
			ApplicationParam ApplicationParam = null;
            if (Default_ApplicationParamFormView.Id != 0)
            {
                ApplicationParam = new ApplicationParamBLO(this.UnitOfWork).FindBaseEntityByID(Default_ApplicationParamFormView.Id);
            }
            else
            {
                ApplicationParam = new ApplicationParam();
            }
			ApplicationParam.Code = Default_ApplicationParamFormView.Code;
			ApplicationParam.Name = Default_ApplicationParamFormView.Name;
			ApplicationParam.Value = Default_ApplicationParamFormView.Value;
			ApplicationParam.Description = Default_ApplicationParamFormView.Description;
			ApplicationParam.Id = Default_ApplicationParamFormView.Id;
            return ApplicationParam;
        }
        public virtual Default_ApplicationParamFormView ConverTo_Default_ApplicationParamFormView(ApplicationParam ApplicationParam)
        {  
            Default_ApplicationParamFormView Default_ApplicationParamFormView = new Default_ApplicationParamFormView();
			Default_ApplicationParamFormView.Code = ApplicationParam.Code;
			Default_ApplicationParamFormView.Name = ApplicationParam.Name;
			Default_ApplicationParamFormView.Value = ApplicationParam.Value;
			Default_ApplicationParamFormView.Description = ApplicationParam.Description;
			Default_ApplicationParamFormView.Id = ApplicationParam.Id;
            return Default_ApplicationParamFormView;            
        }
    }

	public partial class Default_ApplicationParamFormViewBLM : BaseDefault_ApplicationParamFormViewBLM
	{
		public Default_ApplicationParamFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
