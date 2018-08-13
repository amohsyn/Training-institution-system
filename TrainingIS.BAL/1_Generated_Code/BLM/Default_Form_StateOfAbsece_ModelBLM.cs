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
	public partial class BaseDefault_Form_StateOfAbsece_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Form_StateOfAbsece_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual StateOfAbsece ConverTo_StateOfAbsece(Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model)
        {
			StateOfAbsece StateOfAbsece = null;
            if (Default_Form_StateOfAbsece_Model.Id != 0)
            {
                StateOfAbsece = new StateOfAbseceBLO(this.UnitOfWork).FindBaseEntityByID(Default_Form_StateOfAbsece_Model.Id);
            }
            else
            {
                StateOfAbsece = new StateOfAbsece();
            } 
			StateOfAbsece.Name = Default_Form_StateOfAbsece_Model.Name;
			StateOfAbsece.Category = Default_Form_StateOfAbsece_Model.Category;
			StateOfAbsece.Value = Default_Form_StateOfAbsece_Model.Value;
			StateOfAbsece.Trainee = Default_Form_StateOfAbsece_Model.Trainee;
			StateOfAbsece.Id = Default_Form_StateOfAbsece_Model.Id;
            return StateOfAbsece;
        }
        public virtual Default_Form_StateOfAbsece_Model ConverTo_Default_Form_StateOfAbsece_Model(StateOfAbsece StateOfAbsece)
        {  
			Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model = new Default_Form_StateOfAbsece_Model();
			Default_Form_StateOfAbsece_Model.toStringValue = StateOfAbsece.ToString();
			Default_Form_StateOfAbsece_Model.Name = StateOfAbsece.Name;
			Default_Form_StateOfAbsece_Model.Category = StateOfAbsece.Category;
			Default_Form_StateOfAbsece_Model.Value = StateOfAbsece.Value;
			Default_Form_StateOfAbsece_Model.Trainee = StateOfAbsece.Trainee;
			Default_Form_StateOfAbsece_Model.Id = StateOfAbsece.Id;
            return Default_Form_StateOfAbsece_Model;            
        }

		public virtual Default_Form_StateOfAbsece_Model CreateNew()
        {
            StateOfAbsece StateOfAbsece = new StateOfAbsece();
            Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model = this.ConverTo_Default_Form_StateOfAbsece_Model(StateOfAbsece);
            return Default_Form_StateOfAbsece_Model;
        } 
    }

	public partial class Default_Form_StateOfAbsece_ModelBLM : BaseDefault_Form_StateOfAbsece_ModelBLM
	{
		public Default_Form_StateOfAbsece_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
