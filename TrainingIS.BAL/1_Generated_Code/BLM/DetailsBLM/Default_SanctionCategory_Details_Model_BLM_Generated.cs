//modelType = Default_SanctionCategory_Details_Model

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
	public partial class BaseDefault_SanctionCategory_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_SanctionCategory_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SanctionCategory ConverTo_SanctionCategory(Default_SanctionCategory_Details_Model Default_SanctionCategory_Details_Model)
        {
			SanctionCategory SanctionCategory = null;
            if (Default_SanctionCategory_Details_Model.Id != 0)
            {
                SanctionCategory = new SanctionCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_SanctionCategory_Details_Model.Id);
            }
            else
            {
                SanctionCategory = new SanctionCategory();
            } 
			SanctionCategory.DisciplineCategory = Default_SanctionCategory_Details_Model.DisciplineCategory;
			SanctionCategory.Name = Default_SanctionCategory_Details_Model.Name;
			SanctionCategory.Code = Default_SanctionCategory_Details_Model.Code;
			SanctionCategory.DecisionAuthority = Default_SanctionCategory_Details_Model.DecisionAuthority;
			SanctionCategory.WorkflowOrder = Default_SanctionCategory_Details_Model.WorkflowOrder;
			SanctionCategory.Number_Of_Days_Of_Exclusion = Default_SanctionCategory_Details_Model.Number_Of_Days_Of_Exclusion;
			SanctionCategory.Plurality_Of_Absences = Default_SanctionCategory_Details_Model.Plurality_Of_Absences;
			SanctionCategory.Deducted_Points = Default_SanctionCategory_Details_Model.Deducted_Points;
			SanctionCategory.Description = Default_SanctionCategory_Details_Model.Description;
			SanctionCategory.Id = Default_SanctionCategory_Details_Model.Id;
            return SanctionCategory;
        }
        public virtual Default_SanctionCategory_Details_Model ConverTo_Default_SanctionCategory_Details_Model(SanctionCategory SanctionCategory)
        {  
			Default_SanctionCategory_Details_Model Default_SanctionCategory_Details_Model = new Default_SanctionCategory_Details_Model();
			Default_SanctionCategory_Details_Model.toStringValue = SanctionCategory.ToString();
			Default_SanctionCategory_Details_Model.DisciplineCategory = SanctionCategory.DisciplineCategory;
			Default_SanctionCategory_Details_Model.Name = SanctionCategory.Name;
			Default_SanctionCategory_Details_Model.Code = SanctionCategory.Code;
			Default_SanctionCategory_Details_Model.DecisionAuthority = SanctionCategory.DecisionAuthority;
			Default_SanctionCategory_Details_Model.WorkflowOrder = SanctionCategory.WorkflowOrder;
			Default_SanctionCategory_Details_Model.Number_Of_Days_Of_Exclusion = SanctionCategory.Number_Of_Days_Of_Exclusion;
			Default_SanctionCategory_Details_Model.Plurality_Of_Absences = SanctionCategory.Plurality_Of_Absences;
			Default_SanctionCategory_Details_Model.Deducted_Points = SanctionCategory.Deducted_Points;
			Default_SanctionCategory_Details_Model.Description = SanctionCategory.Description;
			Default_SanctionCategory_Details_Model.Id = SanctionCategory.Id;
            return Default_SanctionCategory_Details_Model;            
        }

		public virtual Default_SanctionCategory_Details_Model CreateNew()
        {
            SanctionCategory SanctionCategory = new SanctionCategoryBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_SanctionCategory_Details_Model Default_SanctionCategory_Details_Model = this.ConverTo_Default_SanctionCategory_Details_Model(SanctionCategory);
            return Default_SanctionCategory_Details_Model;
        } 

		public virtual List<Default_SanctionCategory_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SanctionCategoryBLO entityBLO = new SanctionCategoryBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SanctionCategory> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_SanctionCategory_Details_Model> ls_models = new List<Default_SanctionCategory_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_SanctionCategory_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_SanctionCategory_Details_ModelBLM : BaseDefault_SanctionCategory_Details_Model_BLM
	{
		public Default_SanctionCategory_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
