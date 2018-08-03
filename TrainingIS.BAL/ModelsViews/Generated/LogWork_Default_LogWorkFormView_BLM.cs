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
	public partial class BaseDefault_LogWorkFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_LogWorkFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual LogWork ConverTo_LogWork(Default_LogWorkFormView Default_LogWorkFormView)
        {
			LogWork LogWork = null;
            if (Default_LogWorkFormView.Id != 0)
            {
                LogWork = new LogWorkBLO(this.UnitOfWork).FindBaseEntityByID(Default_LogWorkFormView.Id);
            }
            else
            {
                LogWork = new LogWork();
            } 
			LogWork.UserId = Default_LogWorkFormView.UserId;
			LogWork.OperationWorkType = Default_LogWorkFormView.OperationWorkType;
			LogWork.OperationReference = Default_LogWorkFormView.OperationReference;
			LogWork.EntityType = Default_LogWorkFormView.EntityType;
			LogWork.Description = Default_LogWorkFormView.Description;
			LogWork.Id = Default_LogWorkFormView.Id;
            return LogWork;
        }
        public virtual Default_LogWorkFormView ConverTo_Default_LogWorkFormView(LogWork LogWork)
        {  
			Default_LogWorkFormView Default_LogWorkFormView = new Default_LogWorkFormView();
			Default_LogWorkFormView.toStringValue = LogWork.ToString();
			Default_LogWorkFormView.UserId = LogWork.UserId;
			Default_LogWorkFormView.OperationWorkType = LogWork.OperationWorkType;
			Default_LogWorkFormView.OperationReference = LogWork.OperationReference;
			Default_LogWorkFormView.EntityType = LogWork.EntityType;
			Default_LogWorkFormView.Description = LogWork.Description;
			Default_LogWorkFormView.Id = LogWork.Id;
            return Default_LogWorkFormView;            
        }
    }

	public partial class Default_LogWorkFormViewBLM : BaseDefault_LogWorkFormViewBLM
	{
		public Default_LogWorkFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
