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
	public partial class BaseDefault_SeanceTrainingDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeanceTrainingDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Default_SeanceTrainingDetailsView Default_SeanceTrainingDetailsView)
        {
			SeanceTraining SeanceTraining = null;
            if (Default_SeanceTrainingDetailsView.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork).FindBaseEntityByID(Default_SeanceTrainingDetailsView.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Default_SeanceTrainingDetailsView.SeanceDate;
			SeanceTraining.SeancePlanning = Default_SeanceTrainingDetailsView.SeancePlanning;
			SeanceTraining.Id = Default_SeanceTrainingDetailsView.Id;
            return SeanceTraining;
        }
        public virtual Default_SeanceTrainingDetailsView ConverTo_Default_SeanceTrainingDetailsView(SeanceTraining SeanceTraining)
        {  
			Default_SeanceTrainingDetailsView Default_SeanceTrainingDetailsView = new Default_SeanceTrainingDetailsView();
			Default_SeanceTrainingDetailsView.toStringValue = SeanceTraining.ToString();
			Default_SeanceTrainingDetailsView.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Default_SeanceTrainingDetailsView.SeancePlanning = SeanceTraining.SeancePlanning;
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
