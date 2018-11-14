//modelType = Default_RoleApp_Export_Model

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
	public partial class BaseDefault_RoleApp_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_RoleApp_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual RoleApp ConverTo_RoleApp(Default_RoleApp_Export_Model Default_RoleApp_Export_Model)
        {
			RoleApp RoleApp = null;
            if (Default_RoleApp_Export_Model.Id != 0)
            {
                RoleApp = new RoleAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_RoleApp_Export_Model.Id);
            }
            else
            {
                RoleApp = new RoleApp();
            } 
			RoleApp.Code = Default_RoleApp_Export_Model.Code;
			RoleApp.Description = Default_RoleApp_Export_Model.Description;
			RoleApp.Id = Default_RoleApp_Export_Model.Id;
            return RoleApp;
        }
        public virtual Default_RoleApp_Export_Model ConverTo_Default_RoleApp_Export_Model(RoleApp RoleApp)
        {  
			Default_RoleApp_Export_Model Default_RoleApp_Export_Model = new Default_RoleApp_Export_Model();
			Default_RoleApp_Export_Model.toStringValue = RoleApp.ToString();
			Default_RoleApp_Export_Model.Code = RoleApp.Code;
			Default_RoleApp_Export_Model.Description = RoleApp.Description;
			Default_RoleApp_Export_Model.Id = RoleApp.Id;
            return Default_RoleApp_Export_Model;            
        }

		public virtual Default_RoleApp_Export_Model CreateNew()
        {
            RoleApp RoleApp = new RoleAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_RoleApp_Export_Model Default_RoleApp_Export_Model = this.ConverTo_Default_RoleApp_Export_Model(RoleApp);
            return Default_RoleApp_Export_Model;
        } 

		public virtual List<Default_RoleApp_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            RoleAppBLO entityBLO = new RoleAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<RoleApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_RoleApp_Export_Model> ls_models = new List<Default_RoleApp_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_RoleApp_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_RoleApp_Export_ModelBLM : BaseDefault_RoleApp_Export_Model_BLM
	{
		public Default_RoleApp_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
