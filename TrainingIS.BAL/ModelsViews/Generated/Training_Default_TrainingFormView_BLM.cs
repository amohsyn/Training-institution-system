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
	public partial class BaseDefault_TrainingFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Training ConverTo_Training(Default_TrainingFormView Default_TrainingFormView)
        {
			Training Training = new Training();
			Training.TrainingYearId = Default_TrainingFormView.TrainingYearId;
			Training.ModuleTrainingId = Default_TrainingFormView.ModuleTrainingId;
			Training.FormerId = Default_TrainingFormView.FormerId;
			Training.GroupId = Default_TrainingFormView.GroupId;
			Training.Code = Default_TrainingFormView.Code;
			Training.Description = Default_TrainingFormView.Description;
			Training.Id = Default_TrainingFormView.Id;
            return Training;

        }
        public virtual Default_TrainingFormView ConverTo_Default_TrainingFormView(Training Training)
        {
            Default_TrainingFormView Default_TrainingFormView = new Default_TrainingFormView();
			Default_TrainingFormView.TrainingYearId = Training.TrainingYearId;
			Default_TrainingFormView.ModuleTrainingId = Training.ModuleTrainingId;
			Default_TrainingFormView.FormerId = Training.FormerId;
			Default_TrainingFormView.GroupId = Training.GroupId;
			Default_TrainingFormView.Code = Training.Code;
			Default_TrainingFormView.Description = Training.Description;
			Default_TrainingFormView.Id = Training.Id;
            return Default_TrainingFormView;            
        }
    }

	public partial class Default_TrainingFormViewBLM : BaseDefault_TrainingFormViewBLM
	{
		public Default_TrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
