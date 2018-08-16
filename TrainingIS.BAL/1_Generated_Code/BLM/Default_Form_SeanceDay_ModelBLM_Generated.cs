﻿//modelType = Default_Form_SeanceDay_Model

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

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_SeanceDay_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_SeanceDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceDay ConverTo_SeanceDay(Default_Form_SeanceDay_Model Default_Form_SeanceDay_Model)
        {
			SeanceDay SeanceDay = null;
            if (Default_Form_SeanceDay_Model.Id != 0)
            {
                SeanceDay = new SeanceDayBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_SeanceDay_Model.Id);
            }
            else
            {
                SeanceDay = new SeanceDay();
            } 
			SeanceDay.Name = Default_Form_SeanceDay_Model.Name;
			SeanceDay.Code = Default_Form_SeanceDay_Model.Code;
			SeanceDay.Description = Default_Form_SeanceDay_Model.Description;
			SeanceDay.Id = Default_Form_SeanceDay_Model.Id;
            return SeanceDay;
        }
        public virtual Default_Form_SeanceDay_Model ConverTo_Default_Form_SeanceDay_Model(SeanceDay SeanceDay)
        {  
			Default_Form_SeanceDay_Model Default_Form_SeanceDay_Model = new Default_Form_SeanceDay_Model();
			Default_Form_SeanceDay_Model.toStringValue = SeanceDay.ToString();
			Default_Form_SeanceDay_Model.Name = SeanceDay.Name;
			Default_Form_SeanceDay_Model.Code = SeanceDay.Code;
			Default_Form_SeanceDay_Model.Description = SeanceDay.Description;
			Default_Form_SeanceDay_Model.Id = SeanceDay.Id;
            return Default_Form_SeanceDay_Model;            
        }

		public virtual Default_Form_SeanceDay_Model CreateNew()
        {
            SeanceDay SeanceDay = new SeanceDay();
            Default_Form_SeanceDay_Model Default_Form_SeanceDay_Model = this.ConverTo_Default_Form_SeanceDay_Model(SeanceDay);
            return Default_Form_SeanceDay_Model;
        } 
    }

	public partial class Default_Form_SeanceDay_ModelBLM : BaseDefault_Form_SeanceDay_ModelBLM
	{
		public Default_Form_SeanceDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
