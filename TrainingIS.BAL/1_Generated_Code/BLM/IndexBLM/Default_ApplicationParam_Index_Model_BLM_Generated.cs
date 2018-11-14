//modelType = Default_ApplicationParam_Index_Model

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
	public partial class BaseDefault_ApplicationParam_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_ApplicationParam_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ApplicationParam ConverTo_ApplicationParam(Default_ApplicationParam_Index_Model Default_ApplicationParam_Index_Model)
        {
			ApplicationParam ApplicationParam = null;
            if (Default_ApplicationParam_Index_Model.Id != 0)
            {
                ApplicationParam = new ApplicationParamBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_ApplicationParam_Index_Model.Id);
            }
            else
            {
                ApplicationParam = new ApplicationParam();
            } 
			ApplicationParam.Code = Default_ApplicationParam_Index_Model.Code;
			ApplicationParam.Name = Default_ApplicationParam_Index_Model.Name;
			ApplicationParam.Value = Default_ApplicationParam_Index_Model.Value;
			ApplicationParam.Description = Default_ApplicationParam_Index_Model.Description;
			ApplicationParam.Id = Default_ApplicationParam_Index_Model.Id;
            return ApplicationParam;
        }
        public virtual Default_ApplicationParam_Index_Model ConverTo_Default_ApplicationParam_Index_Model(ApplicationParam ApplicationParam)
        {  
			Default_ApplicationParam_Index_Model Default_ApplicationParam_Index_Model = new Default_ApplicationParam_Index_Model();
			Default_ApplicationParam_Index_Model.toStringValue = ApplicationParam.ToString();
			Default_ApplicationParam_Index_Model.Code = ApplicationParam.Code;
			Default_ApplicationParam_Index_Model.Name = ApplicationParam.Name;
			Default_ApplicationParam_Index_Model.Value = ApplicationParam.Value;
			Default_ApplicationParam_Index_Model.Description = ApplicationParam.Description;
			Default_ApplicationParam_Index_Model.Id = ApplicationParam.Id;
            return Default_ApplicationParam_Index_Model;            
        }

		public virtual Default_ApplicationParam_Index_Model CreateNew()
        {
            ApplicationParam ApplicationParam = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_ApplicationParam_Index_Model Default_ApplicationParam_Index_Model = this.ConverTo_Default_ApplicationParam_Index_Model(ApplicationParam);
            return Default_ApplicationParam_Index_Model;
        } 

		public virtual List<Default_ApplicationParam_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ApplicationParamBLO entityBLO = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ApplicationParam> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_ApplicationParam_Index_Model> ls_models = new List<Default_ApplicationParam_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_ApplicationParam_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_ApplicationParam_Index_ModelBLM : BaseDefault_ApplicationParam_Index_Model_BLM
	{
		public Default_ApplicationParam_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
