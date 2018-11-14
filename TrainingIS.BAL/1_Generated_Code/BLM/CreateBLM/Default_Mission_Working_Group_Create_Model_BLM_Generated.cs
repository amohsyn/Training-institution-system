//modelType = Default_Mission_Working_Group_Create_Model

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
	public partial class BaseDefault_Mission_Working_Group_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Default_Form_Mission_Working_Group_ModelBLM Default_Form_Mission_Working_Group_ModelBLM {set;get;}
        
		public BaseDefault_Mission_Working_Group_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_Mission_Working_Group_ModelBLM = new Default_Form_Mission_Working_Group_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Mission_Working_Group ConverTo_Mission_Working_Group(Default_Mission_Working_Group_Create_Model Default_Mission_Working_Group_Create_Model)
        {
            var Mission_Working_Group = Default_Form_Mission_Working_Group_ModelBLM.ConverTo_Mission_Working_Group(Default_Mission_Working_Group_Create_Model);
            return Mission_Working_Group;
        }

		public virtual Default_Mission_Working_Group_Create_Model ConverTo_Default_Mission_Working_Group_Create_Model(Mission_Working_Group Mission_Working_Group)
        {
            Default_Mission_Working_Group_Create_Model Default_Mission_Working_Group_Create_Model = new Default_Mission_Working_Group_Create_Model();
            Default_Form_Mission_Working_Group_ModelBLM.ConverTo_Default_Form_Mission_Working_Group_Model(Default_Mission_Working_Group_Create_Model, Mission_Working_Group);
            return Default_Mission_Working_Group_Create_Model;            
        }

		public virtual Default_Mission_Working_Group_Create_Model CreateNew()
        {
            Mission_Working_Group Mission_Working_Group = new Mission_Working_GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Mission_Working_Group_Create_Model Default_Mission_Working_Group_Create_Model = this.ConverTo_Default_Mission_Working_Group_Create_Model(Mission_Working_Group);
            return Default_Mission_Working_Group_Create_Model;
        } 

		public virtual List<Default_Mission_Working_Group_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Mission_Working_GroupBLO entityBLO = new Mission_Working_GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Mission_Working_Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Mission_Working_Group_Create_Model> ls_models = new List<Default_Mission_Working_Group_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Mission_Working_Group_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Mission_Working_Group_Create_ModelBLM : BaseDefault_Mission_Working_Group_Create_Model_BLM
	{
		public Default_Mission_Working_Group_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
