//modelType = Default_Form_RoleApp_Model

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
	public partial class BaseDefault_Form_RoleApp_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_RoleApp_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual RoleApp ConverTo_RoleApp(Default_Form_RoleApp_Model Default_Form_RoleApp_Model)
        {
			RoleApp RoleApp = null;
            if (Default_Form_RoleApp_Model.Id != 0)
            {
                RoleApp = new RoleAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_RoleApp_Model.Id);
            }
            else
            {
                RoleApp = new RoleApp();
            } 
			RoleApp.Code = Default_Form_RoleApp_Model.Code;
			RoleApp.Description = Default_Form_RoleApp_Model.Description;
			RoleApp.Reference = Default_Form_RoleApp_Model.Reference;
			RoleApp.Id = Default_Form_RoleApp_Model.Id;
            return RoleApp;
        }
        public virtual void ConverTo_Default_Form_RoleApp_Model(Default_Form_RoleApp_Model Default_Form_RoleApp_Model, RoleApp RoleApp)
        {  
			 
			Default_Form_RoleApp_Model.toStringValue = RoleApp.ToString();
			Default_Form_RoleApp_Model.Code = RoleApp.Code;
			Default_Form_RoleApp_Model.Description = RoleApp.Description;
			Default_Form_RoleApp_Model.Id = RoleApp.Id;
			Default_Form_RoleApp_Model.Reference = RoleApp.Reference;
                     
        }

    }

	public partial class Default_Form_RoleApp_ModelBLM : BaseDefault_Form_RoleApp_Model_BLM
	{
		public Default_Form_RoleApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
