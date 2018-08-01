using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDetailsGroupViewBLM : ViewModelBLM
    {
        
        public BaseDetailsGroupViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Group ConverTo_Group(DetailsGroupView DetailsGroupView)
        {
			Group Group = new Group();
			Group.Code = DetailsGroupView.Code;
			Group.YearStudy = DetailsGroupView.YearStudy;
			Group.Specialty = DetailsGroupView.Specialty;
			Group.TrainingType = DetailsGroupView.TrainingType;
			Group.Id = DetailsGroupView.Id;
            return Group;

        }
        public virtual DetailsGroupView ConverTo_DetailsGroupView(Group Group)
        {
            DetailsGroupView DetailsGroupView = new DetailsGroupView();
			DetailsGroupView.TrainingType = Group.TrainingType;
			DetailsGroupView.Specialty = Group.Specialty;
			DetailsGroupView.YearStudy = Group.YearStudy;
			DetailsGroupView.Code = Group.Code;
			DetailsGroupView.Id = Group.Id;
            return DetailsGroupView;            
        }
    }

	public partial class DetailsGroupViewBLM : BaseDetailsGroupViewBLM
	{
		public DetailsGroupViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
