using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_AbsenceDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AbsenceDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Absence ConverTo_Absence(Default_AbsenceDetailsView Default_AbsenceDetailsView)
        {
			Absence Absence = new Absence();
			Absence.TraineeId = Default_AbsenceDetailsView.TraineeId;
			Absence.isHaveAuthorization = Default_AbsenceDetailsView.isHaveAuthorization;
			Absence.SeanceTrainingId = Default_AbsenceDetailsView.SeanceTrainingId;
			Absence.FormerComment = Default_AbsenceDetailsView.FormerComment;
			Absence.TraineeComment = Default_AbsenceDetailsView.TraineeComment;
			Absence.SupervisorComment = Default_AbsenceDetailsView.SupervisorComment;
			Absence.Id = Default_AbsenceDetailsView.Id;
            return Absence;

        }
        public virtual Default_AbsenceDetailsView ConverTo_Default_AbsenceDetailsView(Absence Absence)
        {
            Default_AbsenceDetailsView Default_AbsenceDetailsView = new Default_AbsenceDetailsView();
			Default_AbsenceDetailsView.TraineeId = Absence.TraineeId;
			Default_AbsenceDetailsView.isHaveAuthorization = Absence.isHaveAuthorization;
			Default_AbsenceDetailsView.SeanceTrainingId = Absence.SeanceTrainingId;
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
