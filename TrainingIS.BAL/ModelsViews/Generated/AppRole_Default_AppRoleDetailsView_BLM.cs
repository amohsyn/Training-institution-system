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
	public partial class BaseDefault_AppRoleDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AppRoleDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AppRole ConverTo_AppRole(Default_AppRoleDetailsView Default_AppRoleDetailsView)
        {
			AppRole AppRole = new AppRole();
			AppRole.Code = Default_AppRoleDetailsView.Code;
			AppRole.Description = Default_AppRoleDetailsView.Description;
			AppRole.Id = Default_AppRoleDetailsView.Id;
            return AppRole;

        }
        public virtual Default_AppRoleDetailsView ConverTo_Default_AppRoleDetailsView(AppRole AppRole)
        {
            Default_AppRoleDetailsView Default_AppRoleDetailsView = new Default_AppRoleDetailsView();
			Default_AppRoleDetailsView.Code = AppRole.Code;
			Default_AppRoleDetailsView.Description = AppRole.Description;
			Default_AppRoleDetailsView.Id = AppRole.Id;
            return Default_AppRoleDetailsView;            
        }
    }

	public partial class Default_AppRoleDetailsViewBLM : BaseDefault_AppRoleDetailsViewBLM
	{
		public Default_AppRoleDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
