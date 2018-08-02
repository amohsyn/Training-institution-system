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
	public partial class BaseDefault_RoleAppDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_RoleAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual RoleApp ConverTo_RoleApp(Default_RoleAppDetailsView Default_RoleAppDetailsView)
        {
			RoleApp RoleApp = null;
            if (Default_RoleAppDetailsView.Id != 0)
            {
                RoleApp = new RoleAppBLO(this.UnitOfWork).FindBaseEntityByID(Default_RoleAppDetailsView.Id);
            }
            else
            {
                RoleApp = new RoleApp();
            }
			RoleApp.Code = Default_RoleAppDetailsView.Code;
			RoleApp.Description = Default_RoleAppDetailsView.Description;
			RoleApp.Id = Default_RoleAppDetailsView.Id;
            return RoleApp;
        }
        public virtual Default_RoleAppDetailsView ConverTo_Default_RoleAppDetailsView(RoleApp RoleApp)
        {  
            Default_RoleAppDetailsView Default_RoleAppDetailsView = new Default_RoleAppDetailsView();
			Default_RoleAppDetailsView.Code = RoleApp.Code;
			Default_RoleAppDetailsView.Description = RoleApp.Description;
			Default_RoleAppDetailsView.Id = RoleApp.Id;
            return Default_RoleAppDetailsView;            
        }
    }

	public partial class Default_RoleAppDetailsViewBLM : BaseDefault_RoleAppDetailsViewBLM
	{
		public Default_RoleAppDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
