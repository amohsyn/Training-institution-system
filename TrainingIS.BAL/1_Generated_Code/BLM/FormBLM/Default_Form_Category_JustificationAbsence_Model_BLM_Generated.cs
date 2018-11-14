//modelType = Default_Form_Category_JustificationAbsence_Model

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
	public partial class BaseDefault_Form_Category_JustificationAbsence_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Category_JustificationAbsence_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Category_JustificationAbsence ConverTo_Category_JustificationAbsence(Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model)
        {
			Category_JustificationAbsence Category_JustificationAbsence = null;
            if (Default_Form_Category_JustificationAbsence_Model.Id != 0)
            {
                Category_JustificationAbsence = new Category_JustificationAbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Category_JustificationAbsence_Model.Id);
            }
            else
            {
                Category_JustificationAbsence = new Category_JustificationAbsence();
            } 
			Category_JustificationAbsence.Name = Default_Form_Category_JustificationAbsence_Model.Name;
			Category_JustificationAbsence.Description = Default_Form_Category_JustificationAbsence_Model.Description;
			Category_JustificationAbsence.Reference = Default_Form_Category_JustificationAbsence_Model.Reference;
			Category_JustificationAbsence.Id = Default_Form_Category_JustificationAbsence_Model.Id;
            return Category_JustificationAbsence;
        }
        public virtual void ConverTo_Default_Form_Category_JustificationAbsence_Model(Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model, Category_JustificationAbsence Category_JustificationAbsence)
        {  
			 
			Default_Form_Category_JustificationAbsence_Model.toStringValue = Category_JustificationAbsence.ToString();
			Default_Form_Category_JustificationAbsence_Model.Name = Category_JustificationAbsence.Name;
			Default_Form_Category_JustificationAbsence_Model.Description = Category_JustificationAbsence.Description;
			Default_Form_Category_JustificationAbsence_Model.Id = Category_JustificationAbsence.Id;
			Default_Form_Category_JustificationAbsence_Model.Reference = Category_JustificationAbsence.Reference;
                     
        }

    }

	public partial class Default_Form_Category_JustificationAbsence_ModelBLM : BaseDefault_Form_Category_JustificationAbsence_Model_BLM
	{
		public Default_Form_Category_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
