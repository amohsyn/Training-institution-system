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
	public partial class BaseDefault_ControllerAppDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ControllerAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ControllerApp ConverTo_ControllerApp(Default_ControllerAppDetailsView Default_ControllerAppDetailsView)
        {
			ControllerApp ControllerApp = new ControllerApp();
			ControllerApp.Code = Default_ControllerAppDetailsView.Code;
			ControllerApp.Name = Default_ControllerAppDetailsView.Name;
			ControllerApp.Description = Default_ControllerAppDetailsView.Description;
			ControllerApp.Id = Default_ControllerAppDetailsView.Id;
            return ControllerApp;

        }
        public virtual Default_ControllerAppDetailsView ConverTo_Default_ControllerAppDetailsView(ControllerApp ControllerApp)
        {
            Default_ControllerAppDetailsView Default_ControllerAppDetailsView = new Default_ControllerAppDetailsView();
			Default_ControllerAppDetailsView.Code = ControllerApp.Code;
			Default_ControllerAppDetailsView.Name = ControllerApp.Name;
			Default_ControllerAppDetailsView.Description = ControllerApp.Description;
			Default_ControllerAppDetailsView.Id = ControllerApp.Id;
            return Default_ControllerAppDetailsView;            
        }
    }

	public partial class Default_ControllerAppDetailsViewBLM : BaseDefault_ControllerAppDetailsViewBLM
	{
		public Default_ControllerAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
