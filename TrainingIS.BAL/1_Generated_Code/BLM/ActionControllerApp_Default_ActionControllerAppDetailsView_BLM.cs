using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_ActionControllerAppDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ActionControllerAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ActionControllerApp ConverTo_ActionControllerApp(Default_ActionControllerAppDetailsView Default_ActionControllerAppDetailsView)
        {
			ActionControllerApp ActionControllerApp = null;
            if (Default_ActionControllerAppDetailsView.Id != 0)
            {
                ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_ActionControllerAppDetailsView.Id);
            }
            else
            {
                ActionControllerApp = new ActionControllerApp();
            } 
			ActionControllerApp.Code = Default_ActionControllerAppDetailsView.Code;
			ActionControllerApp.Name = Default_ActionControllerAppDetailsView.Name;
			ActionControllerApp.Description = Default_ActionControllerAppDetailsView.Description;
			ActionControllerApp.ControllerApp = Default_ActionControllerAppDetailsView.ControllerApp;
			ActionControllerApp.Id = Default_ActionControllerAppDetailsView.Id;
            return ActionControllerApp;
        }
        public virtual Default_ActionControllerAppDetailsView ConverTo_Default_ActionControllerAppDetailsView(ActionControllerApp ActionControllerApp)
        {  
			Default_ActionControllerAppDetailsView Default_ActionControllerAppDetailsView = new Default_ActionControllerAppDetailsView();
			Default_ActionControllerAppDetailsView.toStringValue = ActionControllerApp.ToString();
			Default_ActionControllerAppDetailsView.Code = ActionControllerApp.Code;
			Default_ActionControllerAppDetailsView.Name = ActionControllerApp.Name;
			Default_ActionControllerAppDetailsView.Description = ActionControllerApp.Description;
			Default_ActionControllerAppDetailsView.ControllerApp = ActionControllerApp.ControllerApp;
			Default_ActionControllerAppDetailsView.Id = ActionControllerApp.Id;
            return Default_ActionControllerAppDetailsView;            
        }

		public virtual Default_ActionControllerAppDetailsView CreateNew()
        {
            ActionControllerApp ActionControllerApp = new ActionControllerApp();
            Default_ActionControllerAppDetailsView Default_ActionControllerAppDetailsView = this.ConverTo_Default_ActionControllerAppDetailsView(ActionControllerApp);
            return Default_ActionControllerAppDetailsView;
        } 
    }

	public partial class Default_ActionControllerAppDetailsViewBLM : BaseDefault_ActionControllerAppDetailsViewBLM
	{
		public Default_ActionControllerAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
