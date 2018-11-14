//modelType = Default_FormerSpecialty_Edit_Model

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
	public partial class BaseDefault_FormerSpecialty_Edit_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Default_Form_FormerSpecialty_ModelBLM Default_Form_FormerSpecialty_ModelBLM {set;get;}
        
		public BaseDefault_FormerSpecialty_Edit_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_FormerSpecialty_ModelBLM = new Default_Form_FormerSpecialty_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual FormerSpecialty ConverTo_FormerSpecialty(Default_FormerSpecialty_Edit_Model Default_FormerSpecialty_Edit_Model)
        {
            var FormerSpecialty = Default_Form_FormerSpecialty_ModelBLM.ConverTo_FormerSpecialty(Default_FormerSpecialty_Edit_Model);
            return FormerSpecialty;
        }

		public virtual Default_FormerSpecialty_Edit_Model ConverTo_Default_FormerSpecialty_Edit_Model(FormerSpecialty FormerSpecialty)
        {
            Default_FormerSpecialty_Edit_Model Default_FormerSpecialty_Edit_Model = new Default_FormerSpecialty_Edit_Model();
            Default_Form_FormerSpecialty_ModelBLM.ConverTo_Default_Form_FormerSpecialty_Model(Default_FormerSpecialty_Edit_Model, FormerSpecialty);
            return Default_FormerSpecialty_Edit_Model;            
        }

		public virtual Default_FormerSpecialty_Edit_Model CreateNew()
        {
            FormerSpecialty FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_FormerSpecialty_Edit_Model Default_FormerSpecialty_Edit_Model = this.ConverTo_Default_FormerSpecialty_Edit_Model(FormerSpecialty);
            return Default_FormerSpecialty_Edit_Model;
        } 

		public virtual List<Default_FormerSpecialty_Edit_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerSpecialtyBLO entityBLO = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<FormerSpecialty> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_FormerSpecialty_Edit_Model> ls_models = new List<Default_FormerSpecialty_Edit_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_FormerSpecialty_Edit_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_FormerSpecialty_Edit_ModelBLM : BaseDefault_FormerSpecialty_Edit_Model_BLM
	{
		public Default_FormerSpecialty_Edit_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
