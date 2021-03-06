﻿//modelType = Default_Form_ClassroomCategory_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_ClassroomCategory_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ClassroomCategory_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ClassroomCategory ConverTo_ClassroomCategory(Default_Form_ClassroomCategory_Model Default_Form_ClassroomCategory_Model)
        {
			ClassroomCategory ClassroomCategory = null;
            if (Default_Form_ClassroomCategory_Model.Id != 0)
            {
                ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ClassroomCategory_Model.Id);
            }
            else
            {
                ClassroomCategory = new ClassroomCategory();
            } 
			ClassroomCategory.Code = Default_Form_ClassroomCategory_Model.Code;
			ClassroomCategory.Name = Default_Form_ClassroomCategory_Model.Name;
			ClassroomCategory.Description = Default_Form_ClassroomCategory_Model.Description;
			ClassroomCategory.Reference = Default_Form_ClassroomCategory_Model.Reference;
			ClassroomCategory.Id = Default_Form_ClassroomCategory_Model.Id;
            return ClassroomCategory;
        }
        public virtual void ConverTo_Default_Form_ClassroomCategory_Model(Default_Form_ClassroomCategory_Model Default_Form_ClassroomCategory_Model, ClassroomCategory ClassroomCategory)
        {  
			 
			Default_Form_ClassroomCategory_Model.toStringValue = ClassroomCategory.ToString();
			Default_Form_ClassroomCategory_Model.Code = ClassroomCategory.Code;
			Default_Form_ClassroomCategory_Model.Name = ClassroomCategory.Name;
			Default_Form_ClassroomCategory_Model.Description = ClassroomCategory.Description;
			Default_Form_ClassroomCategory_Model.Id = ClassroomCategory.Id;
			Default_Form_ClassroomCategory_Model.Reference = ClassroomCategory.Reference;
                     
        }

    }

	public partial class Default_Form_ClassroomCategory_ModelBLM : BaseDefault_Form_ClassroomCategory_Model_BLM
	{
		public Default_Form_ClassroomCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
