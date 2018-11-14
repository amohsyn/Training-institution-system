//modelType = Default_DisciplineCategory_Export_Model

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
	public partial class BaseDefault_DisciplineCategory_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_DisciplineCategory_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual DisciplineCategory ConverTo_DisciplineCategory(Default_DisciplineCategory_Export_Model Default_DisciplineCategory_Export_Model)
        {
			DisciplineCategory DisciplineCategory = null;
            if (Default_DisciplineCategory_Export_Model.Id != 0)
            {
                DisciplineCategory = new DisciplineCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_DisciplineCategory_Export_Model.Id);
            }
            else
            {
                DisciplineCategory = new DisciplineCategory();
            } 
			DisciplineCategory.Code = Default_DisciplineCategory_Export_Model.Code;
			DisciplineCategory.Name = Default_DisciplineCategory_Export_Model.Name;
			DisciplineCategory.System_DisciplineCategy = Default_DisciplineCategory_Export_Model.System_DisciplineCategy;
			DisciplineCategory.Description = Default_DisciplineCategory_Export_Model.Description;
			DisciplineCategory.Id = Default_DisciplineCategory_Export_Model.Id;
            return DisciplineCategory;
        }
        public virtual Default_DisciplineCategory_Export_Model ConverTo_Default_DisciplineCategory_Export_Model(DisciplineCategory DisciplineCategory)
        {  
			Default_DisciplineCategory_Export_Model Default_DisciplineCategory_Export_Model = new Default_DisciplineCategory_Export_Model();
			Default_DisciplineCategory_Export_Model.toStringValue = DisciplineCategory.ToString();
			Default_DisciplineCategory_Export_Model.Code = DisciplineCategory.Code;
			Default_DisciplineCategory_Export_Model.Name = DisciplineCategory.Name;
			Default_DisciplineCategory_Export_Model.System_DisciplineCategy = DisciplineCategory.System_DisciplineCategy;
			Default_DisciplineCategory_Export_Model.Description = DisciplineCategory.Description;
			Default_DisciplineCategory_Export_Model.Id = DisciplineCategory.Id;
            return Default_DisciplineCategory_Export_Model;            
        }

		public virtual Default_DisciplineCategory_Export_Model CreateNew()
        {
            DisciplineCategory DisciplineCategory = new DisciplineCategoryBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_DisciplineCategory_Export_Model Default_DisciplineCategory_Export_Model = this.ConverTo_Default_DisciplineCategory_Export_Model(DisciplineCategory);
            return Default_DisciplineCategory_Export_Model;
        } 

		public virtual List<Default_DisciplineCategory_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            DisciplineCategoryBLO entityBLO = new DisciplineCategoryBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<DisciplineCategory> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_DisciplineCategory_Export_Model> ls_models = new List<Default_DisciplineCategory_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_DisciplineCategory_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_DisciplineCategory_Export_ModelBLM : BaseDefault_DisciplineCategory_Export_Model_BLM
	{
		public Default_DisciplineCategory_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
