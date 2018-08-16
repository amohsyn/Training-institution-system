﻿//modelType = Default_Details_RoleApp_Model

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

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_RoleApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_RoleApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual RoleApp ConverTo_RoleApp(Default_Details_RoleApp_Model Default_Details_RoleApp_Model)
        {
			RoleApp RoleApp = null;
            if (Default_Details_RoleApp_Model.Id != 0)
            {
                RoleApp = new RoleAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_RoleApp_Model.Id);
            }
            else
            {
                RoleApp = new RoleApp();
            } 
			RoleApp.Code = Default_Details_RoleApp_Model.Code;
			RoleApp.Description = Default_Details_RoleApp_Model.Description;
			RoleApp.Id = Default_Details_RoleApp_Model.Id;
            return RoleApp;
        }
        public virtual Default_Details_RoleApp_Model ConverTo_Default_Details_RoleApp_Model(RoleApp RoleApp)
        {  
			Default_Details_RoleApp_Model Default_Details_RoleApp_Model = new Default_Details_RoleApp_Model();
			Default_Details_RoleApp_Model.toStringValue = RoleApp.ToString();
			Default_Details_RoleApp_Model.Code = RoleApp.Code;
			Default_Details_RoleApp_Model.Description = RoleApp.Description;
			Default_Details_RoleApp_Model.Id = RoleApp.Id;
            return Default_Details_RoleApp_Model;            
        }

		public virtual Default_Details_RoleApp_Model CreateNew()
        {
            RoleApp RoleApp = new RoleApp();
            Default_Details_RoleApp_Model Default_Details_RoleApp_Model = this.ConverTo_Default_Details_RoleApp_Model(RoleApp);
            return Default_Details_RoleApp_Model;
        } 
    }

	public partial class Default_Details_RoleApp_ModelBLM : BaseDefault_Details_RoleApp_ModelBLM
	{
		public Default_Details_RoleApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
