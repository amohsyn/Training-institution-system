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
	public partial class BaseDefault_TrainingDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TrainingDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Training ConverTo_Training(Default_TrainingDetailsView Default_TrainingDetailsView)
        {
			Training Training = new Training();
			Training.TrainingYearId = Default_TrainingDetailsView.TrainingYearId;
			Training.ModuleTrainingId = Default_TrainingDetailsView.ModuleTrainingId;
			Training.FormerId = Default_TrainingDetailsView.FormerId;
			Training.GroupId = Default_TrainingDetailsView.GroupId;
			Training.Code = Default_TrainingDetailsView.Code;
			Training.Description = Default_TrainingDetailsView.Description;
			Training.Id = Default_TrainingDetailsView.Id;
            return Training;

        }
        public virtual Default_TrainingDetailsView ConverTo_Default_TrainingDetailsView(Training Training)
        {
            Default_TrainingDetailsView Default_TrainingDetailsView = new Default_TrainingDetailsView();
			Default_TrainingDetailsView.TrainingYearId = Training.TrainingYearId;
			Default_TrainingDetailsView.ModuleTrainingId = Training.ModuleTrainingId;
			Default_TrainingDetailsView.FormerId = Training.FormerId;
			Default_TrainingDetailsView.GroupId = Training.GroupId;
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
