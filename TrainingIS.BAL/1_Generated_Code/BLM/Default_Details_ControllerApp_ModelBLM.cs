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
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_ControllerApp_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_ControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ControllerApp ConverTo_ControllerApp(Default_Details_ControllerApp_Model Default_Details_ControllerApp_Model)
        {
			ControllerApp ControllerApp = null;
            if (Default_Details_ControllerApp_Model.Id != 0)
            {
                ControllerApp = new ControllerAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_ControllerApp_Model.Id);
            }
            else
            {
                ControllerApp = new ControllerApp();
            } 
			ControllerApp.Code = Default_Details_ControllerApp_Model.Code;
			ControllerApp.Name = Default_Details_ControllerApp_Model.Name;
			ControllerApp.Description = Default_Details_ControllerApp_Model.Description;
			ControllerApp.Id = Default_Details_ControllerApp_Model.Id;
            return ControllerApp;
        }
        public virtual Default_Details_ControllerApp_Model ConverTo_Default_Details_ControllerApp_Model(ControllerApp ControllerApp)
        {  
			Default_Details_ControllerApp_Model Default_Details_ControllerApp_Model = new Default_Details_ControllerApp_Model();
			Default_Details_ControllerApp_Model.toStringValue = ControllerApp.ToString();
			Default_Details_ControllerApp_Model.Code = ControllerApp.Code;
			Default_Details_ControllerApp_Model.Name = ControllerApp.Name;
			Default_Details_ControllerApp_Model.Description = ControllerApp.Description;
			Default_Details_ControllerApp_Model.Id = ControllerApp.Id;
            return Default_Details_ControllerApp_Model;            
        }

		public virtual Default_Details_ControllerApp_Model CreateNew()
        {
            ControllerApp ControllerApp = new ControllerApp();
            Default_Details_ControllerApp_Model Default_Details_ControllerApp_Model = this.ConverTo_Default_Details_ControllerApp_Model(ControllerApp);
            return Default_Details_ControllerApp_Model;
        } 
    }

	public partial class Default_Details_ControllerApp_ModelBLM : BaseDefault_Details_ControllerApp_ModelBLM
	{
		public Default_Details_ControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
