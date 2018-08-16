//modelType = Default_Details_ActionControllerApp_Model

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
	public partial class BaseDefault_Details_ActionControllerApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_ActionControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ActionControllerApp ConverTo_ActionControllerApp(Default_Details_ActionControllerApp_Model Default_Details_ActionControllerApp_Model)
        {
			ActionControllerApp ActionControllerApp = null;
            if (Default_Details_ActionControllerApp_Model.Id != 0)
            {
                ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_ActionControllerApp_Model.Id);
            }
            else
            {
                ActionControllerApp = new ActionControllerApp();
            } 
			ActionControllerApp.Code = Default_Details_ActionControllerApp_Model.Code;
			ActionControllerApp.Name = Default_Details_ActionControllerApp_Model.Name;
			ActionControllerApp.Description = Default_Details_ActionControllerApp_Model.Description;
			ActionControllerApp.ControllerApp = Default_Details_ActionControllerApp_Model.ControllerApp;
			ActionControllerApp.Id = Default_Details_ActionControllerApp_Model.Id;
            return ActionControllerApp;
        }
        public virtual Default_Details_ActionControllerApp_Model ConverTo_Default_Details_ActionControllerApp_Model(ActionControllerApp ActionControllerApp)
        {  
			Default_Details_ActionControllerApp_Model Default_Details_ActionControllerApp_Model = new Default_Details_ActionControllerApp_Model();
			Default_Details_ActionControllerApp_Model.toStringValue = ActionControllerApp.ToString();
			Default_Details_ActionControllerApp_Model.Code = ActionControllerApp.Code;
			Default_Details_ActionControllerApp_Model.Name = ActionControllerApp.Name;
			Default_Details_ActionControllerApp_Model.Description = ActionControllerApp.Description;
			Default_Details_ActionControllerApp_Model.ControllerApp = ActionControllerApp.ControllerApp;
			Default_Details_ActionControllerApp_Model.Id = ActionControllerApp.Id;
            return Default_Details_ActionControllerApp_Model;            
        }

		public virtual Default_Details_ActionControllerApp_Model CreateNew()
        {
            ActionControllerApp ActionControllerApp = new ActionControllerApp();
            Default_Details_ActionControllerApp_Model Default_Details_ActionControllerApp_Model = this.ConverTo_Default_Details_ActionControllerApp_Model(ActionControllerApp);
            return Default_Details_ActionControllerApp_Model;
        } 
    }

	public partial class Default_Details_ActionControllerApp_ModelBLM : BaseDefault_Details_ActionControllerApp_ModelBLM
	{
		public Default_Details_ActionControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
