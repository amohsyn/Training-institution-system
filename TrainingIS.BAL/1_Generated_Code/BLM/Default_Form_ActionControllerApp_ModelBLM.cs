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
	public partial class BaseDefault_Form_ActionControllerApp_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Form_ActionControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ActionControllerApp ConverTo_ActionControllerApp(Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model)
        {
			ActionControllerApp ActionControllerApp = null;
            if (Default_Form_ActionControllerApp_Model.Id != 0)
            {
                ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_Form_ActionControllerApp_Model.Id);
            }
            else
            {
                ActionControllerApp = new ActionControllerApp();
            } 
			ActionControllerApp.Code = Default_Form_ActionControllerApp_Model.Code;
			ActionControllerApp.Name = Default_Form_ActionControllerApp_Model.Name;
			ActionControllerApp.Description = Default_Form_ActionControllerApp_Model.Description;
			ActionControllerApp.ControllerApp = Default_Form_ActionControllerApp_Model.ControllerApp;
			ActionControllerApp.Id = Default_Form_ActionControllerApp_Model.Id;
            return ActionControllerApp;
        }
        public virtual Default_Form_ActionControllerApp_Model ConverTo_Default_Form_ActionControllerApp_Model(ActionControllerApp ActionControllerApp)
        {  
			Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model = new Default_Form_ActionControllerApp_Model();
			Default_Form_ActionControllerApp_Model.toStringValue = ActionControllerApp.ToString();
			Default_Form_ActionControllerApp_Model.Code = ActionControllerApp.Code;
			Default_Form_ActionControllerApp_Model.Name = ActionControllerApp.Name;
			Default_Form_ActionControllerApp_Model.Description = ActionControllerApp.Description;
			Default_Form_ActionControllerApp_Model.ControllerApp = ActionControllerApp.ControllerApp;
			Default_Form_ActionControllerApp_Model.Id = ActionControllerApp.Id;
            return Default_Form_ActionControllerApp_Model;            
        }

		public virtual Default_Form_ActionControllerApp_Model CreateNew()
        {
            ActionControllerApp ActionControllerApp = new ActionControllerApp();
            Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model = this.ConverTo_Default_Form_ActionControllerApp_Model(ActionControllerApp);
            return Default_Form_ActionControllerApp_Model;
        } 
    }

	public partial class Default_Form_ActionControllerApp_ModelBLM : BaseDefault_Form_ActionControllerApp_ModelBLM
	{
		public Default_Form_ActionControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
