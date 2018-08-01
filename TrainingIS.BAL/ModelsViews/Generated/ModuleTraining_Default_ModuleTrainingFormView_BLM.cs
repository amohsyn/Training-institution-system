﻿using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_ModuleTrainingFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ModuleTrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ModuleTraining ConverTo_ModuleTraining(Default_ModuleTrainingFormView Default_ModuleTrainingFormView)
        {
			ModuleTraining ModuleTraining = new ModuleTraining();
			ModuleTraining.SpecialtyId = Default_ModuleTrainingFormView.SpecialtyId;
			ModuleTraining.Name = Default_ModuleTrainingFormView.Name;
			ModuleTraining.Code = Default_ModuleTrainingFormView.Code;
			ModuleTraining.Description = Default_ModuleTrainingFormView.Description;
			ModuleTraining.Id = Default_ModuleTrainingFormView.Id;
            return ModuleTraining;

        }
        public virtual Default_ModuleTrainingFormView ConverTo_Default_ModuleTrainingFormView(ModuleTraining ModuleTraining)
        {
            Default_ModuleTrainingFormView Default_ModuleTrainingFormView = new Default_ModuleTrainingFormView();
			Default_ModuleTrainingFormView.SpecialtyId = ModuleTraining.SpecialtyId;
			Default_ModuleTrainingFormView.Name = ModuleTraining.Name;
			Default_ModuleTrainingFormView.Code = ModuleTraining.Code;
			Default_ModuleTrainingFormView.Description = ModuleTraining.Description;
			Default_ModuleTrainingFormView.Id = ModuleTraining.Id;
            return Default_ModuleTrainingFormView;            
        }
    }

	public partial class Default_ModuleTrainingFormViewBLM : BaseDefault_ModuleTrainingFormViewBLM
	{
		public Default_ModuleTrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
