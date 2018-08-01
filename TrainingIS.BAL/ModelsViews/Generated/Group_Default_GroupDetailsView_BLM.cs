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
	public partial class BaseDefault_GroupDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_GroupDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Group ConverTo_Group(Default_GroupDetailsView Default_GroupDetailsView)
        {
			Group Group = new Group();
			Group.TrainingTypeId = Default_GroupDetailsView.TrainingTypeId;
			Group.TrainingYearId = Default_GroupDetailsView.TrainingYearId;
			Group.SpecialtyId = Default_GroupDetailsView.SpecialtyId;
			Group.YearStudyId = Default_GroupDetailsView.YearStudyId;
			Group.Code = Default_GroupDetailsView.Code;
			Group.Description = Default_GroupDetailsView.Description;
			Group.Id = Default_GroupDetailsView.Id;
            return Group;

        }
        public virtual Default_GroupDetailsView ConverTo_Default_GroupDetailsView(Group Group)
        {
            Default_GroupDetailsView Default_GroupDetailsView = new Default_GroupDetailsView();
			Default_GroupDetailsView.TrainingTypeId = Group.TrainingTypeId;
			Default_GroupDetailsView.TrainingYearId = Group.TrainingYearId;
			Default_GroupDetailsView.SpecialtyId = Group.SpecialtyId;
			Default_GroupDetailsView.YearStudyId = Group.YearStudyId;
			Default_GroupDetailsView.Code = Group.Code;
			Default_GroupDetailsView.Description = Group.Description;
			Default_GroupDetailsView.Id = Group.Id;
            return Default_GroupDetailsView;            
        }
    }

	public partial class Default_GroupDetailsViewBLM : BaseDefault_GroupDetailsViewBLM
	{
		public Default_GroupDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
