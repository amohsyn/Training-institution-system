//modelType = Default_Form_Mission_Working_Group_Model

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
	public partial class BaseDefault_Form_Mission_Working_Group_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Mission_Working_Group_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Mission_Working_Group ConverTo_Mission_Working_Group(Default_Form_Mission_Working_Group_Model Default_Form_Mission_Working_Group_Model)
        {
			Mission_Working_Group Mission_Working_Group = null;
            if (Default_Form_Mission_Working_Group_Model.Id != 0)
            {
                Mission_Working_Group = new Mission_Working_GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Mission_Working_Group_Model.Id);
            }
            else
            {
                Mission_Working_Group = new Mission_Working_Group();
            } 
			Mission_Working_Group.Code = Default_Form_Mission_Working_Group_Model.Code;
			Mission_Working_Group.Name = Default_Form_Mission_Working_Group_Model.Name;
			Mission_Working_Group.DecisionAuthority = Default_Form_Mission_Working_Group_Model.DecisionAuthority;
			Mission_Working_Group.Description = Default_Form_Mission_Working_Group_Model.Description;
			Mission_Working_Group.Reference = Default_Form_Mission_Working_Group_Model.Reference;
			Mission_Working_Group.Id = Default_Form_Mission_Working_Group_Model.Id;
            return Mission_Working_Group;
        }
        public virtual void ConverTo_Default_Form_Mission_Working_Group_Model(Default_Form_Mission_Working_Group_Model Default_Form_Mission_Working_Group_Model, Mission_Working_Group Mission_Working_Group)
        {  
			 
			Default_Form_Mission_Working_Group_Model.toStringValue = Mission_Working_Group.ToString();
			Default_Form_Mission_Working_Group_Model.Code = Mission_Working_Group.Code;
			Default_Form_Mission_Working_Group_Model.Name = Mission_Working_Group.Name;
			Default_Form_Mission_Working_Group_Model.DecisionAuthority = Mission_Working_Group.DecisionAuthority;
			Default_Form_Mission_Working_Group_Model.Description = Mission_Working_Group.Description;
			Default_Form_Mission_Working_Group_Model.Id = Mission_Working_Group.Id;
			Default_Form_Mission_Working_Group_Model.Reference = Mission_Working_Group.Reference;
                     
        }

    }

	public partial class Default_Form_Mission_Working_Group_ModelBLM : BaseDefault_Form_Mission_Working_Group_Model_BLM
	{
		public Default_Form_Mission_Working_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
