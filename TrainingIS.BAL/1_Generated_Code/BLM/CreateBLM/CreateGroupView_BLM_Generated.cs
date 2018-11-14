//modelType = CreateGroupView

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
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseCreateGroupView_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Form_Group_ModelBLM Form_Group_ModelBLM {set;get;}
        
		public BaseCreateGroupView_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Form_Group_ModelBLM = new Form_Group_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Group ConverTo_Group(CreateGroupView CreateGroupView)
        {
            var Group = Form_Group_ModelBLM.ConverTo_Group(CreateGroupView);
            return Group;
        }

		public virtual CreateGroupView ConverTo_CreateGroupView(Group Group)
        {
            CreateGroupView CreateGroupView = new CreateGroupView();
            Form_Group_ModelBLM.ConverTo_Default_Form_Group_Model(CreateGroupView, Group);
            return CreateGroupView;            
        }

		public virtual CreateGroupView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            CreateGroupView CreateGroupView = this.ConverTo_CreateGroupView(Group);
            return CreateGroupView;
        } 

		public virtual List<CreateGroupView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<CreateGroupView> ls_models = new List<CreateGroupView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_CreateGroupView(entity));
            }
            return ls_models;
        }


    }

	public partial class CreateGroupViewBLM : BaseCreateGroupView_BLM
	{
		public CreateGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
