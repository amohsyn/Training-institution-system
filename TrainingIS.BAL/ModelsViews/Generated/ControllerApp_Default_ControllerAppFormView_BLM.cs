using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_ControllerAppFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ControllerAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ControllerApp ConverTo_ControllerApp(Default_ControllerAppFormView Default_ControllerAppFormView)
        {
			ControllerApp ControllerApp = null;
            if (Default_ControllerAppFormView.Id != 0)
            {
                ControllerApp = new ControllerAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_ControllerAppFormView.Id);
            }
            else
            {
                ControllerApp = new ControllerApp();
            } 
			ControllerApp.Code = Default_ControllerAppFormView.Code;
			ControllerApp.Name = Default_ControllerAppFormView.Name;
			ControllerApp.Description = Default_ControllerAppFormView.Description;
			ControllerApp.Id = Default_ControllerAppFormView.Id;
            return ControllerApp;
        }
        public virtual Default_ControllerAppFormView ConverTo_Default_ControllerAppFormView(ControllerApp ControllerApp)
        {  
            Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormView();
			Default_ControllerAppFormView.Code = ControllerApp.Code;
			Default_ControllerAppFormView.Name = ControllerApp.Name;
			Default_ControllerAppFormView.Description = ControllerApp.Description;
			Default_ControllerAppFormView.Id = ControllerApp.Id;
            return Default_ControllerAppFormView;            
        }
    }

	public partial class Default_ControllerAppFormViewBLM : BaseDefault_ControllerAppFormViewBLM
	{
		public Default_ControllerAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
