﻿//modelType = Default_Form_FormerSpecialty_Model

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
	public partial class BaseDefault_Form_FormerSpecialty_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_FormerSpecialty_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual FormerSpecialty ConverTo_FormerSpecialty(Default_Form_FormerSpecialty_Model Default_Form_FormerSpecialty_Model)
        {
			FormerSpecialty FormerSpecialty = null;
            if (Default_Form_FormerSpecialty_Model.Id != 0)
            {
                FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_FormerSpecialty_Model.Id);
            }
            else
            {
                FormerSpecialty = new FormerSpecialty();
            } 
			FormerSpecialty.Code = Default_Form_FormerSpecialty_Model.Code;
			FormerSpecialty.Name = Default_Form_FormerSpecialty_Model.Name;
			FormerSpecialty.Description = Default_Form_FormerSpecialty_Model.Description;
			FormerSpecialty.Reference = Default_Form_FormerSpecialty_Model.Reference;
			FormerSpecialty.Id = Default_Form_FormerSpecialty_Model.Id;
            return FormerSpecialty;
        }
        public virtual void ConverTo_Default_Form_FormerSpecialty_Model(Default_Form_FormerSpecialty_Model Default_Form_FormerSpecialty_Model, FormerSpecialty FormerSpecialty)
        {  
			 
			Default_Form_FormerSpecialty_Model.toStringValue = FormerSpecialty.ToString();
			Default_Form_FormerSpecialty_Model.Code = FormerSpecialty.Code;
			Default_Form_FormerSpecialty_Model.Name = FormerSpecialty.Name;
			Default_Form_FormerSpecialty_Model.Description = FormerSpecialty.Description;
			Default_Form_FormerSpecialty_Model.Id = FormerSpecialty.Id;
			Default_Form_FormerSpecialty_Model.Reference = FormerSpecialty.Reference;
                     
        }

    }

	public partial class Default_Form_FormerSpecialty_ModelBLM : BaseDefault_Form_FormerSpecialty_Model_BLM
	{
		public Default_Form_FormerSpecialty_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
