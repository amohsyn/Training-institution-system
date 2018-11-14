//modelType = Default_Form_SanctionCategory_Model

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
	public partial class BaseDefault_Form_SanctionCategory_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_SanctionCategory_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SanctionCategory ConverTo_SanctionCategory(Default_Form_SanctionCategory_Model Default_Form_SanctionCategory_Model)
        {
			SanctionCategory SanctionCategory = null;
            if (Default_Form_SanctionCategory_Model.Id != 0)
            {
                SanctionCategory = new SanctionCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_SanctionCategory_Model.Id);
            }
            else
            {
                SanctionCategory = new SanctionCategory();
            } 
			SanctionCategory.DisciplineCategoryId = Default_Form_SanctionCategory_Model.DisciplineCategoryId;
			SanctionCategory.DisciplineCategory = new DisciplineCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_SanctionCategory_Model.DisciplineCategoryId)) ;
			SanctionCategory.Name = Default_Form_SanctionCategory_Model.Name;
			SanctionCategory.Code = Default_Form_SanctionCategory_Model.Code;
			SanctionCategory.DecisionAuthority = Default_Form_SanctionCategory_Model.DecisionAuthority;
			SanctionCategory.WorkflowOrder = Default_Form_SanctionCategory_Model.WorkflowOrder;
			SanctionCategory.Number_Of_Days_Of_Exclusion = Default_Form_SanctionCategory_Model.Number_Of_Days_Of_Exclusion;
			SanctionCategory.Plurality_Of_Absences = Default_Form_SanctionCategory_Model.Plurality_Of_Absences;
			SanctionCategory.Deducted_Points = Default_Form_SanctionCategory_Model.Deducted_Points;
			SanctionCategory.Description = Default_Form_SanctionCategory_Model.Description;
			SanctionCategory.Reference = Default_Form_SanctionCategory_Model.Reference;
			SanctionCategory.Id = Default_Form_SanctionCategory_Model.Id;
            return SanctionCategory;
        }
        public virtual void ConverTo_Default_Form_SanctionCategory_Model(Default_Form_SanctionCategory_Model Default_Form_SanctionCategory_Model, SanctionCategory SanctionCategory)
        {  
			 
			Default_Form_SanctionCategory_Model.toStringValue = SanctionCategory.ToString();
			Default_Form_SanctionCategory_Model.DisciplineCategoryId = SanctionCategory.DisciplineCategoryId;
			Default_Form_SanctionCategory_Model.Name = SanctionCategory.Name;
			Default_Form_SanctionCategory_Model.Code = SanctionCategory.Code;
			Default_Form_SanctionCategory_Model.DecisionAuthority = SanctionCategory.DecisionAuthority;
			Default_Form_SanctionCategory_Model.WorkflowOrder = SanctionCategory.WorkflowOrder;
			Default_Form_SanctionCategory_Model.Number_Of_Days_Of_Exclusion = SanctionCategory.Number_Of_Days_Of_Exclusion;
			Default_Form_SanctionCategory_Model.Plurality_Of_Absences = SanctionCategory.Plurality_Of_Absences;
			Default_Form_SanctionCategory_Model.Deducted_Points = SanctionCategory.Deducted_Points;
			Default_Form_SanctionCategory_Model.Description = SanctionCategory.Description;
			Default_Form_SanctionCategory_Model.Id = SanctionCategory.Id;
			Default_Form_SanctionCategory_Model.Reference = SanctionCategory.Reference;
                     
        }

    }

	public partial class Default_Form_SanctionCategory_ModelBLM : BaseDefault_Form_SanctionCategory_Model_BLM
	{
		public Default_Form_SanctionCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
