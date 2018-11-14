//modelType = Default_Form_StateOfAbsece_Model

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
	public partial class BaseDefault_Form_StateOfAbsece_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_StateOfAbsece_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual StateOfAbsece ConverTo_StateOfAbsece(Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model)
        {
			StateOfAbsece StateOfAbsece = null;
            if (Default_Form_StateOfAbsece_Model.Id != 0)
            {
                StateOfAbsece = new StateOfAbseceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_StateOfAbsece_Model.Id);
            }
            else
            {
                StateOfAbsece = new StateOfAbsece();
            } 
			StateOfAbsece.Name = Default_Form_StateOfAbsece_Model.Name;
			StateOfAbsece.Category = Default_Form_StateOfAbsece_Model.Category;
			StateOfAbsece.Value = Default_Form_StateOfAbsece_Model.Value;
			StateOfAbsece.TraineeId = Default_Form_StateOfAbsece_Model.TraineeId;
			StateOfAbsece.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_StateOfAbsece_Model.TraineeId)) ;
			StateOfAbsece.Reference = Default_Form_StateOfAbsece_Model.Reference;
			StateOfAbsece.Id = Default_Form_StateOfAbsece_Model.Id;
            return StateOfAbsece;
        }
        public virtual void ConverTo_Default_Form_StateOfAbsece_Model(Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model, StateOfAbsece StateOfAbsece)
        {  
			 
			Default_Form_StateOfAbsece_Model.toStringValue = StateOfAbsece.ToString();
			Default_Form_StateOfAbsece_Model.Name = StateOfAbsece.Name;
			Default_Form_StateOfAbsece_Model.Category = StateOfAbsece.Category;
			Default_Form_StateOfAbsece_Model.Value = StateOfAbsece.Value;
			Default_Form_StateOfAbsece_Model.TraineeId = StateOfAbsece.TraineeId;
			Default_Form_StateOfAbsece_Model.Id = StateOfAbsece.Id;
			Default_Form_StateOfAbsece_Model.Reference = StateOfAbsece.Reference;
                     
        }

    }

	public partial class Default_Form_StateOfAbsece_ModelBLM : BaseDefault_Form_StateOfAbsece_Model_BLM
	{
		public Default_Form_StateOfAbsece_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
