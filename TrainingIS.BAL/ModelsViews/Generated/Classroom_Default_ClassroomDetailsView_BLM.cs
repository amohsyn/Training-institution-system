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
	public partial class BaseDefault_ClassroomDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ClassroomDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Classroom ConverTo_Classroom(Default_ClassroomDetailsView Default_ClassroomDetailsView)
        {
			Classroom Classroom = null;
            if (Default_ClassroomDetailsView.Id != 0)
            {
                Classroom = new ClassroomBLO(this.UnitOfWork).FindBaseEntityByID(Default_ClassroomDetailsView.Id);
            }
            else
            {
                Classroom = new Classroom();
            } 
			Classroom.Code = Default_ClassroomDetailsView.Code;
			Classroom.Name = Default_ClassroomDetailsView.Name;
			Classroom.ClassroomCategory = Default_ClassroomDetailsView.ClassroomCategory;
			Classroom.Description = Default_ClassroomDetailsView.Description;
			Classroom.Id = Default_ClassroomDetailsView.Id;
            return Classroom;
        }
        public virtual Default_ClassroomDetailsView ConverTo_Default_ClassroomDetailsView(Classroom Classroom)
        {  
			Default_ClassroomDetailsView Default_ClassroomDetailsView = new Default_ClassroomDetailsView();
			Default_ClassroomDetailsView.toStringValue = Classroom.ToString();
			Default_ClassroomDetailsView.Code = Classroom.Code;
			Default_ClassroomDetailsView.Name = Classroom.Name;
			Default_ClassroomDetailsView.ClassroomCategory = Classroom.ClassroomCategory;
			Default_ClassroomDetailsView.Description = Classroom.Description;
			Default_ClassroomDetailsView.Id = Classroom.Id;
            return Default_ClassroomDetailsView;            
        }

		public virtual Default_ClassroomDetailsView CreateNew()
        {
            Classroom Classroom = new Classroom();
            Default_ClassroomDetailsView Default_ClassroomDetailsView = this.ConverTo_Default_ClassroomDetailsView(Classroom);
            return Default_ClassroomDetailsView;
        } 
    }

	public partial class Default_ClassroomDetailsViewBLM : BaseDefault_ClassroomDetailsViewBLM
	{
		public Default_ClassroomDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
