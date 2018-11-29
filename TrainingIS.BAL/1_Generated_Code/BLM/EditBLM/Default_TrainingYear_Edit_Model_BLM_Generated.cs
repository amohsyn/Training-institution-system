//modelType = Default_TrainingYear_Edit_Model

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
	public partial class BaseDefault_TrainingYear_Edit_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_TrainingYear_ModelBLM Default_Form_TrainingYear_ModelBLM {set;get;}
        
		public BaseDefault_TrainingYear_Edit_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_TrainingYear_ModelBLM = new Default_Form_TrainingYear_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual TrainingYear ConverTo_TrainingYear(Default_TrainingYear_Edit_Model Default_TrainingYear_Edit_Model)
        {
            var TrainingYear = Default_Form_TrainingYear_ModelBLM.ConverTo_TrainingYear(Default_TrainingYear_Edit_Model);
            return TrainingYear;
        }

		public virtual Default_TrainingYear_Edit_Model ConverTo_Default_TrainingYear_Edit_Model(TrainingYear TrainingYear)
        {
            Default_TrainingYear_Edit_Model Default_TrainingYear_Edit_Model = new Default_TrainingYear_Edit_Model();
            Default_Form_TrainingYear_ModelBLM.ConverTo_Default_Form_TrainingYear_Model(Default_TrainingYear_Edit_Model, TrainingYear);
            return Default_TrainingYear_Edit_Model;            
        }

		public virtual Default_TrainingYear_Edit_Model CreateNew()
        {
            TrainingYear TrainingYear = new TrainingYearBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_TrainingYear_Edit_Model Default_TrainingYear_Edit_Model = this.ConverTo_Default_TrainingYear_Edit_Model(TrainingYear);
            return Default_TrainingYear_Edit_Model;
        } 

		public virtual List<Default_TrainingYear_Edit_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TrainingYearBLO entityBLO = new TrainingYearBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<TrainingYear> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_TrainingYear_Edit_Model> ls_models = new List<Default_TrainingYear_Edit_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_TrainingYear_Edit_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_TrainingYear_Edit_ModelBLM : BaseDefault_TrainingYear_Edit_Model_BLM
	{
		public Default_TrainingYear_Edit_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
