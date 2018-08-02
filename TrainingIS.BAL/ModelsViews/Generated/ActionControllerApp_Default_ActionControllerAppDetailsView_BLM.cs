using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_ActionControllerAppDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ActionControllerAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ActionControllerApp ConverTo_ActionControllerApp(Default_ActionControllerAppDetailsView Default_ActionControllerAppDetailsView)
        {
			ActionControllerApp ActionControllerApp = new ActionControllerApp();
			ActionControllerApp.Code = Default_ActionControllerAppDetailsView.Code;
			ActionControllerApp.Name = Default_ActionControllerAppDetailsView.Name;
			ActionControllerApp.Description = Default_ActionControllerAppDetailsView.Description;
			ActionControllerApp.AppControllerId = Default_ActionControllerAppDetailsView.AppControllerId;
			ActionControllerApp.Id = Default_ActionControllerAppDetailsView.Id;
            return ActionControllerApp;

        }
        public virtual Default_ActionControllerAppDetailsView ConverTo_Default_ActionControllerAppDetailsView(ActionControllerApp ActionControllerApp)
        {
            Default_ActionControllerAppDetailsView Default_ActionControllerAppDetailsView = new Default_ActionControllerAppDetailsView();
			Default_ActionControllerAppDetailsView.Code = ActionControllerApp.Code;
			Default_ActionControllerAppDetailsView.Name = ActionControllerApp.Name;
			Default_ActionControllerAppDetailsView.Description = ActionControllerApp.Description;
			Default_ActionControllerAppDetailsView.AppControllerId = ActionControllerApp.AppControllerId;
			Default_ActionControllerAppDetailsView.Id = ActionControllerApp.Id;
            return Default_ActionControllerAppDetailsView;            
        }
    }

	public partial class Default_ActionControllerAppDetailsViewBLM : BaseDefault_ActionControllerAppDetailsViewBLM
	{
		public Default_ActionControllerAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
