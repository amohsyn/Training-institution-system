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
	public partial class BaseDefault_Form_Absence_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Form_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Absence ConverTo_Absence(Default_Form_Absence_Model Default_Form_Absence_Model)
        {
			Absence Absence = null;
            if (Default_Form_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork).FindBaseEntityByID(Default_Form_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.Trainee = Default_Form_Absence_Model.Trainee;
			Absence.isHaveAuthorization = Default_Form_Absence_Model.isHaveAuthorization;
			Absence.SeanceTraining = Default_Form_Absence_Model.SeanceTraining;
			Absence.FormerComment = Default_Form_Absence_Model.FormerComment;
			Absence.TraineeComment = Default_Form_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Default_Form_Absence_Model.SupervisorComment;
			Absence.Id = Default_Form_Absence_Model.Id;
            return Absence;
        }
        public virtual Default_Form_Absence_Model ConverTo_Default_Form_Absence_Model(Absence Absence)
        {  
			Default_Form_Absence_Model Default_Form_Absence_Model = new Default_Form_Absence_Model();
			Default_Form_Absence_Model.toStringValue = Absence.ToString();
			Default_Form_Absence_Model.Trainee = Absence.Trainee;
			Default_Form_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Default_Form_Absence_Model.SeanceTraining = Absence.SeanceTraining;
			Default_Form_Absence_Model.FormerComment = Absence.FormerComment;
			Default_Form_Absence_Model.TraineeComment = Absence.TraineeComment;
			Default_Form_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Default_Form_Absence_Model.Id = Absence.Id;
            return Default_Form_Absence_Model;            
        }

		public virtual Default_Form_Absence_Model CreateNew()
        {
            Absence Absence = new Absence();
            Default_Form_Absence_Model Default_Form_Absence_Model = this.ConverTo_Default_Form_Absence_Model(Absence);
            return Default_Form_Absence_Model;
        } 
    }

	public partial class Default_Form_Absence_ModelBLM : BaseDefault_Form_Absence_ModelBLM
	{
		public Default_Form_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
