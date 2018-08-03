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
	public partial class BaseDefault_LogWorkDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_LogWorkDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual LogWork ConverTo_LogWork(Default_LogWorkDetailsView Default_LogWorkDetailsView)
        {
			LogWork LogWork = null;
            if (Default_LogWorkDetailsView.Id != 0)
            {
                LogWork = new LogWorkBLO(this.UnitOfWork).FindBaseEntityByID(Default_LogWorkDetailsView.Id);
            }
            else
            {
                LogWork = new LogWork();
            } 
			LogWork.UserId = Default_LogWorkDetailsView.UserId;
			LogWork.OperationWorkType = Default_LogWorkDetailsView.OperationWorkType;
			LogWork.OperationReference = Default_LogWorkDetailsView.OperationReference;
			LogWork.EntityType = Default_LogWorkDetailsView.EntityType;
			LogWork.Description = Default_LogWorkDetailsView.Description;
			LogWork.Id = Default_LogWorkDetailsView.Id;
            return LogWork;
        }
        public virtual Default_LogWorkDetailsView ConverTo_Default_LogWorkDetailsView(LogWork LogWork)
        {  
			Default_LogWorkDetailsView Default_LogWorkDetailsView = new Default_LogWorkDetailsView();
			Default_LogWorkDetailsView.toStringValue = LogWork.ToString();
			Default_LogWorkDetailsView.UserId = LogWork.UserId;
			Default_LogWorkDetailsView.OperationWorkType = LogWork.OperationWorkType;
			Default_LogWorkDetailsView.OperationReference = LogWork.OperationReference;
			Default_LogWorkDetailsView.EntityType = LogWork.EntityType;
			Default_LogWorkDetailsView.Description = LogWork.Description;
			Default_LogWorkDetailsView.Id = LogWork.Id;
            return Default_LogWorkDetailsView;            
        }
    }

	public partial class Default_LogWorkDetailsViewBLM : BaseDefault_LogWorkDetailsViewBLM
	{
		public Default_LogWorkDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
