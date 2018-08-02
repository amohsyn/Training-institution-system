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
	public partial class BaseDefault_SeanceTrainingFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeanceTrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Default_SeanceTrainingFormView Default_SeanceTrainingFormView)
        {
			SeanceTraining SeanceTraining = null;
            if (Default_SeanceTrainingFormView.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork).FindBaseEntityByID(Default_SeanceTrainingFormView.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            }
			SeanceTraining.SeanceDate = Default_SeanceTrainingFormView.SeanceDate;
			SeanceTraining.SeancePlanningId = Default_SeanceTrainingFormView.SeancePlanningId;
			SeanceTraining.Id = Default_SeanceTrainingFormView.Id;
            return SeanceTraining;
        }
        public virtual Default_SeanceTrainingFormView ConverTo_Default_SeanceTrainingFormView(SeanceTraining SeanceTraining)
        {  
            Default_SeanceTrainingFormView Default_SeanceTrainingFormView = new Default_SeanceTrainingFormView();
			Default_SeanceTrainingFormView.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Default_SeanceTrainingFormView.SeancePlanningId = SeanceTraining.SeancePlanningId;
			Default_SeanceTrainingFormView.Id = SeanceTraining.Id;
            return Default_SeanceTrainingFormView;            
        }
    }

	public partial class Default_SeanceTrainingFormViewBLM : BaseDefault_SeanceTrainingFormViewBLM
	{
		public Default_SeanceTrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
