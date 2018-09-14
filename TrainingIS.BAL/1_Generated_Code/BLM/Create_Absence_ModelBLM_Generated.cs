//modelType = Create_Absence_Model

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
using TrainingIS.Models.Absences;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseCreate_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseCreate_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Create_Absence_Model Create_Absence_Model)
        {
			Absence Absence = null;
            if (Create_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Create_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.TraineeId = Create_Absence_Model.TraineeId;
			Absence.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Create_Absence_Model.TraineeId)) ;
			Absence.isHaveAuthorization = Create_Absence_Model.isHaveAuthorization;
			Absence.SeanceTrainingId = Create_Absence_Model.SeanceTrainingId;
			Absence.SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Create_Absence_Model.SeanceTrainingId)) ;
			Absence.FormerComment = Create_Absence_Model.FormerComment;
			Absence.TraineeComment = Create_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Create_Absence_Model.SupervisorComment;
			Absence.Id = Create_Absence_Model.Id;
            return Absence;
        }
        public virtual Create_Absence_Model ConverTo_Create_Absence_Model(Absence Absence)
        {  
			Create_Absence_Model Create_Absence_Model = new Create_Absence_Model();
			Create_Absence_Model.toStringValue = Absence.ToString();
			Create_Absence_Model.SeanceTrainingId = Absence.SeanceTrainingId;
			Create_Absence_Model.TraineeId = Absence.TraineeId;
			Create_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Create_Absence_Model.FormerComment = Absence.FormerComment;
			Create_Absence_Model.TraineeComment = Absence.TraineeComment;
			Create_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Create_Absence_Model.Id = Absence.Id;
            return Create_Absence_Model;            
        }

		public virtual Create_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Create_Absence_Model Create_Absence_Model = this.ConverTo_Create_Absence_Model(Absence);
            return Create_Absence_Model;
        } 
    }

	public partial class Create_Absence_ModelBLM : BaseCreate_Absence_ModelBLM
	{
		public Create_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
