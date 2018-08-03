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
	public partial class BaseDefault_AbsenceFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AbsenceFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Absence ConverTo_Absence(Default_AbsenceFormView Default_AbsenceFormView)
        {
			Absence Absence = null;
            if (Default_AbsenceFormView.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork).FindBaseEntityByID(Default_AbsenceFormView.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.TraineeId = Default_AbsenceFormView.TraineeId;
			Absence.isHaveAuthorization = Default_AbsenceFormView.isHaveAuthorization;
			Absence.SeanceTrainingId = Default_AbsenceFormView.SeanceTrainingId;
			Absence.FormerComment = Default_AbsenceFormView.FormerComment;
			Absence.TraineeComment = Default_AbsenceFormView.TraineeComment;
			Absence.SupervisorComment = Default_AbsenceFormView.SupervisorComment;
			Absence.Id = Default_AbsenceFormView.Id;
            return Absence;
        }
        public virtual Default_AbsenceFormView ConverTo_Default_AbsenceFormView(Absence Absence)
        {  
            Default_AbsenceFormView Default_AbsenceFormView = new Default_AbsenceFormView();
			Default_AbsenceFormView.TraineeId = Absence.TraineeId;
			Default_AbsenceFormView.isHaveAuthorization = Absence.isHaveAuthorization;
			Default_AbsenceFormView.SeanceTrainingId = Absence.SeanceTrainingId;
			Default_AbsenceFormView.FormerComment = Absence.FormerComment;
			Default_AbsenceFormView.TraineeComment = Absence.TraineeComment;
			Default_AbsenceFormView.SupervisorComment = Absence.SupervisorComment;
			Default_AbsenceFormView.Id = Absence.Id;
            return Default_AbsenceFormView;            
        }
    }

	public partial class Default_AbsenceFormViewBLM : BaseDefault_AbsenceFormViewBLM
	{
		public Default_AbsenceFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
