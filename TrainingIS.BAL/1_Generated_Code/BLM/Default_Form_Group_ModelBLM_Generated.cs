//modelType = Default_Form_Group_Model

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Group_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Form_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Group ConverTo_Group(Default_Form_Group_Model Default_Form_Group_Model)
        {
			Group Group = null;
            if (Default_Form_Group_Model.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork).FindBaseEntityByID(Default_Form_Group_Model.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingTypeId = Default_Form_Group_Model.TrainingTypeId;
			Group.TrainingYearId = Default_Form_Group_Model.TrainingYearId;
			Group.SpecialtyId = Default_Form_Group_Model.SpecialtyId;
			Group.YearStudyId = Default_Form_Group_Model.YearStudyId;
			Group.Code = Default_Form_Group_Model.Code;
			Group.Description = Default_Form_Group_Model.Description;
			Group.Id = Default_Form_Group_Model.Id;
            return Group;
        }
        public virtual Default_Form_Group_Model ConverTo_Default_Form_Group_Model(Group Group)
        {  
			Default_Form_Group_Model Default_Form_Group_Model = new Default_Form_Group_Model();
			Default_Form_Group_Model.toStringValue = Group.ToString();
			Default_Form_Group_Model.TrainingTypeId = Group.TrainingTypeId;
			Default_Form_Group_Model.TrainingYearId = Group.TrainingYearId;
			Default_Form_Group_Model.SpecialtyId = Group.SpecialtyId;
			Default_Form_Group_Model.YearStudyId = Group.YearStudyId;
			Default_Form_Group_Model.Code = Group.Code;
			Default_Form_Group_Model.Description = Group.Description;
			Default_Form_Group_Model.Id = Group.Id;
            return Default_Form_Group_Model;            
        }

		public virtual Default_Form_Group_Model CreateNew()
        {
            Group Group = new Group();
            Default_Form_Group_Model Default_Form_Group_Model = this.ConverTo_Default_Form_Group_Model(Group);
            return Default_Form_Group_Model;
        } 
    }

	public partial class Default_Form_Group_ModelBLM : BaseDefault_Form_Group_ModelBLM
	{
		public Default_Form_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
