//modelType = Default_Form_ApplicationParam_Model

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
	public partial class BaseDefault_Form_ApplicationParam_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ApplicationParam_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ApplicationParam ConverTo_ApplicationParam(Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model)
        {
			ApplicationParam ApplicationParam = null;
            if (Default_Form_ApplicationParam_Model.Id != 0)
            {
                ApplicationParam = new ApplicationParamBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ApplicationParam_Model.Id);
            }
            else
            {
                ApplicationParam = new ApplicationParam();
            } 
			ApplicationParam.Code = Default_Form_ApplicationParam_Model.Code;
			ApplicationParam.Name = Default_Form_ApplicationParam_Model.Name;
			ApplicationParam.Value = Default_Form_ApplicationParam_Model.Value;
			ApplicationParam.Description = Default_Form_ApplicationParam_Model.Description;
			ApplicationParam.Id = Default_Form_ApplicationParam_Model.Id;
            return ApplicationParam;
        }
        public virtual Default_Form_ApplicationParam_Model ConverTo_Default_Form_ApplicationParam_Model(ApplicationParam ApplicationParam)
        {  
			Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model = new Default_Form_ApplicationParam_Model();
			Default_Form_ApplicationParam_Model.toStringValue = ApplicationParam.ToString();
			Default_Form_ApplicationParam_Model.Code = ApplicationParam.Code;
			Default_Form_ApplicationParam_Model.Name = ApplicationParam.Name;
			Default_Form_ApplicationParam_Model.Value = ApplicationParam.Value;
			Default_Form_ApplicationParam_Model.Description = ApplicationParam.Description;
			Default_Form_ApplicationParam_Model.Id = ApplicationParam.Id;
            return Default_Form_ApplicationParam_Model;            
        }

		public virtual Default_Form_ApplicationParam_Model CreateNew()
        {
            ApplicationParam ApplicationParam = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model = this.ConverTo_Default_Form_ApplicationParam_Model(ApplicationParam);
            return Default_Form_ApplicationParam_Model;
        } 

		public virtual List<Default_Form_ApplicationParam_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ApplicationParamBLO entityBLO = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ApplicationParam> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_ApplicationParam_Model> ls_models = new List<Default_Form_ApplicationParam_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_ApplicationParam_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_ApplicationParam_ModelBLM : BaseDefault_Form_ApplicationParam_ModelBLM
	{
		public Default_Form_ApplicationParam_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
