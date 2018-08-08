﻿using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseCreateGroupViewBLM : ViewModelBLM
    {
        
        public BaseCreateGroupViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Group ConverTo_Group(CreateGroupView CreateGroupView)
        {
			Group Group = null;
            if (CreateGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork).FindBaseEntityByID(CreateGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingYear = CreateGroupView.TrainingYear;
			Group.TrainingYearId = CreateGroupView.TrainingYearId;
			Group.Specialty = CreateGroupView.Specialty;
			Group.SpecialtyId = CreateGroupView.SpecialtyId;
			Group.TrainingType = CreateGroupView.TrainingType;
			Group.TrainingTypeId = CreateGroupView.TrainingTypeId;
			Group.YearStudy = CreateGroupView.YearStudy;
			Group.YearStudyId = CreateGroupView.YearStudyId;
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
            Group Group = new Group();
            CreateGroupView CreateGroupView = this.ConverTo_CreateGroupView(Group);
            return CreateGroupView;
        } 
    }

	public partial class CreateGroupViewBLM : BaseCreateGroupViewBLM
	{
		public CreateGroupViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
