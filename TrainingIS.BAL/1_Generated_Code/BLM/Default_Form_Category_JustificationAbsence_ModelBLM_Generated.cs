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
	public partial class BaseDefault_Form_Category_JustificationAbsence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Category_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
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
			Category_JustificationAbsence.Id = Default_Form_Category_JustificationAbsence_Model.Id;
            return Category_JustificationAbsence;
        }
        public virtual Default_Form_Category_JustificationAbsence_Model ConverTo_Default_Form_Category_JustificationAbsence_Model(Category_JustificationAbsence Category_JustificationAbsence)
        {  
			Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model = new Default_Form_Category_JustificationAbsence_Model();
			Default_Form_Category_JustificationAbsence_Model.toStringValue = Category_JustificationAbsence.ToString();
			Default_Form_Category_JustificationAbsence_Model.Name = Category_JustificationAbsence.Name;
			Default_Form_Category_JustificationAbsence_Model.Description = Category_JustificationAbsence.Description;
			Default_Form_Category_JustificationAbsence_Model.Id = Category_JustificationAbsence.Id;
            return Default_Form_Category_JustificationAbsence_Model;            
        }

		public virtual Default_Form_Category_JustificationAbsence_Model CreateNew()
        {
            Category_JustificationAbsence Category_JustificationAbsence = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model = this.ConverTo_Default_Form_Category_JustificationAbsence_Model(Category_JustificationAbsence);
            return Default_Form_Category_JustificationAbsence_Model;
        } 

		public virtual List<Default_Form_Category_JustificationAbsence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Category_JustificationAbsenceBLO entityBLO = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Category_JustificationAbsence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Category_JustificationAbsence_Model> ls_models = new List<Default_Form_Category_JustificationAbsence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Category_JustificationAbsence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Category_JustificationAbsence_ModelBLM : BaseDefault_Form_Category_JustificationAbsence_ModelBLM
	{
		public Default_Form_Category_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
