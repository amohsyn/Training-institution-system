//modelType = DetailsGroupView

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDetailsGroupViewBLM : BaseModelBLM
    {
        
        public BaseDetailsGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Group ConverTo_Group(DetailsGroupView DetailsGroupView)
        {
			Group Group = null;
            if (DetailsGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork).FindBaseEntityByID(DetailsGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
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
			DetailsGroupView.toStringValue = Group.ToString();
			DetailsGroupView.TrainingType = Group.TrainingType;
			DetailsGroupView.Specialty = Group.Specialty;
			DetailsGroupView.YearStudy = Group.YearStudy;
			DetailsGroupView.Code = Group.Code;
			DetailsGroupView.Id = Group.Id;
            return DetailsGroupView;            
        }

		public virtual DetailsGroupView CreateNew()
        {
            Group Group = new Group();
            DetailsGroupView DetailsGroupView = this.ConverTo_DetailsGroupView(Group);
            return DetailsGroupView;
        } 
    }

	public partial class DetailsGroupViewBLM : BaseDetailsGroupViewBLM
	{
		public DetailsGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
