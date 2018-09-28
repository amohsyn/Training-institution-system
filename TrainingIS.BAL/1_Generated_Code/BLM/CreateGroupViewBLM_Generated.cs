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
	public partial class BaseCreateGroupViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseCreateGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(CreateGroupView CreateGroupView)
        {
			Group Group = null;
            if (CreateGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(CreateGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingYear = CreateGroupView.TrainingYear;
			Group.TrainingYearId = CreateGroupView.TrainingYearId;
			Group.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(CreateGroupView.TrainingYearId)) ;
			Group.Specialty = CreateGroupView.Specialty;
			Group.SpecialtyId = CreateGroupView.SpecialtyId;
			Group.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(CreateGroupView.SpecialtyId)) ;
			Group.TrainingType = CreateGroupView.TrainingType;
			Group.TrainingTypeId = CreateGroupView.TrainingTypeId;
			Group.TrainingType = new TrainingTypeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(CreateGroupView.TrainingTypeId)) ;
			Group.YearStudy = CreateGroupView.YearStudy;
			Group.YearStudyId = CreateGroupView.YearStudyId;
			Group.YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(CreateGroupView.YearStudyId)) ;
			Group.Code = CreateGroupView.Code;
			Group.Id = CreateGroupView.Id;
            return Group;
        }
        public virtual CreateGroupView ConverTo_CreateGroupView(Group Group)
        {  
			CreateGroupView CreateGroupView = new CreateGroupView();
			CreateGroupView.toStringValue = Group.ToString();
			CreateGroupView.TrainingType = Group.TrainingType;
			CreateGroupView.TrainingTypeId = Group.TrainingTypeId;
			CreateGroupView.TrainingYear = Group.TrainingYear;
			CreateGroupView.TrainingYearId = Group.TrainingYearId;
			CreateGroupView.Specialty = Group.Specialty;
			CreateGroupView.SpecialtyId = Group.SpecialtyId;
			CreateGroupView.YearStudy = Group.YearStudy;
			CreateGroupView.YearStudyId = Group.YearStudyId;
			CreateGroupView.Code = Group.Code;
			CreateGroupView.Id = Group.Id;
            return CreateGroupView;            
        }

		public virtual CreateGroupView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            CreateGroupView CreateGroupView = this.ConverTo_CreateGroupView(Group);
            return CreateGroupView;
        } 

		public List<CreateGroupView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
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

	public partial class CreateGroupViewBLM : BaseCreateGroupViewBLM
	{
		public CreateGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
