//modelType = Default_Form_Category_WarningTrainee_Model

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
	public partial class BaseDefault_Form_Category_WarningTrainee_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Category_WarningTrainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Category_WarningTrainee ConverTo_Category_WarningTrainee(Default_Form_Category_WarningTrainee_Model Default_Form_Category_WarningTrainee_Model)
        {
			Category_WarningTrainee Category_WarningTrainee = null;
            if (Default_Form_Category_WarningTrainee_Model.Id != 0)
            {
                Category_WarningTrainee = new Category_WarningTraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Category_WarningTrainee_Model.Id);
            }
            else
            {
                Category_WarningTrainee = new Category_WarningTrainee();
            } 
			Category_WarningTrainee.Name = Default_Form_Category_WarningTrainee_Model.Name;
			Category_WarningTrainee.Description = Default_Form_Category_WarningTrainee_Model.Description;
			Category_WarningTrainee.Reference = Default_Form_Category_WarningTrainee_Model.Reference;
			Category_WarningTrainee.Id = Default_Form_Category_WarningTrainee_Model.Id;
            return Category_WarningTrainee;
        }
        public virtual Default_Form_Category_WarningTrainee_Model ConverTo_Default_Form_Category_WarningTrainee_Model(Category_WarningTrainee Category_WarningTrainee)
        {  
			Default_Form_Category_WarningTrainee_Model Default_Form_Category_WarningTrainee_Model = new Default_Form_Category_WarningTrainee_Model();
			Default_Form_Category_WarningTrainee_Model.toStringValue = Category_WarningTrainee.ToString();
			Default_Form_Category_WarningTrainee_Model.Name = Category_WarningTrainee.Name;
			Default_Form_Category_WarningTrainee_Model.Description = Category_WarningTrainee.Description;
			Default_Form_Category_WarningTrainee_Model.Id = Category_WarningTrainee.Id;
			Default_Form_Category_WarningTrainee_Model.Reference = Category_WarningTrainee.Reference;
            return Default_Form_Category_WarningTrainee_Model;            
        }

		public virtual Default_Form_Category_WarningTrainee_Model CreateNew()
        {
            Category_WarningTrainee Category_WarningTrainee = new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Category_WarningTrainee_Model Default_Form_Category_WarningTrainee_Model = this.ConverTo_Default_Form_Category_WarningTrainee_Model(Category_WarningTrainee);
            return Default_Form_Category_WarningTrainee_Model;
        } 

		public virtual List<Default_Form_Category_WarningTrainee_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Category_WarningTraineeBLO entityBLO = new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Category_WarningTrainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Category_WarningTrainee_Model> ls_models = new List<Default_Form_Category_WarningTrainee_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Category_WarningTrainee_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Category_WarningTrainee_ModelBLM : BaseDefault_Form_Category_WarningTrainee_ModelBLM
	{
		public Default_Form_Category_WarningTrainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
