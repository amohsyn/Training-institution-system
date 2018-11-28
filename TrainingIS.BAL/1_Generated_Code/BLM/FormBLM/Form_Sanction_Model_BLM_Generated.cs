//modelType = Form_Sanction_Model

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
	public partial class BaseForm_Sanction_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseForm_Sanction_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Sanction ConverTo_Sanction(Form_Sanction_Model Form_Sanction_Model)
        {
			Sanction Sanction = null;
            if (Form_Sanction_Model.Id != 0)
            {
                Sanction = new SanctionBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Form_Sanction_Model.Id);
            }
            else
            {
                Sanction = new Sanction();
            } 
			Sanction.TraineeId = Form_Sanction_Model.TraineeId;
			Sanction.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Sanction_Model.TraineeId)) ;
			Sanction.Trainee = Form_Sanction_Model.Trainee;
			Sanction.SanctionCategoryId = Form_Sanction_Model.SanctionCategoryId;
			Sanction.SanctionCategory = new SanctionCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Sanction_Model.SanctionCategoryId)) ;
			Sanction.SanctionCategory = Form_Sanction_Model.SanctionCategory;
			Sanction.MeetingId = Form_Sanction_Model.MeetingId;
			Sanction.Meeting = new MeetingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Sanction_Model.MeetingId)) ;
			Sanction.Reference = Form_Sanction_Model.Reference;
			Sanction.Id = Form_Sanction_Model.Id;
            return Sanction;
        }
        public virtual void ConverTo_Form_Sanction_Model(Form_Sanction_Model Form_Sanction_Model, Sanction Sanction)
        {  
			 
			Form_Sanction_Model.toStringValue = Sanction.ToString();
			Form_Sanction_Model.Trainee = Sanction.Trainee;
			Form_Sanction_Model.TraineeId = Sanction.TraineeId;
			Form_Sanction_Model.SanctionCategory = Sanction.SanctionCategory;
			Form_Sanction_Model.SanctionCategoryId = Sanction.SanctionCategoryId;
			Form_Sanction_Model.MeetingId = Sanction.MeetingId;
			Form_Sanction_Model.Id = Sanction.Id;
			Form_Sanction_Model.Reference = Sanction.Reference;
                     
        }

    }

	public partial class Form_Sanction_ModelBLM : BaseForm_Sanction_Model_BLM
	{
		public Form_Sanction_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
