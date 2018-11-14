//modelType = Default_Form_Sanction_Model

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
	public partial class BaseDefault_Form_Sanction_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Sanction_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Sanction ConverTo_Sanction(Default_Form_Sanction_Model Default_Form_Sanction_Model)
        {
			Sanction Sanction = null;
            if (Default_Form_Sanction_Model.Id != 0)
            {
                Sanction = new SanctionBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Sanction_Model.Id);
            }
            else
            {
                Sanction = new Sanction();
            } 
			Sanction.TraineeId = Default_Form_Sanction_Model.TraineeId;
			Sanction.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Sanction_Model.TraineeId)) ;
			Sanction.SanctionCategoryId = Default_Form_Sanction_Model.SanctionCategoryId;
			Sanction.SanctionCategory = new SanctionCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Sanction_Model.SanctionCategoryId)) ;
			Sanction.SanctionState = Default_Form_Sanction_Model.SanctionState;
			Sanction.MeetingId = Default_Form_Sanction_Model.MeetingId;
			Sanction.Meeting = new MeetingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Sanction_Model.MeetingId)) ;
			Sanction.Reference = Default_Form_Sanction_Model.Reference;
			Sanction.Id = Default_Form_Sanction_Model.Id;
            return Sanction;
        }
        public virtual void ConverTo_Default_Form_Sanction_Model(Default_Form_Sanction_Model Default_Form_Sanction_Model, Sanction Sanction)
        {  
			 
			Default_Form_Sanction_Model.toStringValue = Sanction.ToString();
			Default_Form_Sanction_Model.TraineeId = Sanction.TraineeId;
			Default_Form_Sanction_Model.SanctionCategoryId = Sanction.SanctionCategoryId;
			Default_Form_Sanction_Model.SanctionState = Sanction.SanctionState;
			Default_Form_Sanction_Model.MeetingId = Sanction.MeetingId;
			Default_Form_Sanction_Model.Id = Sanction.Id;
			Default_Form_Sanction_Model.Reference = Sanction.Reference;
                     
        }

    }

	public partial class Default_Form_Sanction_ModelBLM : BaseDefault_Form_Sanction_Model_BLM
	{
		public Default_Form_Sanction_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
