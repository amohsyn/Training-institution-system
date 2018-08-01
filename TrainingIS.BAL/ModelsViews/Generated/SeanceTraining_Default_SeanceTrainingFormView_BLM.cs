using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_SeanceTrainingFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeanceTrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Default_SeanceTrainingFormView Default_SeanceTrainingFormView)
        {
			SeanceTraining SeanceTraining = new SeanceTraining();
			SeanceTraining.SeancePlanningId = Default_SeanceTrainingFormView.SeancePlanningId;
			SeanceTraining.Id = Default_SeanceTrainingFormView.Id;
            return SeanceTraining;

        }
        public virtual Default_SeanceTrainingFormView ConverTo_Default_SeanceTrainingFormView(SeanceTraining SeanceTraining)
        {
            Default_SeanceTrainingFormView Default_SeanceTrainingFormView = new Default_SeanceTrainingFormView();
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
