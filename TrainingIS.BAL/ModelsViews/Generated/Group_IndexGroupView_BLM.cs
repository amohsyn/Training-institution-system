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
	public partial class BaseIndexGroupViewBLM : ViewModelBLM
    {
        
        public BaseIndexGroupViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Group ConverTo_Group(IndexGroupView IndexGroupView)
        {
			Group Group = null;
            if (IndexGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork).FindBaseEntityByID(IndexGroupView.Id);
            }
            else
            {
                Group = new Group();
            }
			Group.Code = IndexGroupView.Code;
			Group.YearStudy = IndexGroupView.YearStudy;
			Group.Specialty = IndexGroupView.Specialty;
			Group.TrainingType = IndexGroupView.TrainingType;
			Group.Id = IndexGroupView.Id;
            return Group;
        }
        public virtual IndexGroupView ConverTo_IndexGroupView(Group Group)
        {  
            IndexGroupView IndexGroupView = new IndexGroupView();
			IndexGroupView.TrainingType = Group.TrainingType;
			IndexGroupView.Specialty = Group.Specialty;
			IndexGroupView.YearStudy = Group.YearStudy;
			IndexGroupView.Code = Group.Code;
			IndexGroupView.Id = Group.Id;
            return IndexGroupView;            
        }
    }

	public partial class IndexGroupViewBLM : BaseIndexGroupViewBLM
	{
		public IndexGroupViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
