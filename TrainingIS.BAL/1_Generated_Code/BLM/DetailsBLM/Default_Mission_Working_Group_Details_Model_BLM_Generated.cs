//modelType = Default_Mission_Working_Group_Details_Model

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
	public partial class BaseDefault_Mission_Working_Group_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Mission_Working_Group_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Mission_Working_Group ConverTo_Mission_Working_Group(Default_Mission_Working_Group_Details_Model Default_Mission_Working_Group_Details_Model)
        {
			Mission_Working_Group Mission_Working_Group = null;
            if (Default_Mission_Working_Group_Details_Model.Id != 0)
            {
                Mission_Working_Group = new Mission_Working_GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Mission_Working_Group_Details_Model.Id);
            }
            else
            {
                Mission_Working_Group = new Mission_Working_Group();
            } 
			Mission_Working_Group.Code = Default_Mission_Working_Group_Details_Model.Code;
			Mission_Working_Group.Name = Default_Mission_Working_Group_Details_Model.Name;
			Mission_Working_Group.DecisionAuthority = Default_Mission_Working_Group_Details_Model.DecisionAuthority;
			Mission_Working_Group.Description = Default_Mission_Working_Group_Details_Model.Description;
			Mission_Working_Group.Id = Default_Mission_Working_Group_Details_Model.Id;
            return Mission_Working_Group;
        }
        public virtual Default_Mission_Working_Group_Details_Model ConverTo_Default_Mission_Working_Group_Details_Model(Mission_Working_Group Mission_Working_Group)
        {  
			Default_Mission_Working_Group_Details_Model Default_Mission_Working_Group_Details_Model = new Default_Mission_Working_Group_Details_Model();
			Default_Mission_Working_Group_Details_Model.toStringValue = Mission_Working_Group.ToString();
			Default_Mission_Working_Group_Details_Model.Code = Mission_Working_Group.Code;
			Default_Mission_Working_Group_Details_Model.Name = Mission_Working_Group.Name;
			Default_Mission_Working_Group_Details_Model.DecisionAuthority = Mission_Working_Group.DecisionAuthority;
			Default_Mission_Working_Group_Details_Model.Description = Mission_Working_Group.Description;
			Default_Mission_Working_Group_Details_Model.Id = Mission_Working_Group.Id;
            return Default_Mission_Working_Group_Details_Model;            
        }

		public virtual Default_Mission_Working_Group_Details_Model CreateNew()
        {
            Mission_Working_Group Mission_Working_Group = new Mission_Working_GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Mission_Working_Group_Details_Model Default_Mission_Working_Group_Details_Model = this.ConverTo_Default_Mission_Working_Group_Details_Model(Mission_Working_Group);
            return Default_Mission_Working_Group_Details_Model;
        } 

		public virtual List<Default_Mission_Working_Group_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Mission_Working_GroupBLO entityBLO = new Mission_Working_GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Mission_Working_Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Mission_Working_Group_Details_Model> ls_models = new List<Default_Mission_Working_Group_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Mission_Working_Group_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Mission_Working_Group_Details_ModelBLM : BaseDefault_Mission_Working_Group_Details_Model_BLM
	{
		public Default_Mission_Working_Group_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
