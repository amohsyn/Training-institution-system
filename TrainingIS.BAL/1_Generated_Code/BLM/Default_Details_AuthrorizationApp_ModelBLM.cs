using System;
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
	public partial class BaseDefault_Details_AuthrorizationApp_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_AuthrorizationApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(Default_Details_AuthrorizationApp_Model Default_Details_AuthrorizationApp_Model)
        {
			AuthrorizationApp AuthrorizationApp = null;
            if (Default_Details_AuthrorizationApp_Model.Id != 0)
            {
                AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_AuthrorizationApp_Model.Id);
            }
            else
            {
                AuthrorizationApp = new AuthrorizationApp();
            } 
			AuthrorizationApp.RoleApp = Default_Details_AuthrorizationApp_Model.RoleApp;
			AuthrorizationApp.ControllerApp = Default_Details_AuthrorizationApp_Model.ControllerApp;
			AuthrorizationApp.isAllAction = Default_Details_AuthrorizationApp_Model.isAllAction;
			AuthrorizationApp.ActionControllerApps = Default_Details_AuthrorizationApp_Model.ActionControllerApps;
			AuthrorizationApp.Id = Default_Details_AuthrorizationApp_Model.Id;
            return AuthrorizationApp;
        }
        public virtual Default_Details_AuthrorizationApp_Model ConverTo_Default_Details_AuthrorizationApp_Model(AuthrorizationApp AuthrorizationApp)
        {  
			Default_Details_AuthrorizationApp_Model Default_Details_AuthrorizationApp_Model = new Default_Details_AuthrorizationApp_Model();
			Default_Details_AuthrorizationApp_Model.toStringValue = AuthrorizationApp.ToString();
			Default_Details_AuthrorizationApp_Model.RoleApp = AuthrorizationApp.RoleApp;
			Default_Details_AuthrorizationApp_Model.ControllerApp = AuthrorizationApp.ControllerApp;
			Default_Details_AuthrorizationApp_Model.isAllAction = AuthrorizationApp.isAllAction;
			Default_Details_AuthrorizationApp_Model.ActionControllerApps = AuthrorizationApp.ActionControllerApps;
			Default_Details_AuthrorizationApp_Model.Id = AuthrorizationApp.Id;
            return Default_Details_AuthrorizationApp_Model;            
        }

		public virtual Default_Details_AuthrorizationApp_Model CreateNew()
        {
            AuthrorizationApp AuthrorizationApp = new AuthrorizationApp();
            Default_Details_AuthrorizationApp_Model Default_Details_AuthrorizationApp_Model = this.ConverTo_Default_Details_AuthrorizationApp_Model(AuthrorizationApp);
            return Default_Details_AuthrorizationApp_Model;
        } 
    }

	public partial class Default_Details_AuthrorizationApp_ModelBLM : BaseDefault_Details_AuthrorizationApp_ModelBLM
	{
		public Default_Details_AuthrorizationApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
