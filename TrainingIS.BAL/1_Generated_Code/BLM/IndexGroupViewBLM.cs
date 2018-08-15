﻿using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseIndexGroupViewBLM : BaseModelBLM
    {
        
        public BaseIndexGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Group ConverTo_Group(IndexGroupView IndexGroupView)
        {
			Group Group = null;
            if (IndexGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork).FindBaseEntityByID(IndexGroupView.Id);
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
            Group Group = new Group();
            IndexGroupView IndexGroupView = this.ConverTo_IndexGroupView(Group);
            return IndexGroupView;
        } 
    }

	public partial class IndexGroupViewBLM : BaseIndexGroupViewBLM
	{
		public IndexGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}