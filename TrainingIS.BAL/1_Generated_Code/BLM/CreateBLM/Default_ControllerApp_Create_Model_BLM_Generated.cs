//modelType = Default_ControllerApp_Create_Model

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
	public partial class BaseDefault_ControllerApp_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_ControllerApp_ModelBLM Default_Form_ControllerApp_ModelBLM {set;get;}
        
		public BaseDefault_ControllerApp_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_ControllerApp_ModelBLM = new Default_Form_ControllerApp_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual ControllerApp ConverTo_ControllerApp(Default_ControllerApp_Create_Model Default_ControllerApp_Create_Model)
        {
            var ControllerApp = Default_Form_ControllerApp_ModelBLM.ConverTo_ControllerApp(Default_ControllerApp_Create_Model);
            return ControllerApp;
        }

		public virtual Default_ControllerApp_Create_Model ConverTo_Default_ControllerApp_Create_Model(ControllerApp ControllerApp)
        {
            Default_ControllerApp_Create_Model Default_ControllerApp_Create_Model = new Default_ControllerApp_Create_Model();
            Default_Form_ControllerApp_ModelBLM.ConverTo_Default_Form_ControllerApp_Model(Default_ControllerApp_Create_Model, ControllerApp);
            return Default_ControllerApp_Create_Model;            
        }

		public virtual Default_ControllerApp_Create_Model CreateNew()
        {
            ControllerApp ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_ControllerApp_Create_Model Default_ControllerApp_Create_Model = this.ConverTo_Default_ControllerApp_Create_Model(ControllerApp);
            return Default_ControllerApp_Create_Model;
        } 

		public virtual List<Default_ControllerApp_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ControllerAppBLO entityBLO = new ControllerAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ControllerApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_ControllerApp_Create_Model> ls_models = new List<Default_ControllerApp_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_ControllerApp_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_ControllerApp_Create_ModelBLM : BaseDefault_ControllerApp_Create_Model_BLM
	{
		public Default_ControllerApp_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
