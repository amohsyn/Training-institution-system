//modelType = Form_Group_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseForm_Group_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseForm_Group_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(Form_Group_Model Form_Group_Model)
        {
			Group Group = null;
            if (Form_Group_Model.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Form_Group_Model.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingYear = Form_Group_Model.TrainingYear;
			Group.TrainingYearId = Form_Group_Model.TrainingYearId;
			Group.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Group_Model.TrainingYearId)) ;
			Group.Specialty = Form_Group_Model.Specialty;
			Group.SpecialtyId = Form_Group_Model.SpecialtyId;
			Group.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Group_Model.SpecialtyId)) ;
			Group.TrainingType = Form_Group_Model.TrainingType;
			Group.TrainingTypeId = Form_Group_Model.TrainingTypeId;
			Group.TrainingType = new TrainingTypeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Group_Model.TrainingTypeId)) ;
			Group.YearStudy = Form_Group_Model.YearStudy;
			Group.YearStudyId = Form_Group_Model.YearStudyId;
			Group.YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Group_Model.YearStudyId)) ;
			Group.Code = Form_Group_Model.Code;
			Group.Id = Form_Group_Model.Id;
            return Group;
        }
        public virtual void ConverTo_Form_Group_Model(Form_Group_Model Form_Group_Model, Group Group)
        {  
			 
			Form_Group_Model.toStringValue = Group.ToString();
			Form_Group_Model.TrainingType = Group.TrainingType;
			Form_Group_Model.TrainingTypeId = Group.TrainingTypeId;
			Form_Group_Model.TrainingYear = Group.TrainingYear;
			Form_Group_Model.TrainingYearId = Group.TrainingYearId;
			Form_Group_Model.Specialty = Group.Specialty;
			Form_Group_Model.SpecialtyId = Group.SpecialtyId;
			Form_Group_Model.YearStudy = Group.YearStudy;
			Form_Group_Model.YearStudyId = Group.YearStudyId;
			Form_Group_Model.Code = Group.Code;
			Form_Group_Model.Id = Group.Id;
                     
        }

    }

	public partial class Form_Group_ModelBLM : BaseForm_Group_Model_BLM
	{
		public Form_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
