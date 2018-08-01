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
	public partial class BaseDefault_ClassroomDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ClassroomDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Classroom ConverTo_Classroom(Default_ClassroomDetailsView Default_ClassroomDetailsView)
        {
			Classroom Classroom = new Classroom();
			Classroom.Code = Default_ClassroomDetailsView.Code;
			Classroom.Name = Default_ClassroomDetailsView.Name;
			Classroom.ClassroomCategoryId = Default_ClassroomDetailsView.ClassroomCategoryId;
			Classroom.Description = Default_ClassroomDetailsView.Description;
			Classroom.Id = Default_ClassroomDetailsView.Id;
            return Classroom;

        }
        public virtual Default_ClassroomDetailsView ConverTo_Default_ClassroomDetailsView(Classroom Classroom)
        {
            Default_ClassroomDetailsView Default_ClassroomDetailsView = new Default_ClassroomDetailsView();
			Default_ClassroomDetailsView.Code = Classroom.Code;
			Default_ClassroomDetailsView.Name = Classroom.Name;
			Default_ClassroomDetailsView.ClassroomCategoryId = Classroom.ClassroomCategoryId;
			Default_ClassroomDetailsView.Description = Classroom.Description;
			Default_ClassroomDetailsView.Id = Classroom.Id;
            return Default_ClassroomDetailsView;            
        }
    }

	public partial class Default_ClassroomDetailsViewBLM : BaseDefault_ClassroomDetailsViewBLM
	{
		public Default_ClassroomDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
