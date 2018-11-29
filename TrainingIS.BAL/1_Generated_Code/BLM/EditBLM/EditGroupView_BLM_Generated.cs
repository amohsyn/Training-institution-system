//modelType = EditGroupView

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
	public partial class BaseEditGroupView_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Form_Group_ModelBLM Form_Group_ModelBLM {set;get;}
        
		public BaseEditGroupView_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Form_Group_ModelBLM = new Form_Group_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Group ConverTo_Group(EditGroupView EditGroupView)
        {
            var Group = Form_Group_ModelBLM.ConverTo_Group(EditGroupView);
            return Group;
        }

		public virtual EditGroupView ConverTo_EditGroupView(Group Group)
        {
            EditGroupView EditGroupView = new EditGroupView();
            Form_Group_ModelBLM.ConverTo_Form_Group_Model(EditGroupView, Group);
            return EditGroupView;            
        }

		public virtual EditGroupView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            EditGroupView EditGroupView = this.ConverTo_EditGroupView(Group);
            return EditGroupView;
        } 

		public virtual List<EditGroupView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<EditGroupView> ls_models = new List<EditGroupView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_EditGroupView(entity));
            }
            return ls_models;
        }


    }

	public partial class EditGroupViewBLM : BaseEditGroupView_BLM
	{
		public EditGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
