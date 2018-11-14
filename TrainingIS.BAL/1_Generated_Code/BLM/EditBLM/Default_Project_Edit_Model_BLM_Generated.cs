//modelType = Default_Project_Edit_Model

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
	public partial class BaseDefault_Project_Edit_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Default_Form_Project_ModelBLM Default_Form_Project_ModelBLM {set;get;}
        
		public BaseDefault_Project_Edit_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_Project_ModelBLM = new Default_Form_Project_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Project ConverTo_Project(Default_Project_Edit_Model Default_Project_Edit_Model)
        {
            var Project = Default_Form_Project_ModelBLM.ConverTo_Project(Default_Project_Edit_Model);
            return Project;
        }

		public virtual Default_Project_Edit_Model ConverTo_Default_Project_Edit_Model(Project Project)
        {
            Default_Project_Edit_Model Default_Project_Edit_Model = new Default_Project_Edit_Model();
            Default_Form_Project_ModelBLM.ConverTo_Default_Form_Project_Model(Default_Project_Edit_Model, Project);
            return Default_Project_Edit_Model;            
        }

		public virtual Default_Project_Edit_Model CreateNew()
        {
            Project Project = new ProjectBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Project_Edit_Model Default_Project_Edit_Model = this.ConverTo_Default_Project_Edit_Model(Project);
            return Default_Project_Edit_Model;
        } 

		public virtual List<Default_Project_Edit_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ProjectBLO entityBLO = new ProjectBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Project> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Project_Edit_Model> ls_models = new List<Default_Project_Edit_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Project_Edit_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Project_Edit_ModelBLM : BaseDefault_Project_Edit_Model_BLM
	{
		public Default_Project_Edit_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
