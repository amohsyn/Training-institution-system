//modelType = TrainingFormView

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
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseTrainingFormViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseTrainingFormViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(TrainingFormView TrainingFormView)
        {
			Group Group = null;
            if (TrainingFormView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(TrainingFormView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingYearId = TrainingFormView.TrainingYearId;
			Group.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(TrainingFormView.TrainingYearId)) ;
			Group.SpecialtyId = TrainingFormView.SpecialtyId;
			Group.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(TrainingFormView.SpecialtyId)) ;
			Group.Code = TrainingFormView.Code;
			Group.Description = TrainingFormView.Description;
			Group.Id = TrainingFormView.Id;
            return Group;
        }
        public virtual TrainingFormView ConverTo_TrainingFormView(Group Group)
        {  
			TrainingFormView TrainingFormView = new TrainingFormView();
			TrainingFormView.toStringValue = Group.ToString();
			TrainingFormView.TrainingYearId = Group.TrainingYearId;
			TrainingFormView.SpecialtyId = Group.SpecialtyId;
			TrainingFormView.Code = Group.Code;
			TrainingFormView.Description = Group.Description;
			TrainingFormView.Id = Group.Id;
            return TrainingFormView;            
        }

		public virtual TrainingFormView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            TrainingFormView TrainingFormView = this.ConverTo_TrainingFormView(Group);
            return TrainingFormView;
        } 

		public virtual List<TrainingFormView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<TrainingFormView> ls_models = new List<TrainingFormView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_TrainingFormView(entity));
            }
            return ls_models;
        }


    }

	public partial class TrainingFormViewBLM : BaseTrainingFormViewBLM
	{
		public TrainingFormViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
