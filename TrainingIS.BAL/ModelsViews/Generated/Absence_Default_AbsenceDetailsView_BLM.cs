using System;
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
	public partial class BaseDefault_AbsenceDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AbsenceDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Absence ConverTo_Absence(Default_AbsenceDetailsView Default_AbsenceDetailsView)
        {
			Absence Absence = null;
            if (Default_AbsenceDetailsView.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork).FindBaseEntityByID(Default_AbsenceDetailsView.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.Trainee = Default_AbsenceDetailsView.Trainee;
			Absence.isHaveAuthorization = Default_AbsenceDetailsView.isHaveAuthorization;
			Absence.SeanceTraining = Default_AbsenceDetailsView.SeanceTraining;
			Absence.FormerComment = Default_AbsenceDetailsView.FormerComment;
			Absence.TraineeComment = Default_AbsenceDetailsView.TraineeComment;
			Absence.SupervisorComment = Default_AbsenceDetailsView.SupervisorComment;
			Absence.Id = Default_AbsenceDetailsView.Id;
            return Absence;
        }
        public virtual Default_AbsenceDetailsView ConverTo_Default_AbsenceDetailsView(Absence Absence)
        {  
			Default_AbsenceDetailsView Default_AbsenceDetailsView = new Default_AbsenceDetailsView();
			Default_AbsenceDetailsView.toStringValue = Absence.ToString();
			Default_AbsenceDetailsView.Trainee = Absence.Trainee;
			Default_AbsenceDetailsView.isHaveAuthorization = Absence.isHaveAuthorization;
			Default_AbsenceDetailsView.SeanceTraining = Absence.SeanceTraining;
			Default_AbsenceDetailsView.FormerComment = Absence.FormerComment;
			Default_AbsenceDetailsView.TraineeComment = Absence.TraineeComment;
			Default_AbsenceDetailsView.SupervisorComment = Absence.SupervisorComment;
			Default_AbsenceDetailsView.Id = Absence.Id;
            return Default_AbsenceDetailsView;            
        }
    }

	public partial class Default_AbsenceDetailsViewBLM : BaseDefault_AbsenceDetailsViewBLM
	{
		public Default_AbsenceDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
