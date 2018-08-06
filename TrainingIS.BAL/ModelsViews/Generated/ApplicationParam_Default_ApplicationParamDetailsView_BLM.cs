using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_ApplicationParamDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ApplicationParamDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ApplicationParam ConverTo_ApplicationParam(Default_ApplicationParamDetailsView Default_ApplicationParamDetailsView)
        {
			ApplicationParam ApplicationParam = null;
            if (Default_ApplicationParamDetailsView.Id != 0)
            {
                ApplicationParam = new ApplicationParamBLO(this.UnitOfWork).FindBaseEntityByID(Default_ApplicationParamDetailsView.Id);
            }
            else
            {
                ApplicationParam = new ApplicationParam();
            } 
			ApplicationParam.Code = Default_ApplicationParamDetailsView.Code;
			ApplicationParam.Name = Default_ApplicationParamDetailsView.Name;
			ApplicationParam.Value = Default_ApplicationParamDetailsView.Value;
			ApplicationParam.Description = Default_ApplicationParamDetailsView.Description;
			ApplicationParam.Id = Default_ApplicationParamDetailsView.Id;
            return ApplicationParam;
        }
        public virtual Default_ApplicationParamDetailsView ConverTo_Default_ApplicationParamDetailsView(ApplicationParam ApplicationParam)
        {  
			Default_ApplicationParamDetailsView Default_ApplicationParamDetailsView = new Default_ApplicationParamDetailsView();
			Default_ApplicationParamDetailsView.toStringValue = ApplicationParam.ToString();
			Default_ApplicationParamDetailsView.Code = ApplicationParam.Code;
			Default_ApplicationParamDetailsView.Name = ApplicationParam.Name;
			Default_ApplicationParamDetailsView.Value = ApplicationParam.Value;
			Default_ApplicationParamDetailsView.Description = ApplicationParam.Description;
			Default_ApplicationParamDetailsView.Id = ApplicationParam.Id;
            return Default_ApplicationParamDetailsView;            
        }
    }

	public partial class Default_ApplicationParamDetailsViewBLM : BaseDefault_ApplicationParamDetailsViewBLM
	{
		public Default_ApplicationParamDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
