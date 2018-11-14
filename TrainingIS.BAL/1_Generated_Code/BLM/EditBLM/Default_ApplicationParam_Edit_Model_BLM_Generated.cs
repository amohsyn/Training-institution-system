//modelType = Default_ApplicationParam_Edit_Model

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
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_ApplicationParam_Edit_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Default_Form_ApplicationParam_ModelBLM Default_Form_ApplicationParam_ModelBLM {set;get;}
        
		public BaseDefault_ApplicationParam_Edit_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_ApplicationParam_ModelBLM = new Default_Form_ApplicationParam_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual ApplicationParam ConverTo_ApplicationParam(Default_ApplicationParam_Edit_Model Default_ApplicationParam_Edit_Model)
        {
            var ApplicationParam = Default_Form_ApplicationParam_ModelBLM.ConverTo_ApplicationParam(Default_ApplicationParam_Edit_Model);
            return ApplicationParam;
        }

		public virtual Default_ApplicationParam_Edit_Model ConverTo_Default_ApplicationParam_Edit_Model(ApplicationParam ApplicationParam)
        {
            Default_ApplicationParam_Edit_Model Default_ApplicationParam_Edit_Model = new Default_ApplicationParam_Edit_Model();
            Default_Form_ApplicationParam_ModelBLM.ConverTo_Default_Form_ApplicationParam_Model(Default_ApplicationParam_Edit_Model, ApplicationParam);
            return Default_ApplicationParam_Edit_Model;            
        }

		public virtual Default_ApplicationParam_Edit_Model CreateNew()
        {
            ApplicationParam ApplicationParam = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_ApplicationParam_Edit_Model Default_ApplicationParam_Edit_Model = this.ConverTo_Default_ApplicationParam_Edit_Model(ApplicationParam);
            return Default_ApplicationParam_Edit_Model;
        } 

		public virtual List<Default_ApplicationParam_Edit_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ApplicationParamBLO entityBLO = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ApplicationParam> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_ApplicationParam_Edit_Model> ls_models = new List<Default_ApplicationParam_Edit_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_ApplicationParam_Edit_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_ApplicationParam_Edit_ModelBLM : BaseDefault_ApplicationParam_Edit_Model_BLM
	{
		public Default_ApplicationParam_Edit_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
