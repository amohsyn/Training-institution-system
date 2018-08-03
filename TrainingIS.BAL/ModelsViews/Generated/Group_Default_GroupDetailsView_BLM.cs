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
	public partial class BaseDefault_GroupDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_GroupDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Group ConverTo_Group(Default_GroupDetailsView Default_GroupDetailsView)
        {
			Group Group = null;
            if (Default_GroupDetailsView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork).FindBaseEntityByID(Default_GroupDetailsView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingType = Default_GroupDetailsView.TrainingType;
			Group.TrainingYear = Default_GroupDetailsView.TrainingYear;
			Group.Specialty = Default_GroupDetailsView.Specialty;
			Group.YearStudy = Default_GroupDetailsView.YearStudy;
			Group.Code = Default_GroupDetailsView.Code;
			Group.Description = Default_GroupDetailsView.Description;
			Group.Id = Default_GroupDetailsView.Id;
            return Group;
        }
        public virtual Default_GroupDetailsView ConverTo_Default_GroupDetailsView(Group Group)
        {  
            Default_GroupDetailsView Default_GroupDetailsView = new Default_GroupDetailsView();
			Default_GroupDetailsView.TrainingType = Group.TrainingType;
			Default_GroupDetailsView.TrainingYear = Group.TrainingYear;
			Default_GroupDetailsView.Specialty = Group.Specialty;
			Default_GroupDetailsView.YearStudy = Group.YearStudy;
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
