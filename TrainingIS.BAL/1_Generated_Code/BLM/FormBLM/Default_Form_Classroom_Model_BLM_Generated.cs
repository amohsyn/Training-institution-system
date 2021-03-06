﻿//modelType = Default_Form_Classroom_Model

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
	public partial class BaseDefault_Form_Classroom_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Classroom_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Classroom ConverTo_Classroom(Default_Form_Classroom_Model Default_Form_Classroom_Model)
        {
			Classroom Classroom = null;
            if (Default_Form_Classroom_Model.Id != 0)
            {
                Classroom = new ClassroomBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Classroom_Model.Id);
            }
            else
            {
                Classroom = new Classroom();
            } 
			Classroom.Code = Default_Form_Classroom_Model.Code;
			Classroom.Name = Default_Form_Classroom_Model.Name;
			Classroom.ClassroomCategoryId = Default_Form_Classroom_Model.ClassroomCategoryId;
			Classroom.ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Classroom_Model.ClassroomCategoryId)) ;
			Classroom.Description = Default_Form_Classroom_Model.Description;
			Classroom.Reference = Default_Form_Classroom_Model.Reference;
			Classroom.Id = Default_Form_Classroom_Model.Id;
            return Classroom;
        }
        public virtual void ConverTo_Default_Form_Classroom_Model(Default_Form_Classroom_Model Default_Form_Classroom_Model, Classroom Classroom)
        {  
			 
			Default_Form_Classroom_Model.toStringValue = Classroom.ToString();
			Default_Form_Classroom_Model.Code = Classroom.Code;
			Default_Form_Classroom_Model.Name = Classroom.Name;
			Default_Form_Classroom_Model.ClassroomCategoryId = Classroom.ClassroomCategoryId;
			Default_Form_Classroom_Model.Description = Classroom.Description;
			Default_Form_Classroom_Model.Id = Classroom.Id;
			Default_Form_Classroom_Model.Reference = Classroom.Reference;
                     
        }

    }

	public partial class Default_Form_Classroom_ModelBLM : BaseDefault_Form_Classroom_Model_BLM
	{
		public Default_Form_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
