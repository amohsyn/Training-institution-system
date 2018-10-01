//modelType = Default_Details_Project_Model

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
	public partial class BaseDefault_Details_Project_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Project_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Project ConverTo_Project(Default_Details_Project_Model Default_Details_Project_Model)
        {
			Project Project = null;
            if (Default_Details_Project_Model.Id != 0)
            {
                Project = new ProjectBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Project_Model.Id);
            }
            else
            {
                Project = new Project();
            } 
			Project.Name = Default_Details_Project_Model.Name;
			Project.Description = Default_Details_Project_Model.Description;
			Project.StartDate = DefaultDateTime_If_Empty(Default_Details_Project_Model.StartDate);
			Project.EndtDate = DefaultDateTime_If_Empty(Default_Details_Project_Model.EndtDate);
			Project.isPublic = Default_Details_Project_Model.isPublic;
			Project.Id = Default_Details_Project_Model.Id;
            return Project;
        }
        public virtual Default_Details_Project_Model ConverTo_Default_Details_Project_Model(Project Project)
        {  
			Default_Details_Project_Model Default_Details_Project_Model = new Default_Details_Project_Model();
			Default_Details_Project_Model.toStringValue = Project.ToString();
			Default_Details_Project_Model.Name = Project.Name;
			Default_Details_Project_Model.Description = Project.Description;
			Default_Details_Project_Model.StartDate = DefaultDateTime_If_Empty(Project.StartDate);
			Default_Details_Project_Model.EndtDate = DefaultDateTime_If_Empty(Project.EndtDate);
			Default_Details_Project_Model.isPublic = Project.isPublic;
			Default_Details_Project_Model.Id = Project.Id;
            return Default_Details_Project_Model;            
        }

		public virtual Default_Details_Project_Model CreateNew()
        {
            Project Project = new ProjectBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Project_Model Default_Details_Project_Model = this.ConverTo_Default_Details_Project_Model(Project);
            return Default_Details_Project_Model;
        } 

		public List<Default_Details_Project_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ProjectBLO entityBLO = new ProjectBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Project> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Project_Model> ls_models = new List<Default_Details_Project_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Project_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Project_ModelBLM : BaseDefault_Details_Project_ModelBLM
	{
		public Default_Details_Project_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
