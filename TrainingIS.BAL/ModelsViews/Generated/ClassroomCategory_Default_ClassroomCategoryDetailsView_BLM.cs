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
	public partial class BaseDefault_ClassroomCategoryDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ClassroomCategoryDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ClassroomCategory ConverTo_ClassroomCategory(Default_ClassroomCategoryDetailsView Default_ClassroomCategoryDetailsView)
        {
			ClassroomCategory ClassroomCategory = new ClassroomCategory();
			ClassroomCategory.Code = Default_ClassroomCategoryDetailsView.Code;
			ClassroomCategory.Name = Default_ClassroomCategoryDetailsView.Name;
			ClassroomCategory.Description = Default_ClassroomCategoryDetailsView.Description;
			ClassroomCategory.Id = Default_ClassroomCategoryDetailsView.Id;
            return ClassroomCategory;

        }
        public virtual Default_ClassroomCategoryDetailsView ConverTo_Default_ClassroomCategoryDetailsView(ClassroomCategory ClassroomCategory)
        {
            Default_ClassroomCategoryDetailsView Default_ClassroomCategoryDetailsView = new Default_ClassroomCategoryDetailsView();
			Default_ClassroomCategoryDetailsView.Code = ClassroomCategory.Code;
			Default_ClassroomCategoryDetailsView.Name = ClassroomCategory.Name;
			Default_ClassroomCategoryDetailsView.Description = ClassroomCategory.Description;
			Default_ClassroomCategoryDetailsView.Id = ClassroomCategory.Id;
            return Default_ClassroomCategoryDetailsView;            
        }
    }

	public partial class Default_ClassroomCategoryDetailsViewBLM : BaseDefault_ClassroomCategoryDetailsViewBLM
	{
		public Default_ClassroomCategoryDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
