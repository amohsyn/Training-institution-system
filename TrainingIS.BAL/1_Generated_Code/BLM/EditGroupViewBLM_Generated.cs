//modelType = EditGroupView

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseEditGroupViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseEditGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(EditGroupView EditGroupView)
        {
			Group Group = null;
            if (EditGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(EditGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingYear = EditGroupView.TrainingYear;
			Group.TrainingYearId = EditGroupView.TrainingYearId;
			Group.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(EditGroupView.TrainingYearId)) ;
			Group.Specialty = EditGroupView.Specialty;
			Group.SpecialtyId = EditGroupView.SpecialtyId;
			Group.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(EditGroupView.SpecialtyId)) ;
			Group.TrainingType = EditGroupView.TrainingType;
			Group.TrainingTypeId = EditGroupView.TrainingTypeId;
			Group.TrainingType = new TrainingTypeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(EditGroupView.TrainingTypeId)) ;
			Group.YearStudy = EditGroupView.YearStudy;
			Group.YearStudyId = EditGroupView.YearStudyId;
			Group.YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(EditGroupView.YearStudyId)) ;
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

		public virtual EditGroupView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            EditGroupView EditGroupView = this.ConverTo_EditGroupView(Group);
            return EditGroupView;
        } 
    }

	public partial class EditGroupViewBLM : BaseEditGroupViewBLM
	{
		public EditGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
