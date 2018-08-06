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
	public partial class BaseDefault_RoleAppFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_RoleAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual RoleApp ConverTo_RoleApp(Default_RoleAppFormView Default_RoleAppFormView)
        {
			RoleApp RoleApp = null;
            if (Default_RoleAppFormView.Id != 0)
            {
                RoleApp = new RoleAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_RoleAppFormView.Id);
            }
            else
            {
                RoleApp = new RoleApp();
            } 
			RoleApp.Code = Default_RoleAppFormView.Code;
			RoleApp.Description = Default_RoleAppFormView.Description;
			RoleApp.Id = Default_RoleAppFormView.Id;
            return RoleApp;
        }
        public virtual Default_RoleAppFormView ConverTo_Default_RoleAppFormView(RoleApp RoleApp)
        {  
			Default_RoleAppFormView Default_RoleAppFormView = new Default_RoleAppFormView();
			Default_RoleAppFormView.toStringValue = RoleApp.ToString();
			Default_RoleAppFormView.Code = RoleApp.Code;
			Default_RoleAppFormView.Description = RoleApp.Description;
			Default_RoleAppFormView.Id = RoleApp.Id;
            return Default_RoleAppFormView;            
        }
    }

	public partial class Default_RoleAppFormViewBLM : BaseDefault_RoleAppFormViewBLM
	{
		public Default_RoleAppFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
