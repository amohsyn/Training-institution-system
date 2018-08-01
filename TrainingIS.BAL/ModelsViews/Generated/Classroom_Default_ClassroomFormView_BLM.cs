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
	public partial class BaseDefault_ClassroomFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ClassroomFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Classroom ConverTo_Classroom(Default_ClassroomFormView Default_ClassroomFormView)
        {
			Classroom Classroom = new Classroom();
			Classroom.Code = Default_ClassroomFormView.Code;
			Classroom.Name = Default_ClassroomFormView.Name;
			Classroom.ClassroomCategoryId = Default_ClassroomFormView.ClassroomCategoryId;
			Classroom.Description = Default_ClassroomFormView.Description;
			Classroom.Id = Default_ClassroomFormView.Id;
            return Classroom;

        }
        public virtual Default_ClassroomFormView ConverTo_Default_ClassroomFormView(Classroom Classroom)
        {
            Default_ClassroomFormView Default_ClassroomFormView = new Default_ClassroomFormView();
			Default_ClassroomFormView.Code = Classroom.Code;
			Default_ClassroomFormView.Name = Classroom.Name;
			Default_ClassroomFormView.ClassroomCategoryId = Classroom.ClassroomCategoryId;
			Default_ClassroomFormView.Description = Classroom.Description;
			Default_ClassroomFormView.Id = Classroom.Id;
            return Default_ClassroomFormView;            
        }
    }

	public partial class Default_ClassroomFormViewBLM : BaseDefault_ClassroomFormViewBLM
	{
		public Default_ClassroomFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
