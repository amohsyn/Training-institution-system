//modelType = Default_Form_Absence_Model

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

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Default_Form_Absence_Model Default_Form_Absence_Model)
        {
			Absence Absence = null;
            if (Default_Form_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.AbsenceDate = DefaultDateTime_If_Empty(Default_Form_Absence_Model.AbsenceDate);
			Absence.SeanceTrainingId = Default_Form_Absence_Model.SeanceTrainingId;
			Absence.SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Absence_Model.SeanceTrainingId)) ;
			Absence.TraineeId = Default_Form_Absence_Model.TraineeId;
			Absence.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Absence_Model.TraineeId)) ;
			Absence.isHaveAuthorization = Default_Form_Absence_Model.isHaveAuthorization;
			Absence.SeancePlanningId = Default_Form_Absence_Model.SeancePlanningId;
			Absence.SeancePlanning = new SeancePlanningBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Absence_Model.SeancePlanningId)) ;
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
			Default_Form_Absence_Model.AbsenceDate = DefaultDateTime_If_Empty(Absence.AbsenceDate);
			Default_Form_Absence_Model.SeanceTrainingId = ConversionUtil.DefaultValue_if_Null<Int64>(Absence.SeanceTrainingId);
			Default_Form_Absence_Model.TraineeId = Absence.TraineeId;
			Default_Form_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Default_Form_Absence_Model.SeancePlanningId = Absence.SeancePlanningId;
			Default_Form_Absence_Model.FormerComment = Absence.FormerComment;
			Default_Form_Absence_Model.TraineeComment = Absence.TraineeComment;
			Default_Form_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Default_Form_Absence_Model.Id = Absence.Id;
            return Default_Form_Absence_Model;            
        }

		public virtual Default_Form_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Absence_Model Default_Form_Absence_Model = this.ConverTo_Default_Form_Absence_Model(Absence);
            return Default_Form_Absence_Model;
        } 
    }

	public partial class Default_Form_Absence_ModelBLM : BaseDefault_Form_Absence_ModelBLM
	{
		public Default_Form_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
