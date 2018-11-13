//modelType = Default_Details_SanctionCategory_Model

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
	public partial class BaseDefault_Details_SanctionCategory_Model_Index_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_SanctionCategory_Model_Index_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SanctionCategory ConverTo_SanctionCategory(Default_Details_SanctionCategory_Model Default_Details_SanctionCategory_Model)
        {
			SanctionCategory SanctionCategory = null;
            if (Default_Details_SanctionCategory_Model.Id != 0)
            {
                SanctionCategory = new SanctionCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_SanctionCategory_Model.Id);
            }
            else
            {
                SanctionCategory = new SanctionCategory();
            } 
			SanctionCategory.DisciplineCategory = Default_Details_SanctionCategory_Model.DisciplineCategory;
			SanctionCategory.Name = Default_Details_SanctionCategory_Model.Name;
			SanctionCategory.Code = Default_Details_SanctionCategory_Model.Code;
			SanctionCategory.DecisionAuthority = Default_Details_SanctionCategory_Model.DecisionAuthority;
			SanctionCategory.WorkflowOrder = Default_Details_SanctionCategory_Model.WorkflowOrder;
			SanctionCategory.Number_Of_Days_Of_Exclusion = Default_Details_SanctionCategory_Model.Number_Of_Days_Of_Exclusion;
			SanctionCategory.Plurality_Of_Absences = Default_Details_SanctionCategory_Model.Plurality_Of_Absences;
			SanctionCategory.Deducted_Points = Default_Details_SanctionCategory_Model.Deducted_Points;
			SanctionCategory.Description = Default_Details_SanctionCategory_Model.Description;
			SanctionCategory.Id = Default_Details_SanctionCategory_Model.Id;
            return SanctionCategory;
        }
        public virtual Default_Details_SanctionCategory_Model ConverTo_Default_Details_SanctionCategory_Model(SanctionCategory SanctionCategory)
        {  
			Default_Details_SanctionCategory_Model Default_Details_SanctionCategory_Model = new Default_Details_SanctionCategory_Model();
			Default_Details_SanctionCategory_Model.toStringValue = SanctionCategory.ToString();
			Default_Details_SanctionCategory_Model.DisciplineCategory = SanctionCategory.DisciplineCategory;
			Default_Details_SanctionCategory_Model.Name = SanctionCategory.Name;
			Default_Details_SanctionCategory_Model.Code = SanctionCategory.Code;
			Default_Details_SanctionCategory_Model.DecisionAuthority = SanctionCategory.DecisionAuthority;
			Default_Details_SanctionCategory_Model.WorkflowOrder = SanctionCategory.WorkflowOrder;
			Default_Details_SanctionCategory_Model.Number_Of_Days_Of_Exclusion = SanctionCategory.Number_Of_Days_Of_Exclusion;
			Default_Details_SanctionCategory_Model.Plurality_Of_Absences = SanctionCategory.Plurality_Of_Absences;
			Default_Details_SanctionCategory_Model.Deducted_Points = SanctionCategory.Deducted_Points;
			Default_Details_SanctionCategory_Model.Description = SanctionCategory.Description;
			Default_Details_SanctionCategory_Model.Id = SanctionCategory.Id;
            return Default_Details_SanctionCategory_Model;            
        }

		public virtual Default_Details_SanctionCategory_Model CreateNew()
        {
            SanctionCategory SanctionCategory = new SanctionCategoryBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_SanctionCategory_Model Default_Details_SanctionCategory_Model = this.ConverTo_Default_Details_SanctionCategory_Model(SanctionCategory);
            return Default_Details_SanctionCategory_Model;
        } 

		public virtual List<Default_Details_SanctionCategory_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SanctionCategoryBLO entityBLO = new SanctionCategoryBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SanctionCategory> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_SanctionCategory_Model> ls_models = new List<Default_Details_SanctionCategory_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_SanctionCategory_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_SanctionCategory_ModelBLM : BaseDefault_Details_SanctionCategory_Model_Index_BLM
	{
		public Default_Details_SanctionCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
