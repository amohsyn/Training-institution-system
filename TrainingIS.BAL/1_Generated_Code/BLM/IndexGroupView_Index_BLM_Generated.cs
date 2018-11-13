//modelType = IndexGroupView

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
	public partial class BaseIndexGroupView_Index_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndexGroupView_Index_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(IndexGroupView IndexGroupView)
        {
			Group Group = null;
            if (IndexGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(IndexGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.Code = IndexGroupView.Code;
			Group.YearStudy = IndexGroupView.YearStudy;
			Group.Specialty = IndexGroupView.Specialty;
			Group.TrainingType = IndexGroupView.TrainingType;
			Group.Id = IndexGroupView.Id;
            return Group;
        }
        public virtual IndexGroupView ConverTo_IndexGroupView(Group Group)
        {  
			IndexGroupView IndexGroupView = new IndexGroupView();
			IndexGroupView.toStringValue = Group.ToString();
			IndexGroupView.TrainingType = Group.TrainingType;
			IndexGroupView.Specialty = Group.Specialty;
			IndexGroupView.YearStudy = Group.YearStudy;
			IndexGroupView.Code = Group.Code;
			IndexGroupView.Id = Group.Id;
            return IndexGroupView;            
        }

		public virtual IndexGroupView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            IndexGroupView IndexGroupView = this.ConverTo_IndexGroupView(Group);
            return IndexGroupView;
        } 

		public virtual List<IndexGroupView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<IndexGroupView> ls_models = new List<IndexGroupView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_IndexGroupView(entity));
            }
            return ls_models;
        }


    }

	public partial class IndexGroupViewBLM : BaseIndexGroupView_Index_BLM
	{
		public IndexGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
