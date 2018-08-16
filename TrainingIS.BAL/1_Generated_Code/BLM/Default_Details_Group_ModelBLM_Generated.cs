//modelType = Default_Details_Group_Model

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

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Group_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(Default_Details_Group_Model Default_Details_Group_Model)
        {
			Group Group = null;
            if (Default_Details_Group_Model.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Group_Model.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingType = Default_Details_Group_Model.TrainingType;
			Group.TrainingYear = Default_Details_Group_Model.TrainingYear;
			Group.Specialty = Default_Details_Group_Model.Specialty;
			Group.YearStudy = Default_Details_Group_Model.YearStudy;
			Group.Code = Default_Details_Group_Model.Code;
			Group.Description = Default_Details_Group_Model.Description;
			Group.Id = Default_Details_Group_Model.Id;
            return Group;
        }
        public virtual Default_Details_Group_Model ConverTo_Default_Details_Group_Model(Group Group)
        {  
			Default_Details_Group_Model Default_Details_Group_Model = new Default_Details_Group_Model();
			Default_Details_Group_Model.toStringValue = Group.ToString();
			Default_Details_Group_Model.TrainingType = Group.TrainingType;
			Default_Details_Group_Model.TrainingYear = Group.TrainingYear;
			Default_Details_Group_Model.Specialty = Group.Specialty;
			Default_Details_Group_Model.YearStudy = Group.YearStudy;
			Default_Details_Group_Model.Code = Group.Code;
			Default_Details_Group_Model.Description = Group.Description;
			Default_Details_Group_Model.Id = Group.Id;
            return Default_Details_Group_Model;            
        }

		public virtual Default_Details_Group_Model CreateNew()
        {
            Group Group = new Group();
            Default_Details_Group_Model Default_Details_Group_Model = this.ConverTo_Default_Details_Group_Model(Group);
            return Default_Details_Group_Model;
        } 
    }

	public partial class Default_Details_Group_ModelBLM : BaseDefault_Details_Group_ModelBLM
	{
		public Default_Details_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
