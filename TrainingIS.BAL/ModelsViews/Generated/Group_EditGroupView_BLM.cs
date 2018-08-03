using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseEditGroupViewBLM : ViewModelBLM
    {
        
        public BaseEditGroupViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Group ConverTo_Group(EditGroupView EditGroupView)
        {
			Group Group = null;
            if (EditGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork).FindBaseEntityByID(EditGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingYear = EditGroupView.TrainingYear;
			Group.TrainingYearId = EditGroupView.TrainingYearId;
			Group.Specialty = EditGroupView.Specialty;
			Group.SpecialtyId = EditGroupView.SpecialtyId;
			Group.TrainingType = EditGroupView.TrainingType;
			Group.TrainingTypeId = EditGroupView.TrainingTypeId;
			Group.YearStudy = EditGroupView.YearStudy;
			Group.YearStudyId = EditGroupView.YearStudyId;
			Group.Code = EditGroupView.Code;
			Group.Id = EditGroupView.Id;
            return Group;
        }
        public virtual EditGroupView ConverTo_EditGroupView(Group Group)
        {  
			EditGroupView EditGroupView = new EditGroupView();
			EditGroupView.toStringValue = Group.ToString();
			EditGroupView.TrainingType = Group.TrainingType;
			EditGroupView.TrainingTypeId = Group.TrainingTypeId;
			EditGroupView.TrainingYear = Group.TrainingYear;
			EditGroupView.TrainingYearId = Group.TrainingYearId;
			EditGroupView.Specialty = Group.Specialty;
			EditGroupView.SpecialtyId = Group.SpecialtyId;
			EditGroupView.YearStudy = Group.YearStudy;
			EditGroupView.YearStudyId = Group.YearStudyId;
			EditGroupView.Code = Group.Code;
			EditGroupView.Id = Group.Id;
            return EditGroupView;            
        }
    }

	public partial class EditGroupViewBLM : BaseEditGroupViewBLM
	{
		public EditGroupViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
