//modelType = Form_Absence_Model

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
	public partial class BaseForm_Absence_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseForm_Absence_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Form_Absence_Model Form_Absence_Model)
        {
			Absence Absence = null;
            if (Form_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Form_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.Sanction = Form_Absence_Model.Sanction;
			Absence.Trainee = Form_Absence_Model.Trainee;
			Absence.TraineeId = Form_Absence_Model.TraineeId;
			Absence.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Absence_Model.TraineeId)) ;
			Absence.AbsenceState = Form_Absence_Model.AbsenceState;
			Absence.SeanceTraining = Form_Absence_Model.SeanceTraining;
			Absence.SeanceTrainingId = Form_Absence_Model.SeanceTrainingId;
			Absence.SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Absence_Model.SeanceTrainingId)) ;
			Absence.FormerComment = Form_Absence_Model.FormerComment;
			Absence.TraineeComment = Form_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Form_Absence_Model.SupervisorComment;
			Absence.JustificationAbsence = Form_Absence_Model.JustificationAbsence;
			Absence.Id = Form_Absence_Model.Id;
            return Absence;
        }
        public virtual void ConverTo_Form_Absence_Model(Form_Absence_Model Form_Absence_Model, Absence Absence)
        {  
			 
			Form_Absence_Model.toStringValue = Absence.ToString();
			Form_Absence_Model.SeanceTraining = Absence.SeanceTraining;
			Form_Absence_Model.SeanceTrainingId = Absence.SeanceTrainingId;
			Form_Absence_Model.Trainee = Absence.Trainee;
			Form_Absence_Model.TraineeId = Absence.TraineeId;
			Form_Absence_Model.FormerComment = Absence.FormerComment;
			Form_Absence_Model.TraineeComment = Absence.TraineeComment;
			Form_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Form_Absence_Model.AbsenceState = Absence.AbsenceState;
			Form_Absence_Model.JustificationAbsence = Absence.JustificationAbsence;
			Form_Absence_Model.Sanction = Absence.Sanction;
			Form_Absence_Model.Id = Absence.Id;
                     
        }

    }

	public partial class Form_Absence_ModelBLM : BaseForm_Absence_Model_BLM
	{
		public Form_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
