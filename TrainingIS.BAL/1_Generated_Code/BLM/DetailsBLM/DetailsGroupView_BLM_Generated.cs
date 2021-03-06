﻿//modelType = DetailsGroupView

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
	public partial class BaseDetailsGroupView_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDetailsGroupView_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(DetailsGroupView DetailsGroupView)
        {
			Group Group = null;
            if (DetailsGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(DetailsGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.Code = DetailsGroupView.Code;
			Group.YearStudy = DetailsGroupView.YearStudy;
			Group.Specialty = DetailsGroupView.Specialty;
			Group.TrainingType = DetailsGroupView.TrainingType;
			Group.Id = DetailsGroupView.Id;
            return Group;
        }
        public virtual DetailsGroupView ConverTo_DetailsGroupView(Group Group)
        {  
			DetailsGroupView DetailsGroupView = new DetailsGroupView();
			DetailsGroupView.toStringValue = Group.ToString();
			DetailsGroupView.TrainingType = Group.TrainingType;
			DetailsGroupView.Specialty = Group.Specialty;
			DetailsGroupView.YearStudy = Group.YearStudy;
			DetailsGroupView.Code = Group.Code;
			DetailsGroupView.Id = Group.Id;
            return DetailsGroupView;            
        }

		public virtual DetailsGroupView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            DetailsGroupView DetailsGroupView = this.ConverTo_DetailsGroupView(Group);
            return DetailsGroupView;
        } 

		public virtual List<DetailsGroupView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<DetailsGroupView> ls_models = new List<DetailsGroupView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_DetailsGroupView(entity));
            }
            return ls_models;
        }


    }

	public partial class DetailsGroupViewBLM : BaseDetailsGroupView_BLM
	{
		public DetailsGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
