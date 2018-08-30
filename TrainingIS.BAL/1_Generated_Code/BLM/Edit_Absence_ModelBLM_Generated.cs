//modelType = Edit_Absence_Model

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
	public partial class BaseEdit_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseEdit_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Edit_Absence_Model Edit_Absence_Model)
        {
			Absence Absence = null;
            if (Edit_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Edit_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.TraineeId = Edit_Absence_Model.TraineeId;
			Absence.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Edit_Absence_Model.TraineeId)) ;
			Absence.isHaveAuthorization = Edit_Absence_Model.isHaveAuthorization;
			Absence.SeanceTraining = Edit_Absence_Model.SeanceTraining;
			Absence.SeancePlanning = Edit_Absence_Model.SeancePlanning;
			Absence.FormerComment = Edit_Absence_Model.FormerComment;
			Absence.TraineeComment = Edit_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Edit_Absence_Model.SupervisorComment;
			Absence.Id = Edit_Absence_Model.Id;
            return Absence;
        }
        public virtual Edit_Absence_Model ConverTo_Edit_Absence_Model(Absence Absence)
        {  
			Edit_Absence_Model Edit_Absence_Model = new Edit_Absence_Model();
			Edit_Absence_Model.toStringValue = Absence.ToString();
			Edit_Absence_Model.SeanceTraining = Absence.SeanceTraining;
			Edit_Absence_Model.TraineeId = Absence.TraineeId;
			Edit_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Edit_Absence_Model.SeancePlanning = Absence.SeancePlanning;
			Edit_Absence_Model.FormerComment = Absence.FormerComment;
			Edit_Absence_Model.TraineeComment = Absence.TraineeComment;
			Edit_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Edit_Absence_Model.Id = Absence.Id;
            return Edit_Absence_Model;            
        }

		public virtual Edit_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Edit_Absence_Model Edit_Absence_Model = this.ConverTo_Edit_Absence_Model(Absence);
            return Edit_Absence_Model;
        } 
    }

	public partial class Edit_Absence_ModelBLM : BaseEdit_Absence_ModelBLM
	{
		public Edit_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
