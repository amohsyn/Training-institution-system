﻿using System;
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
	public partial class BaseDefault_TrainingFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Training ConverTo_Training(Default_TrainingFormView Default_TrainingFormView)
        {
			Training Training = null;
            if (Default_TrainingFormView.Id != 0)
            {
                Training = new TrainingBLO(this.UnitOfWork).FindBaseEntityByID(Default_TrainingFormView.Id);
            }
            else
            {
                Training = new Training();
            } 
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
			Default_TrainingFormView.toStringValue = Training.ToString();
			Default_TrainingFormView.TrainingYearId = Training.TrainingYearId;
			Default_TrainingFormView.ModuleTrainingId = Training.ModuleTrainingId;
			Default_TrainingFormView.FormerId = Training.FormerId;
			Default_TrainingFormView.GroupId = Training.GroupId;
			Default_TrainingFormView.Code = Training.Code;
			Default_TrainingFormView.Description = Training.Description;
			Default_TrainingFormView.Id = Training.Id;
            return Default_TrainingFormView;            
        }

		public virtual Default_TrainingFormView CreateNew()
        {
            Training Training = new Training();
            Default_TrainingFormView Default_TrainingFormView = this.ConverTo_Default_TrainingFormView(Training);
            return Default_TrainingFormView;
        } 
    }

	public partial class Default_TrainingFormViewBLM : BaseDefault_TrainingFormViewBLM
	{
		public Default_TrainingFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
