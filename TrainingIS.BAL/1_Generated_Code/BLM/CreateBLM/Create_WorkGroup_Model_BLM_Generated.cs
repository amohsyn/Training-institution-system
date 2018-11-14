//modelType = Create_WorkGroup_Model

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
using TrainingIS.Models.WorkGroups;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseCreate_WorkGroup_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Form_WorkGroup_ModelBLM Form_WorkGroup_ModelBLM {set;get;}
        
		public BaseCreate_WorkGroup_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Form_WorkGroup_ModelBLM = new Form_WorkGroup_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual WorkGroup ConverTo_WorkGroup(Create_WorkGroup_Model Create_WorkGroup_Model)
        {
            var WorkGroup = Form_WorkGroup_ModelBLM.ConverTo_WorkGroup(Create_WorkGroup_Model);
            return WorkGroup;
        }

		public virtual Create_WorkGroup_Model ConverTo_Create_WorkGroup_Model(WorkGroup WorkGroup)
        {
            Create_WorkGroup_Model Create_WorkGroup_Model = new Create_WorkGroup_Model();
            Form_WorkGroup_ModelBLM.ConverTo_Form_WorkGroup_Model(Create_WorkGroup_Model, WorkGroup);
            return Create_WorkGroup_Model;            
        }

		public virtual Create_WorkGroup_Model CreateNew()
        {
            WorkGroup WorkGroup = new WorkGroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Create_WorkGroup_Model Create_WorkGroup_Model = this.ConverTo_Create_WorkGroup_Model(WorkGroup);
            return Create_WorkGroup_Model;
        } 

		public virtual List<Create_WorkGroup_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            WorkGroupBLO entityBLO = new WorkGroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<WorkGroup> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Create_WorkGroup_Model> ls_models = new List<Create_WorkGroup_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Create_WorkGroup_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Create_WorkGroup_ModelBLM : BaseCreate_WorkGroup_Model_BLM
	{
		public Create_WorkGroup_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
