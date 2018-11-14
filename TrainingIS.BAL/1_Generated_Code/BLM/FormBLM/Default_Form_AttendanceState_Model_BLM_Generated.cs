//modelType = Default_Form_AttendanceState_Model

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
	public partial class BaseDefault_Form_AttendanceState_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_AttendanceState_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual AttendanceState ConverTo_AttendanceState(Default_Form_AttendanceState_Model Default_Form_AttendanceState_Model)
        {
			AttendanceState AttendanceState = null;
            if (Default_Form_AttendanceState_Model.Id != 0)
            {
                AttendanceState = new AttendanceStateBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_AttendanceState_Model.Id);
            }
            else
            {
                AttendanceState = new AttendanceState();
            } 
			AttendanceState.TraineeId = Default_Form_AttendanceState_Model.TraineeId;
			AttendanceState.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_AttendanceState_Model.TraineeId)) ;
			AttendanceState.Valid_Note = Default_Form_AttendanceState_Model.Valid_Note;
			AttendanceState.Invalid_Note = Default_Form_AttendanceState_Model.Invalid_Note;
			AttendanceState.Reference = Default_Form_AttendanceState_Model.Reference;
			AttendanceState.Id = Default_Form_AttendanceState_Model.Id;
            return AttendanceState;
        }
        public virtual void ConverTo_Default_Form_AttendanceState_Model(Default_Form_AttendanceState_Model Default_Form_AttendanceState_Model, AttendanceState AttendanceState)
        {  
			 
			Default_Form_AttendanceState_Model.toStringValue = AttendanceState.ToString();
			Default_Form_AttendanceState_Model.TraineeId = AttendanceState.TraineeId;
			Default_Form_AttendanceState_Model.Valid_Note = AttendanceState.Valid_Note;
			Default_Form_AttendanceState_Model.Invalid_Note = AttendanceState.Invalid_Note;
			Default_Form_AttendanceState_Model.Id = AttendanceState.Id;
			Default_Form_AttendanceState_Model.Reference = AttendanceState.Reference;
                     
        }

    }

	public partial class Default_Form_AttendanceState_ModelBLM : BaseDefault_Form_AttendanceState_Model_BLM
	{
		public Default_Form_AttendanceState_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
