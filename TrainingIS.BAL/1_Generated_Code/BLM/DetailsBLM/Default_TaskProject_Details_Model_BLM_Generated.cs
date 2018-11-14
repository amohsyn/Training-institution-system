//modelType = Default_TaskProject_Details_Model

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
	public partial class BaseDefault_TaskProject_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_TaskProject_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual TaskProject ConverTo_TaskProject(Default_TaskProject_Details_Model Default_TaskProject_Details_Model)
        {
			TaskProject TaskProject = null;
            if (Default_TaskProject_Details_Model.Id != 0)
            {
                TaskProject = new TaskProjectBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_TaskProject_Details_Model.Id);
            }
            else
            {
                TaskProject = new TaskProject();
            } 
			TaskProject.Project = Default_TaskProject_Details_Model.Project;
			TaskProject.TaskState = Default_TaskProject_Details_Model.TaskState;
			TaskProject.Name = Default_TaskProject_Details_Model.Name;
			TaskProject.Description = Default_TaskProject_Details_Model.Description;
			TaskProject.StartDate = DefaultDateTime_If_Empty(Default_TaskProject_Details_Model.StartDate);
			TaskProject.EndtDate = DefaultDateTime_If_Empty(Default_TaskProject_Details_Model.EndtDate);
			TaskProject.isPublic = Default_TaskProject_Details_Model.isPublic;
			TaskProject.Id = Default_TaskProject_Details_Model.Id;
            return TaskProject;
        }
        public virtual Default_TaskProject_Details_Model ConverTo_Default_TaskProject_Details_Model(TaskProject TaskProject)
        {  
			Default_TaskProject_Details_Model Default_TaskProject_Details_Model = new Default_TaskProject_Details_Model();
			Default_TaskProject_Details_Model.toStringValue = TaskProject.ToString();
			Default_TaskProject_Details_Model.Project = TaskProject.Project;
			Default_TaskProject_Details_Model.TaskState = TaskProject.TaskState;
			Default_TaskProject_Details_Model.Name = TaskProject.Name;
			Default_TaskProject_Details_Model.Description = TaskProject.Description;
			Default_TaskProject_Details_Model.StartDate = DefaultDateTime_If_Empty(TaskProject.StartDate);
			Default_TaskProject_Details_Model.EndtDate = DefaultDateTime_If_Empty(TaskProject.EndtDate);
			Default_TaskProject_Details_Model.isPublic = TaskProject.isPublic;
			Default_TaskProject_Details_Model.Id = TaskProject.Id;
            return Default_TaskProject_Details_Model;            
        }

		public virtual Default_TaskProject_Details_Model CreateNew()
        {
            TaskProject TaskProject = new TaskProjectBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_TaskProject_Details_Model Default_TaskProject_Details_Model = this.ConverTo_Default_TaskProject_Details_Model(TaskProject);
            return Default_TaskProject_Details_Model;
        } 

		public virtual List<Default_TaskProject_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TaskProjectBLO entityBLO = new TaskProjectBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<TaskProject> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_TaskProject_Details_Model> ls_models = new List<Default_TaskProject_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_TaskProject_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_TaskProject_Details_ModelBLM : BaseDefault_TaskProject_Details_Model_BLM
	{
		public Default_TaskProject_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
