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
	public partial class BaseDefault_ClassroomCategoryFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ClassroomCategoryFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ClassroomCategory ConverTo_ClassroomCategory(Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView)
        {
			ClassroomCategory ClassroomCategory = null;
            if (Default_ClassroomCategoryFormView.Id != 0)
            {
                ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork).FindBaseEntityByID(Default_ClassroomCategoryFormView.Id);
            }
            else
            {
                ClassroomCategory = new ClassroomCategory();
            } 
			ClassroomCategory.Code = Default_ClassroomCategoryFormView.Code;
			ClassroomCategory.Name = Default_ClassroomCategoryFormView.Name;
			ClassroomCategory.Description = Default_ClassroomCategoryFormView.Description;
			ClassroomCategory.Id = Default_ClassroomCategoryFormView.Id;
            return ClassroomCategory;
        }
        public virtual Default_ClassroomCategoryFormView ConverTo_Default_ClassroomCategoryFormView(ClassroomCategory ClassroomCategory)
        {  
			Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormView();
			Default_ClassroomCategoryFormView.toStringValue = ClassroomCategory.ToString();
			Default_ClassroomCategoryFormView.Code = ClassroomCategory.Code;
			Default_ClassroomCategoryFormView.Name = ClassroomCategory.Name;
			Default_ClassroomCategoryFormView.Description = ClassroomCategory.Description;
			Default_ClassroomCategoryFormView.Id = ClassroomCategory.Id;
            return Default_ClassroomCategoryFormView;            
        }

		public virtual Default_ClassroomCategoryFormView CreateNew()
        {
            ClassroomCategory ClassroomCategory = new ClassroomCategory();
            Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = this.ConverTo_Default_ClassroomCategoryFormView(ClassroomCategory);
            return Default_ClassroomCategoryFormView;
        } 
    }

	public partial class Default_ClassroomCategoryFormViewBLM : BaseDefault_ClassroomCategoryFormViewBLM
	{
		public Default_ClassroomCategoryFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
