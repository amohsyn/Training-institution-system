//modelType = Default_TaskProject_Edit_Model

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
	public partial class BaseDefault_TaskProject_Edit_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_TaskProject_ModelBLM Default_Form_TaskProject_ModelBLM {set;get;}
        
		public BaseDefault_TaskProject_Edit_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_TaskProject_ModelBLM = new Default_Form_TaskProject_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual TaskProject ConverTo_TaskProject(Default_TaskProject_Edit_Model Default_TaskProject_Edit_Model)
        {
            var TaskProject = Default_Form_TaskProject_ModelBLM.ConverTo_TaskProject(Default_TaskProject_Edit_Model);
            return TaskProject;
        }

		public virtual Default_TaskProject_Edit_Model ConverTo_Default_TaskProject_Edit_Model(TaskProject TaskProject)
        {
            Default_TaskProject_Edit_Model Default_TaskProject_Edit_Model = new Default_TaskProject_Edit_Model();
            Default_Form_TaskProject_ModelBLM.ConverTo_Default_Form_TaskProject_Model(Default_TaskProject_Edit_Model, TaskProject);
            return Default_TaskProject_Edit_Model;            
        }

		public virtual Default_TaskProject_Edit_Model CreateNew()
        {
            TaskProject TaskProject = new TaskProjectBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_TaskProject_Edit_Model Default_TaskProject_Edit_Model = this.ConverTo_Default_TaskProject_Edit_Model(TaskProject);
            return Default_TaskProject_Edit_Model;
        } 

		public virtual List<Default_TaskProject_Edit_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TaskProjectBLO entityBLO = new TaskProjectBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<TaskProject> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_TaskProject_Edit_Model> ls_models = new List<Default_TaskProject_Edit_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_TaskProject_Edit_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_TaskProject_Edit_ModelBLM : BaseDefault_TaskProject_Edit_Model_BLM
	{
		public Default_TaskProject_Edit_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
