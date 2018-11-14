//modelType = Default_Category_WarningTrainee_Create_Model

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
	public partial class BaseDefault_Category_WarningTrainee_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Default_Form_Category_WarningTrainee_ModelBLM Default_Form_Category_WarningTrainee_ModelBLM {set;get;}
        
		public BaseDefault_Category_WarningTrainee_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_Category_WarningTrainee_ModelBLM = new Default_Form_Category_WarningTrainee_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Category_WarningTrainee ConverTo_Category_WarningTrainee(Default_Category_WarningTrainee_Create_Model Default_Category_WarningTrainee_Create_Model)
        {
            var Category_WarningTrainee = Default_Form_Category_WarningTrainee_ModelBLM.ConverTo_Category_WarningTrainee(Default_Category_WarningTrainee_Create_Model);
            return Category_WarningTrainee;
        }

		public virtual Default_Category_WarningTrainee_Create_Model ConverTo_Default_Category_WarningTrainee_Create_Model(Category_WarningTrainee Category_WarningTrainee)
        {
            Default_Category_WarningTrainee_Create_Model Default_Category_WarningTrainee_Create_Model = new Default_Category_WarningTrainee_Create_Model();
            Default_Form_Category_WarningTrainee_ModelBLM.ConverTo_Default_Form_Category_WarningTrainee_Model(Default_Category_WarningTrainee_Create_Model, Category_WarningTrainee);
            return Default_Category_WarningTrainee_Create_Model;            
        }

		public virtual Default_Category_WarningTrainee_Create_Model CreateNew()
        {
            Category_WarningTrainee Category_WarningTrainee = new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Category_WarningTrainee_Create_Model Default_Category_WarningTrainee_Create_Model = this.ConverTo_Default_Category_WarningTrainee_Create_Model(Category_WarningTrainee);
            return Default_Category_WarningTrainee_Create_Model;
        } 

		public virtual List<Default_Category_WarningTrainee_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Category_WarningTraineeBLO entityBLO = new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Category_WarningTrainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Category_WarningTrainee_Create_Model> ls_models = new List<Default_Category_WarningTrainee_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Category_WarningTrainee_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Category_WarningTrainee_Create_ModelBLM : BaseDefault_Category_WarningTrainee_Create_Model_BLM
	{
		public Default_Category_WarningTrainee_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
