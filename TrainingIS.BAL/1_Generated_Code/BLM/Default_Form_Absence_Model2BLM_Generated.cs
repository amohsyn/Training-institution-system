//modelType = Default_Form_Absence_Model2

using System;
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
	public partial class BaseDefault_Form_Absence_Model2BLM : BaseModelBLM
    {
        
        public BaseDefault_Form_Absence_Model2BLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Absence ConverTo_Absence(Default_Form_Absence_Model2 Default_Form_Absence_Model2)
        {
			Absence Absence = null;
            if (Default_Form_Absence_Model2.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork).FindBaseEntityByID(Default_Form_Absence_Model2.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.TraineeId = Default_Form_Absence_Model2.TraineeId;
			Absence.isHaveAuthorization = Default_Form_Absence_Model2.isHaveAuthorization;
			Absence.SeanceTrainingId = Default_Form_Absence_Model2.SeanceTrainingId;
			Absence.FormerComment = Default_Form_Absence_Model2.FormerComment;
			Absence.TraineeComment = Default_Form_Absence_Model2.TraineeComment;
			Absence.SupervisorComment = Default_Form_Absence_Model2.SupervisorComment;
			Absence.Id = Default_Form_Absence_Model2.Id;
            return Absence;
        }
        public virtual Default_Form_Absence_Model2 ConverTo_Default_Form_Absence_Model2(Absence Absence)
        {  
			Default_Form_Absence_Model2 Default_Form_Absence_Model2 = new Default_Form_Absence_Model2();
			Default_Form_Absence_Model2.toStringValue = Absence.ToString();
			Default_Form_Absence_Model2.TraineeId = Absence.TraineeId;
			Default_Form_Absence_Model2.isHaveAuthorization = Absence.isHaveAuthorization;
			Default_Form_Absence_Model2.SeanceTrainingId = Absence.SeanceTrainingId;
			Default_Form_Absence_Model2.FormerComment = Absence.FormerComment;
			Default_Form_Absence_Model2.TraineeComment = Absence.TraineeComment;
			Default_Form_Absence_Model2.SupervisorComment = Absence.SupervisorComment;
			Default_Form_Absence_Model2.Id = Absence.Id;
            return Default_Form_Absence_Model2;            
        }

		public virtual Default_Form_Absence_Model2 CreateNew()
        {
            Absence Absence = new Absence();
            Default_Form_Absence_Model2 Default_Form_Absence_Model2 = this.ConverTo_Default_Form_Absence_Model2(Absence);
            return Default_Form_Absence_Model2;
        } 
    }

	public partial class Default_Form_Absence_Model2BLM : BaseDefault_Form_Absence_Model2BLM
	{
		public Default_Form_Absence_Model2BLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
