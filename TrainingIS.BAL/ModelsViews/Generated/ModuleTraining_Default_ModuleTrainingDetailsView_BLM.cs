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
	public partial class BaseDefault_ModuleTrainingDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ModuleTrainingDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ModuleTraining ConverTo_ModuleTraining(Default_ModuleTrainingDetailsView Default_ModuleTrainingDetailsView)
        {
			ModuleTraining ModuleTraining = null;
            if (Default_ModuleTrainingDetailsView.Id != 0)
            {
                ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork).FindBaseEntityByID(Default_ModuleTrainingDetailsView.Id);
            }
            else
            {
                ModuleTraining = new ModuleTraining();
            } 
			ModuleTraining.Specialty = Default_ModuleTrainingDetailsView.Specialty;
			ModuleTraining.Name = Default_ModuleTrainingDetailsView.Name;
			ModuleTraining.Code = Default_ModuleTrainingDetailsView.Code;
			ModuleTraining.Description = Default_ModuleTrainingDetailsView.Description;
			ModuleTraining.Id = Default_ModuleTrainingDetailsView.Id;
            return ModuleTraining;
        }
        public virtual Default_ModuleTrainingDetailsView ConverTo_Default_ModuleTrainingDetailsView(ModuleTraining ModuleTraining)
        {  
			Default_ModuleTrainingDetailsView Default_ModuleTrainingDetailsView = new Default_ModuleTrainingDetailsView();
			Default_ModuleTrainingDetailsView.toStringValue = ModuleTraining.ToString();
			Default_ModuleTrainingDetailsView.Specialty = ModuleTraining.Specialty;
			Default_ModuleTrainingDetailsView.Name = ModuleTraining.Name;
			Default_ModuleTrainingDetailsView.Code = ModuleTraining.Code;
			Default_ModuleTrainingDetailsView.Description = ModuleTraining.Description;
			Default_ModuleTrainingDetailsView.Id = ModuleTraining.Id;
            return Default_ModuleTrainingDetailsView;            
        }
    }

	public partial class Default_ModuleTrainingDetailsViewBLM : BaseDefault_ModuleTrainingDetailsViewBLM
	{
		public Default_ModuleTrainingDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
