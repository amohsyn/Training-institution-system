﻿//modelType = Default_Form_ActionControllerApp_Model

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
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_ActionControllerApp_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ActionControllerApp_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ActionControllerApp ConverTo_ActionControllerApp(Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model)
        {
			ActionControllerApp ActionControllerApp = null;
            if (Default_Form_ActionControllerApp_Model.Id != 0)
            {
                ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ActionControllerApp_Model.Id);
            }
            else
            {
                ActionControllerApp = new ActionControllerApp();
            } 
			ActionControllerApp.Code = Default_Form_ActionControllerApp_Model.Code;
			ActionControllerApp.Name = Default_Form_ActionControllerApp_Model.Name;
			ActionControllerApp.Description = Default_Form_ActionControllerApp_Model.Description;
			ActionControllerApp.ControllerAppId = Default_Form_ActionControllerApp_Model.ControllerAppId;
			ActionControllerApp.ControllerApp = new ControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_ActionControllerApp_Model.ControllerAppId)) ;
			ActionControllerApp.Reference = Default_Form_ActionControllerApp_Model.Reference;
			ActionControllerApp.Id = Default_Form_ActionControllerApp_Model.Id;
            return ActionControllerApp;
        }
        public virtual void ConverTo_Default_Form_ActionControllerApp_Model(Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model, ActionControllerApp ActionControllerApp)
        {  
			 
			Default_Form_ActionControllerApp_Model.toStringValue = ActionControllerApp.ToString();
			Default_Form_ActionControllerApp_Model.Code = ActionControllerApp.Code;
			Default_Form_ActionControllerApp_Model.Name = ActionControllerApp.Name;
			Default_Form_ActionControllerApp_Model.Description = ActionControllerApp.Description;
			Default_Form_ActionControllerApp_Model.ControllerAppId = ActionControllerApp.ControllerAppId;
			Default_Form_ActionControllerApp_Model.Id = ActionControllerApp.Id;
			Default_Form_ActionControllerApp_Model.Reference = ActionControllerApp.Reference;
                     
        }

    }

	public partial class Default_Form_ActionControllerApp_ModelBLM : BaseDefault_Form_ActionControllerApp_Model_BLM
	{
		public Default_Form_ActionControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
