//modelType = Default_Details_ControllerApp_Model

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
	public partial class BaseDefault_Details_ControllerApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_ControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ControllerApp ConverTo_ControllerApp(Default_Details_ControllerApp_Model Default_Details_ControllerApp_Model)
        {
			ControllerApp ControllerApp = null;
            if (Default_Details_ControllerApp_Model.Id != 0)
            {
                ControllerApp = new ControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_ControllerApp_Model.Id);
            }
            else
            {
                ControllerApp = new ControllerApp();
            } 
			ControllerApp.Code = Default_Details_ControllerApp_Model.Code;
			ControllerApp.Name = Default_Details_ControllerApp_Model.Name;
			ControllerApp.Description = Default_Details_ControllerApp_Model.Description;
			ControllerApp.Id = Default_Details_ControllerApp_Model.Id;
            return ControllerApp;
        }
        public virtual Default_Details_ControllerApp_Model ConverTo_Default_Details_ControllerApp_Model(ControllerApp ControllerApp)
        {  
			Default_Details_ControllerApp_Model Default_Details_ControllerApp_Model = new Default_Details_ControllerApp_Model();
			Default_Details_ControllerApp_Model.toStringValue = ControllerApp.ToString();
			Default_Details_ControllerApp_Model.Code = ControllerApp.Code;
			Default_Details_ControllerApp_Model.Name = ControllerApp.Name;
			Default_Details_ControllerApp_Model.Description = ControllerApp.Description;
			Default_Details_ControllerApp_Model.Id = ControllerApp.Id;
            return Default_Details_ControllerApp_Model;            
        }

		public virtual Default_Details_ControllerApp_Model CreateNew()
        {
            ControllerApp ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_ControllerApp_Model Default_Details_ControllerApp_Model = this.ConverTo_Default_Details_ControllerApp_Model(ControllerApp);
            return Default_Details_ControllerApp_Model;
        } 

		public virtual List<Default_Details_ControllerApp_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ControllerAppBLO entityBLO = new ControllerAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ControllerApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_ControllerApp_Model> ls_models = new List<Default_Details_ControllerApp_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_ControllerApp_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_ControllerApp_ModelBLM : BaseDefault_Details_ControllerApp_ModelBLM
	{
		public Default_Details_ControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
