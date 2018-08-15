﻿using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_ModuleTraining_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Form_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ModuleTraining ConverTo_ModuleTraining(Default_Form_ModuleTraining_Model Default_Form_ModuleTraining_Model)
        {
			ModuleTraining ModuleTraining = null;
            if (Default_Form_ModuleTraining_Model.Id != 0)
            {
                ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork).FindBaseEntityByID(Default_Form_ModuleTraining_Model.Id);
            }
            else
            {
                ModuleTraining = new ModuleTraining();
            } 
			ModuleTraining.Specialty = Default_Form_ModuleTraining_Model.Specialty;
			ModuleTraining.Name = Default_Form_ModuleTraining_Model.Name;
			ModuleTraining.Code = Default_Form_ModuleTraining_Model.Code;
			ModuleTraining.Description = Default_Form_ModuleTraining_Model.Description;
			ModuleTraining.Id = Default_Form_ModuleTraining_Model.Id;
            return ModuleTraining;
        }
        public virtual Default_Form_ModuleTraining_Model ConverTo_Default_Form_ModuleTraining_Model(ModuleTraining ModuleTraining)
        {  
			Default_Form_ModuleTraining_Model Default_Form_ModuleTraining_Model = new Default_Form_ModuleTraining_Model();
			Default_Form_ModuleTraining_Model.toStringValue = ModuleTraining.ToString();
			Default_Form_ModuleTraining_Model.Specialty = ModuleTraining.Specialty;
			Default_Form_ModuleTraining_Model.Name = ModuleTraining.Name;
			Default_Form_ModuleTraining_Model.Code = ModuleTraining.Code;
			Default_Form_ModuleTraining_Model.Description = ModuleTraining.Description;
			Default_Form_ModuleTraining_Model.Id = ModuleTraining.Id;
            return Default_Form_ModuleTraining_Model;            
        }

		public virtual Default_Form_ModuleTraining_Model CreateNew()
        {
            ModuleTraining ModuleTraining = new ModuleTraining();
            Default_Form_ModuleTraining_Model Default_Form_ModuleTraining_Model = this.ConverTo_Default_Form_ModuleTraining_Model(ModuleTraining);
            return Default_Form_ModuleTraining_Model;
        } 
    }

	public partial class Default_Form_ModuleTraining_ModelBLM : BaseDefault_Form_ModuleTraining_ModelBLM
	{
		public Default_Form_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}