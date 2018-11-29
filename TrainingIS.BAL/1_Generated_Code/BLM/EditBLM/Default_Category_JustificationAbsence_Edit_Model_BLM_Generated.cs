//modelType = Default_Category_JustificationAbsence_Edit_Model

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
	public partial class BaseDefault_Category_JustificationAbsence_Edit_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_Category_JustificationAbsence_ModelBLM Default_Form_Category_JustificationAbsence_ModelBLM {set;get;}
        
		public BaseDefault_Category_JustificationAbsence_Edit_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_Category_JustificationAbsence_ModelBLM = new Default_Form_Category_JustificationAbsence_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Category_JustificationAbsence ConverTo_Category_JustificationAbsence(Default_Category_JustificationAbsence_Edit_Model Default_Category_JustificationAbsence_Edit_Model)
        {
            var Category_JustificationAbsence = Default_Form_Category_JustificationAbsence_ModelBLM.ConverTo_Category_JustificationAbsence(Default_Category_JustificationAbsence_Edit_Model);
            return Category_JustificationAbsence;
        }

		public virtual Default_Category_JustificationAbsence_Edit_Model ConverTo_Default_Category_JustificationAbsence_Edit_Model(Category_JustificationAbsence Category_JustificationAbsence)
        {
            Default_Category_JustificationAbsence_Edit_Model Default_Category_JustificationAbsence_Edit_Model = new Default_Category_JustificationAbsence_Edit_Model();
            Default_Form_Category_JustificationAbsence_ModelBLM.ConverTo_Default_Form_Category_JustificationAbsence_Model(Default_Category_JustificationAbsence_Edit_Model, Category_JustificationAbsence);
            return Default_Category_JustificationAbsence_Edit_Model;            
        }

		public virtual Default_Category_JustificationAbsence_Edit_Model CreateNew()
        {
            Category_JustificationAbsence Category_JustificationAbsence = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Category_JustificationAbsence_Edit_Model Default_Category_JustificationAbsence_Edit_Model = this.ConverTo_Default_Category_JustificationAbsence_Edit_Model(Category_JustificationAbsence);
            return Default_Category_JustificationAbsence_Edit_Model;
        } 

		public virtual List<Default_Category_JustificationAbsence_Edit_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Category_JustificationAbsenceBLO entityBLO = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Category_JustificationAbsence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Category_JustificationAbsence_Edit_Model> ls_models = new List<Default_Category_JustificationAbsence_Edit_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Category_JustificationAbsence_Edit_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Category_JustificationAbsence_Edit_ModelBLM : BaseDefault_Category_JustificationAbsence_Edit_Model_BLM
	{
		public Default_Category_JustificationAbsence_Edit_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
