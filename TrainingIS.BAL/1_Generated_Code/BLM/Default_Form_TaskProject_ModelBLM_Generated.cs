//modelType = Default_Form_TaskProject_Model

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
	public partial class BaseDefault_Form_TaskProject_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_TaskProject_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual TaskProject ConverTo_TaskProject(Default_Form_TaskProject_Model Default_Form_TaskProject_Model)
        {
			TaskProject TaskProject = null;
            if (Default_Form_TaskProject_Model.Id != 0)
            {
                TaskProject = new TaskProjectBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_TaskProject_Model.Id);
            }
            else
            {
                TaskProject = new TaskProject();
            } 
			TaskProject.ProjectId = Default_Form_TaskProject_Model.ProjectId;
			TaskProject.Project = new ProjectBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_TaskProject_Model.ProjectId)) ;
			TaskProject.TaskState = Default_Form_TaskProject_Model.TaskState;
			TaskProject.Name = Default_Form_TaskProject_Model.Name;
			TaskProject.Description = Default_Form_TaskProject_Model.Description;
			TaskProject.StartDate = DefaultDateTime_If_Empty(Default_Form_TaskProject_Model.StartDate);
			TaskProject.EndtDate = DefaultDateTime_If_Empty(Default_Form_TaskProject_Model.EndtDate);
			TaskProject.isPublic = Default_Form_TaskProject_Model.isPublic;
			TaskProject.Reference = Default_Form_TaskProject_Model.Reference;
			TaskProject.Id = Default_Form_TaskProject_Model.Id;
            return TaskProject;
        }
        public virtual Default_Form_TaskProject_Model ConverTo_Default_Form_TaskProject_Model(TaskProject TaskProject)
        {  
			Default_Form_TaskProject_Model Default_Form_TaskProject_Model = new Default_Form_TaskProject_Model();
			Default_Form_TaskProject_Model.toStringValue = TaskProject.ToString();
			Default_Form_TaskProject_Model.ProjectId = TaskProject.ProjectId;
			Default_Form_TaskProject_Model.TaskState = TaskProject.TaskState;
			Default_Form_TaskProject_Model.Name = TaskProject.Name;
			Default_Form_TaskProject_Model.Description = TaskProject.Description;
			Default_Form_TaskProject_Model.StartDate = DefaultDateTime_If_Empty(TaskProject.StartDate);
			Default_Form_TaskProject_Model.EndtDate = DefaultDateTime_If_Empty(TaskProject.EndtDate);
			Default_Form_TaskProject_Model.isPublic = TaskProject.isPublic;
			Default_Form_TaskProject_Model.Id = TaskProject.Id;
			Default_Form_TaskProject_Model.Reference = TaskProject.Reference;
            return Default_Form_TaskProject_Model;            
        }

		public virtual Default_Form_TaskProject_Model CreateNew()
        {
            TaskProject TaskProject = new TaskProjectBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_TaskProject_Model Default_Form_TaskProject_Model = this.ConverTo_Default_Form_TaskProject_Model(TaskProject);
            return Default_Form_TaskProject_Model;
        } 

		public virtual List<Default_Form_TaskProject_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TaskProjectBLO entityBLO = new TaskProjectBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<TaskProject> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_TaskProject_Model> ls_models = new List<Default_Form_TaskProject_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_TaskProject_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_TaskProject_ModelBLM : BaseDefault_Form_TaskProject_ModelBLM
	{
		public Default_Form_TaskProject_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
