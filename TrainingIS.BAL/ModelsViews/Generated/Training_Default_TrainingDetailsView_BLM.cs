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
	public partial class BaseDefault_TrainingDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TrainingDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Training ConverTo_Training(Default_TrainingDetailsView Default_TrainingDetailsView)
        {
			Training Training = null;
            if (Default_TrainingDetailsView.Id != 0)
            {
                Training = new TrainingBLO(this.UnitOfWork).FindBaseEntityByID(Default_TrainingDetailsView.Id);
            }
            else
            {
                Training = new Training();
            } 
			Training.TrainingYear = Default_TrainingDetailsView.TrainingYear;
			Training.ModuleTraining = Default_TrainingDetailsView.ModuleTraining;
			Training.Former = Default_TrainingDetailsView.Former;
			Training.Group = Default_TrainingDetailsView.Group;
			Training.Code = Default_TrainingDetailsView.Code;
			Training.Description = Default_TrainingDetailsView.Description;
			Training.Id = Default_TrainingDetailsView.Id;
            return Training;
        }
        public virtual Default_TrainingDetailsView ConverTo_Default_TrainingDetailsView(Training Training)
        {  
			Default_TrainingDetailsView Default_TrainingDetailsView = new Default_TrainingDetailsView();
			Default_TrainingDetailsView.toStringValue = Training.ToString();
			Default_TrainingDetailsView.TrainingYear = Training.TrainingYear;
			Default_TrainingDetailsView.ModuleTraining = Training.ModuleTraining;
			Default_TrainingDetailsView.Former = Training.Former;
			Default_TrainingDetailsView.Group = Training.Group;
			Default_TrainingDetailsView.Code = Training.Code;
			Default_TrainingDetailsView.Description = Training.Description;
			Default_TrainingDetailsView.Id = Training.Id;
            return Default_TrainingDetailsView;            
        }
    }

	public partial class Default_TrainingDetailsViewBLM : BaseDefault_TrainingDetailsViewBLM
	{
		public Default_TrainingDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
