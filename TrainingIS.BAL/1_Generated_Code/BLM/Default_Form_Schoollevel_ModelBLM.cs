﻿using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Schoollevel_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Form_Schoollevel_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Schoollevel ConverTo_Schoollevel(Default_Form_Schoollevel_Model Default_Form_Schoollevel_Model)
        {
			Schoollevel Schoollevel = null;
            if (Default_Form_Schoollevel_Model.Id != 0)
            {
                Schoollevel = new SchoollevelBLO(this.UnitOfWork).FindBaseEntityByID(Default_Form_Schoollevel_Model.Id);
            }
            else
            {
                Schoollevel = new Schoollevel();
            } 
			Schoollevel.Code = Default_Form_Schoollevel_Model.Code;
			Schoollevel.Name = Default_Form_Schoollevel_Model.Name;
			Schoollevel.Description = Default_Form_Schoollevel_Model.Description;
			Schoollevel.Id = Default_Form_Schoollevel_Model.Id;
            return Schoollevel;
        }
        public virtual Default_Form_Schoollevel_Model ConverTo_Default_Form_Schoollevel_Model(Schoollevel Schoollevel)
        {  
			Default_Form_Schoollevel_Model Default_Form_Schoollevel_Model = new Default_Form_Schoollevel_Model();
			Default_Form_Schoollevel_Model.toStringValue = Schoollevel.ToString();
			Default_Form_Schoollevel_Model.Code = Schoollevel.Code;
			Default_Form_Schoollevel_Model.Name = Schoollevel.Name;
			Default_Form_Schoollevel_Model.Description = Schoollevel.Description;
			Default_Form_Schoollevel_Model.Id = Schoollevel.Id;
            return Default_Form_Schoollevel_Model;            
        }

		public virtual Default_Form_Schoollevel_Model CreateNew()
        {
            Schoollevel Schoollevel = new Schoollevel();
            Default_Form_Schoollevel_Model Default_Form_Schoollevel_Model = this.ConverTo_Default_Form_Schoollevel_Model(Schoollevel);
            return Default_Form_Schoollevel_Model;
        } 
    }

	public partial class Default_Form_Schoollevel_ModelBLM : BaseDefault_Form_Schoollevel_ModelBLM
	{
		public Default_Form_Schoollevel_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
