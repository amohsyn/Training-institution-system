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
	public partial class BaseDefault_SeanceTrainingDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeanceTrainingDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Default_SeanceTrainingDetailsView Default_SeanceTrainingDetailsView)
        {
			SeanceTraining SeanceTraining = new SeanceTraining();
			SeanceTraining.SeancePlanningId = Default_SeanceTrainingDetailsView.SeancePlanningId;
			SeanceTraining.Id = Default_SeanceTrainingDetailsView.Id;
            return SeanceTraining;

        }
        public virtual Default_SeanceTrainingDetailsView ConverTo_Default_SeanceTrainingDetailsView(SeanceTraining SeanceTraining)
        {
            Default_SeanceTrainingDetailsView Default_SeanceTrainingDetailsView = new Default_SeanceTrainingDetailsView();
			Default_SeanceTrainingDetailsView.SeancePlanningId = SeanceTraining.SeancePlanningId;
			Default_SeanceTrainingDetailsView.Id = SeanceTraining.Id;
            return Default_SeanceTrainingDetailsView;            
        }
    }

	public partial class Default_SeanceTrainingDetailsViewBLM : BaseDefault_SeanceTrainingDetailsViewBLM
	{
		public Default_SeanceTrainingDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
