﻿using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_GroupFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_GroupFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Group ConverTo_Group(Default_GroupFormView Default_GroupFormView)
        {
			Group Group = null;
            if (Default_GroupFormView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork).FindBaseEntityByID(Default_GroupFormView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingTypeId = Default_GroupFormView.TrainingTypeId;
			Group.TrainingYearId = Default_GroupFormView.TrainingYearId;
			Group.SpecialtyId = Default_GroupFormView.SpecialtyId;
			Group.YearStudyId = Default_GroupFormView.YearStudyId;
			Group.Code = Default_GroupFormView.Code;
			Group.Description = Default_GroupFormView.Description;
			Group.Id = Default_GroupFormView.Id;
            return Group;
        }
        public virtual Default_GroupFormView ConverTo_Default_GroupFormView(Group Group)
        {  
			Default_GroupFormView Default_GroupFormView = new Default_GroupFormView();
			Default_GroupFormView.toStringValue = Group.ToString();
			Default_GroupFormView.TrainingTypeId = Group.TrainingTypeId;
			Default_GroupFormView.TrainingYearId = Group.TrainingYearId;
			Default_GroupFormView.SpecialtyId = Group.SpecialtyId;
			Default_GroupFormView.YearStudyId = Group.YearStudyId;
			Default_GroupFormView.Code = Group.Code;
			Default_GroupFormView.Description = Group.Description;
			Default_GroupFormView.Id = Group.Id;
            return Default_GroupFormView;            
        }

		public virtual Default_GroupFormView CreateNew()
        {
            Group Group = new Group();
            Default_GroupFormView Default_GroupFormView = this.ConverTo_Default_GroupFormView(Group);
            return Default_GroupFormView;
        } 
    }

	public partial class Default_GroupFormViewBLM : BaseDefault_GroupFormViewBLM
	{
		public Default_GroupFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
