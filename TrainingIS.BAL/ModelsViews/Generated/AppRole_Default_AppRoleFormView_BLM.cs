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
	public partial class BaseDefault_AppRoleFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_AppRoleFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual AppRole ConverTo_AppRole(Default_AppRoleFormView Default_AppRoleFormView)
        {
			AppRole AppRole = new AppRole();
			AppRole.Code = Default_AppRoleFormView.Code;
			AppRole.Description = Default_AppRoleFormView.Description;
			AppRole.Id = Default_AppRoleFormView.Id;
            return AppRole;

        }
        public virtual Default_AppRoleFormView ConverTo_Default_AppRoleFormView(AppRole AppRole)
        {
            Default_AppRoleFormView Default_AppRoleFormView = new Default_AppRoleFormView();
			Default_AppRoleFormView.Code = AppRole.Code;
			Default_AppRoleFormView.Description = AppRole.Description;
			Default_AppRoleFormView.Id = AppRole.Id;
            return Default_AppRoleFormView;            
        }
    }

	public partial class Default_AppRoleFormViewBLM : BaseDefault_AppRoleFormViewBLM
	{
		public Default_AppRoleFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
