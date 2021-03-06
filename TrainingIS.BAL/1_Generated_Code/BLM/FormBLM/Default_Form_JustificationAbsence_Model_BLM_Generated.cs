﻿//modelType = Default_Form_JustificationAbsence_Model

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_JustificationAbsence_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_JustificationAbsence_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual JustificationAbsence ConverTo_JustificationAbsence(Default_Form_JustificationAbsence_Model Default_Form_JustificationAbsence_Model)
        {
			JustificationAbsence JustificationAbsence = null;
            if (Default_Form_JustificationAbsence_Model.Id != 0)
            {
                JustificationAbsence = new JustificationAbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_JustificationAbsence_Model.Id);
            }
            else
            {
                JustificationAbsence = new JustificationAbsence();
            } 
			JustificationAbsence.TraineeId = Default_Form_JustificationAbsence_Model.TraineeId;
			JustificationAbsence.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_JustificationAbsence_Model.TraineeId)) ;
			JustificationAbsence.Category_JustificationAbsenceId = Default_Form_JustificationAbsence_Model.Category_JustificationAbsenceId;
			JustificationAbsence.Category_JustificationAbsence = new Category_JustificationAbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_JustificationAbsence_Model.Category_JustificationAbsenceId)) ;
			JustificationAbsence.StartDate = DefaultDateTime_If_Empty(Default_Form_JustificationAbsence_Model.StartDate);
			JustificationAbsence.EndtDate = DefaultDateTime_If_Empty(Default_Form_JustificationAbsence_Model.EndtDate);
			JustificationAbsence.Description = Default_Form_JustificationAbsence_Model.Description;
			JustificationAbsence.Reference = Default_Form_JustificationAbsence_Model.Reference;
			JustificationAbsence.Id = Default_Form_JustificationAbsence_Model.Id;
            return JustificationAbsence;
        }
        public virtual void ConverTo_Default_Form_JustificationAbsence_Model(Default_Form_JustificationAbsence_Model Default_Form_JustificationAbsence_Model, JustificationAbsence JustificationAbsence)
        {  
			 
			Default_Form_JustificationAbsence_Model.toStringValue = JustificationAbsence.ToString();
			Default_Form_JustificationAbsence_Model.TraineeId = JustificationAbsence.TraineeId;
			Default_Form_JustificationAbsence_Model.Category_JustificationAbsenceId = JustificationAbsence.Category_JustificationAbsenceId;
			Default_Form_JustificationAbsence_Model.StartDate = DefaultDateTime_If_Empty(JustificationAbsence.StartDate);
			Default_Form_JustificationAbsence_Model.EndtDate = DefaultDateTime_If_Empty(JustificationAbsence.EndtDate);
			Default_Form_JustificationAbsence_Model.Description = JustificationAbsence.Description;
			Default_Form_JustificationAbsence_Model.Id = JustificationAbsence.Id;
			Default_Form_JustificationAbsence_Model.Reference = JustificationAbsence.Reference;
                     
        }

    }

	public partial class Default_Form_JustificationAbsence_ModelBLM : BaseDefault_Form_JustificationAbsence_Model_BLM
	{
		public Default_Form_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
