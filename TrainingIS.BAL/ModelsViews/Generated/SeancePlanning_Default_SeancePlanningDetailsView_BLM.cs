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
	public partial class BaseDefault_SeancePlanningDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeancePlanningDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeancePlanning ConverTo_SeancePlanning(Default_SeancePlanningDetailsView Default_SeancePlanningDetailsView)
        {
			SeancePlanning SeancePlanning = new SeancePlanning();
			SeancePlanning.TrainingId = Default_SeancePlanningDetailsView.TrainingId;
			SeancePlanning.SeanceDayId = Default_SeancePlanningDetailsView.SeanceDayId;
			SeancePlanning.SeanceNumberId = Default_SeancePlanningDetailsView.SeanceNumberId;
			SeancePlanning.Description = Default_SeancePlanningDetailsView.Description;
			SeancePlanning.Id = Default_SeancePlanningDetailsView.Id;
            return SeancePlanning;

        }
        public virtual Default_SeancePlanningDetailsView ConverTo_Default_SeancePlanningDetailsView(SeancePlanning SeancePlanning)
        {
            Default_SeancePlanningDetailsView Default_SeancePlanningDetailsView = new Default_SeancePlanningDetailsView();
			Default_SeancePlanningDetailsView.TrainingId = SeancePlanning.TrainingId;
			Default_SeancePlanningDetailsView.SeanceDayId = SeancePlanning.SeanceDayId;
			Default_SeancePlanningDetailsView.SeanceNumberId = SeancePlanning.SeanceNumberId;
			Default_SeancePlanningDetailsView.Description = SeancePlanning.Description;
			Default_SeancePlanningDetailsView.Id = SeancePlanning.Id;
            return Default_SeancePlanningDetailsView;            
        }
    }

	public partial class Default_SeancePlanningDetailsViewBLM : BaseDefault_SeancePlanningDetailsViewBLM
	{
		public Default_SeancePlanningDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
