//modelType = Default_Project_Details_Model

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
	public partial class BaseDefault_Project_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Project_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Project ConverTo_Project(Default_Project_Details_Model Default_Project_Details_Model)
        {
			Project Project = null;
            if (Default_Project_Details_Model.Id != 0)
            {
                Project = new ProjectBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Project_Details_Model.Id);
            }
            else
            {
                Project = new Project();
            } 
			Project.Name = Default_Project_Details_Model.Name;
			Project.Description = Default_Project_Details_Model.Description;
			Project.StartDate = DefaultDateTime_If_Empty(Default_Project_Details_Model.StartDate);
			Project.EndtDate = DefaultDateTime_If_Empty(Default_Project_Details_Model.EndtDate);
			Project.isPublic = Default_Project_Details_Model.isPublic;
			Project.Id = Default_Project_Details_Model.Id;
            return Project;
        }
        public virtual Default_Project_Details_Model ConverTo_Default_Project_Details_Model(Project Project)
        {  
			Default_Project_Details_Model Default_Project_Details_Model = new Default_Project_Details_Model();
			Default_Project_Details_Model.toStringValue = Project.ToString();
			Default_Project_Details_Model.Name = Project.Name;
			Default_Project_Details_Model.Description = Project.Description;
			Default_Project_Details_Model.StartDate = DefaultDateTime_If_Empty(Project.StartDate);
			Default_Project_Details_Model.EndtDate = DefaultDateTime_If_Empty(Project.EndtDate);
			Default_Project_Details_Model.isPublic = Project.isPublic;
			Default_Project_Details_Model.Id = Project.Id;
            return Default_Project_Details_Model;            
        }

		public virtual Default_Project_Details_Model CreateNew()
        {
            Project Project = new ProjectBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Project_Details_Model Default_Project_Details_Model = this.ConverTo_Default_Project_Details_Model(Project);
            return Default_Project_Details_Model;
        } 

		public virtual List<Default_Project_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ProjectBLO entityBLO = new ProjectBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Project> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Project_Details_Model> ls_models = new List<Default_Project_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Project_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Project_Details_ModelBLM : BaseDefault_Project_Details_Model_BLM
	{
		public Default_Project_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
