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
	public partial class BaseDefault_SeancePlanningFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeancePlanningFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeancePlanning ConverTo_SeancePlanning(Default_SeancePlanningFormView Default_SeancePlanningFormView)
        {
			SeancePlanning SeancePlanning = null;
            if (Default_SeancePlanningFormView.Id != 0)
            {
                SeancePlanning = new SeancePlanningBLO(this.UnitOfWork).FindBaseEntityByID(Default_SeancePlanningFormView.Id);
            }
            else
            {
                SeancePlanning = new SeancePlanning();
            } 
			SeancePlanning.TrainingId = Default_SeancePlanningFormView.TrainingId;
			SeancePlanning.SeanceDayId = Default_SeancePlanningFormView.SeanceDayId;
			SeancePlanning.SeanceNumberId = Default_SeancePlanningFormView.SeanceNumberId;
			SeancePlanning.Description = Default_SeancePlanningFormView.Description;
			SeancePlanning.Id = Default_SeancePlanningFormView.Id;
            return SeancePlanning;
        }
        public virtual Default_SeancePlanningFormView ConverTo_Default_SeancePlanningFormView(SeancePlanning SeancePlanning)
        {  
			Default_SeancePlanningFormView Default_SeancePlanningFormView = new Default_SeancePlanningFormView();
			Default_SeancePlanningFormView.toStringValue = SeancePlanning.ToString();
			Default_SeancePlanningFormView.TrainingId = SeancePlanning.TrainingId;
			Default_SeancePlanningFormView.SeanceDayId = SeancePlanning.SeanceDayId;
			Default_SeancePlanningFormView.SeanceNumberId = SeancePlanning.SeanceNumberId;
			Default_SeancePlanningFormView.Description = SeancePlanning.Description;
			Default_SeancePlanningFormView.Id = SeancePlanning.Id;
            return Default_SeancePlanningFormView;            
        }
    }

	public partial class Default_SeancePlanningFormViewBLM : BaseDefault_SeancePlanningFormViewBLM
	{
		public Default_SeancePlanningFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
