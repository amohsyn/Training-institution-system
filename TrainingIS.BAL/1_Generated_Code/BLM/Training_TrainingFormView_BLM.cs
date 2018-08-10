using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseTrainingFormViewBLM : ViewModelBLM
    {
        
        public BaseTrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Training ConverTo_Training(TrainingFormView TrainingFormView)
        {
			Training Training = null;
            if (TrainingFormView.Id != 0)
            {
                Training = new TrainingBLO(this.UnitOfWork).FindBaseEntityByID(TrainingFormView.Id);
            }
            else
            {
                Training = new Training();
            } 
			Training.TrainingYearId = TrainingFormView.TrainingYearId;
			Training.ModuleTrainingId = TrainingFormView.ModuleTrainingId;
			Training.FormerId = TrainingFormView.FormerId;
			Training.GroupId = TrainingFormView.GroupId;
			Training.Code = TrainingFormView.Code;
			Training.Description = TrainingFormView.Description;
			Training.Id = TrainingFormView.Id;
            return Training;
        }
        public virtual TrainingFormView ConverTo_TrainingFormView(Training Training)
        {  
			TrainingFormView TrainingFormView = new TrainingFormView();
			TrainingFormView.toStringValue = Training.ToString();
			TrainingFormView.TrainingYearId = Training.TrainingYearId;
			TrainingFormView.ModuleTrainingId = Training.ModuleTrainingId;
			TrainingFormView.FormerId = Training.FormerId;
			TrainingFormView.GroupId = Training.GroupId;
			TrainingFormView.Code = Training.Code;
			TrainingFormView.Description = Training.Description;
			TrainingFormView.Id = Training.Id;
            return TrainingFormView;            
        }

		public virtual TrainingFormView CreateNew()
        {
            Training Training = new Training();
            TrainingFormView TrainingFormView = this.ConverTo_TrainingFormView(Training);
            return TrainingFormView;
        } 
    }

	public partial class TrainingFormViewBLM : BaseTrainingFormViewBLM
	{
		public TrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
