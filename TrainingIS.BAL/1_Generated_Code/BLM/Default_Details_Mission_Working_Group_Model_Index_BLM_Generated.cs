//modelType = Default_Details_Mission_Working_Group_Model

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
	public partial class BaseDefault_Details_Mission_Working_Group_Model_Index_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Mission_Working_Group_Model_Index_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Mission_Working_Group ConverTo_Mission_Working_Group(Default_Details_Mission_Working_Group_Model Default_Details_Mission_Working_Group_Model)
        {
			Mission_Working_Group Mission_Working_Group = null;
            if (Default_Details_Mission_Working_Group_Model.Id != 0)
            {
                Mission_Working_Group = new Mission_Working_GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Mission_Working_Group_Model.Id);
            }
            else
            {
                Mission_Working_Group = new Mission_Working_Group();
            } 
			Mission_Working_Group.Code = Default_Details_Mission_Working_Group_Model.Code;
			Mission_Working_Group.Name = Default_Details_Mission_Working_Group_Model.Name;
			Mission_Working_Group.DecisionAuthority = Default_Details_Mission_Working_Group_Model.DecisionAuthority;
			Mission_Working_Group.Description = Default_Details_Mission_Working_Group_Model.Description;
			Mission_Working_Group.Id = Default_Details_Mission_Working_Group_Model.Id;
            return Mission_Working_Group;
        }
        public virtual Default_Details_Mission_Working_Group_Model ConverTo_Default_Details_Mission_Working_Group_Model(Mission_Working_Group Mission_Working_Group)
        {  
			Default_Details_Mission_Working_Group_Model Default_Details_Mission_Working_Group_Model = new Default_Details_Mission_Working_Group_Model();
			Default_Details_Mission_Working_Group_Model.toStringValue = Mission_Working_Group.ToString();
			Default_Details_Mission_Working_Group_Model.Code = Mission_Working_Group.Code;
			Default_Details_Mission_Working_Group_Model.Name = Mission_Working_Group.Name;
			Default_Details_Mission_Working_Group_Model.DecisionAuthority = Mission_Working_Group.DecisionAuthority;
			Default_Details_Mission_Working_Group_Model.Description = Mission_Working_Group.Description;
			Default_Details_Mission_Working_Group_Model.Id = Mission_Working_Group.Id;
            return Default_Details_Mission_Working_Group_Model;            
        }

		public virtual Default_Details_Mission_Working_Group_Model CreateNew()
        {
            Mission_Working_Group Mission_Working_Group = new Mission_Working_GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Mission_Working_Group_Model Default_Details_Mission_Working_Group_Model = this.ConverTo_Default_Details_Mission_Working_Group_Model(Mission_Working_Group);
            return Default_Details_Mission_Working_Group_Model;
        } 

		public virtual List<Default_Details_Mission_Working_Group_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Mission_Working_GroupBLO entityBLO = new Mission_Working_GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Mission_Working_Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Mission_Working_Group_Model> ls_models = new List<Default_Details_Mission_Working_Group_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Mission_Working_Group_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Mission_Working_Group_ModelBLM : BaseDefault_Details_Mission_Working_Group_Model_Index_BLM
	{
		public Default_Details_Mission_Working_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
