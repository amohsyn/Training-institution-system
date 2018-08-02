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
	public partial class BaseDefault_ActionControllerAppFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ActionControllerAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ActionControllerApp ConverTo_ActionControllerApp(Default_ActionControllerAppFormView Default_ActionControllerAppFormView)
        {
			ActionControllerApp ActionControllerApp = new ActionControllerApp();
			ActionControllerApp.Code = Default_ActionControllerAppFormView.Code;
			ActionControllerApp.Name = Default_ActionControllerAppFormView.Name;
			ActionControllerApp.Description = Default_ActionControllerAppFormView.Description;
			ActionControllerApp.AppControllerId = Default_ActionControllerAppFormView.AppControllerId;
			ActionControllerApp.Id = Default_ActionControllerAppFormView.Id;
            return ActionControllerApp;

        }
        public virtual Default_ActionControllerAppFormView ConverTo_Default_ActionControllerAppFormView(ActionControllerApp ActionControllerApp)
        {
            Default_ActionControllerAppFormView Default_ActionControllerAppFormView = new Default_ActionControllerAppFormView();
			Default_ActionControllerAppFormView.Code = ActionControllerApp.Code;
			Default_ActionControllerAppFormView.Name = ActionControllerApp.Name;
			Default_ActionControllerAppFormView.Description = ActionControllerApp.Description;
			Default_ActionControllerAppFormView.AppControllerId = ActionControllerApp.AppControllerId;
			Default_ActionControllerAppFormView.Id = ActionControllerApp.Id;
            return Default_ActionControllerAppFormView;            
        }
    }

	public partial class Default_ActionControllerAppFormViewBLM : BaseDefault_ActionControllerAppFormViewBLM
	{
		public Default_ActionControllerAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
